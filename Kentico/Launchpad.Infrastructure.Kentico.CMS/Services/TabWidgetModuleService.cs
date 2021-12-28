using CMS.DocumentEngine;
using Launchpad.Core.Constants;
using Launchpad.Core.Models;
using Launchpad.Infrastructure.Extensions;
using Launchpad.Infrastructure.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Launchpad.Infrastructure.Kentico.CMS.Services
{
    public class TabWidgetModuleService
    {
        private const string tabNamesKey = "tabNames";
        private const string indexKey = "index";
        private readonly CustomCmsModuleLoggingService customCmsModuleLoggingService;

        public TabWidgetModuleService()
        {
            this.customCmsModuleLoggingService = new CustomCmsModuleLoggingService();
        }

        public void InsertBefore(object sender, DocumentEventArgs e)
        {
            customCmsModuleLoggingService.LogInformation(nameof(TabWidgetModuleService), nameof(InsertBefore));
            EnsureTabWidgetEditableAreas(e.Node);

        }

        public void UpdateBefore(object sender, DocumentEventArgs e)
        {
            customCmsModuleLoggingService.LogInformation(nameof(TabWidgetModuleService), nameof(UpdateBefore));
            EnsureTabWidgetEditableAreas(e.Node);
        }

        private void EnsureTabWidgetEditableAreas(TreeNode treeNode)
        {
            RemoveEditableAreasForDeletedTabWidgets(treeNode);
            EnsureTabWidgetNumbering(treeNode);
            EnsureTabOrder(treeNode);
        }

        private void EnsureTabOrder(TreeNode treeNode)
        {
            var pageBuilderWidgets = treeNode.GetPageBuilderWidgets();

            if (pageBuilderWidgets is null)
            {
                return;
            }

            var tabWidgetAndIndividualTabGuids = GetTabWidgets(pageBuilderWidgets)
                .Select(x => new
                {
                    TabWidgetIdentifer = x.Identifier.ToString(),
                    TabIdentifiers = JsonConvert.DeserializeObject<List<NameAndGuid>>(x.PropertiesDictionary[tabNamesKey].ToString()).Select(y => y.Guid.ToString()).ToList()
                })
                .ToList();

            // no TabWidgets on page
            if (tabWidgetAndIndividualTabGuids.Count == 0)
            {
                return;
            }

            var editableAreas = pageBuilderWidgets.EditableAreas.ToList();

            // no editable areas on page
            if (editableAreas.Count == 0)
            {
                return;
            }

            var existingTabWidgetEditableAreaIndexRanges = tabWidgetAndIndividualTabGuids
                .Select(x => new
                {
                    TabWidgetIdentifier = x.TabWidgetIdentifer,
                    AreaIdentifierIndexes = x.TabIdentifiers
                        .Select(y => editableAreas.FindIndex(z => z.Identifier == y))
                        .Where(y => y != -1)
                        .ToList()
                })
                .Where(x => x.AreaIdentifierIndexes.Count > 0)
                .Select(x => new
                {
                    TabWidgetIdentifier = x.TabWidgetIdentifier,
                    Min = x.AreaIdentifierIndexes.Min(),
                    Max = x.AreaIdentifierIndexes.Max()
                })
                .OrderBy(x => x.Min)
                .ToList();

            // no TabWidget editable areas previously added to page
            if (existingTabWidgetEditableAreaIndexRanges.Count == 0)
            {
                return;
            }

            List<EditableArea> orderedEditableAreas = new List<EditableArea>();

            orderedEditableAreas.AddRange(editableAreas.GetRange(0, existingTabWidgetEditableAreaIndexRanges[0].Min));

            // stitch together ordered ranges of TabWidget editable areas
            foreach (var indexRange in existingTabWidgetEditableAreaIndexRanges)
            {
                var orderedTabIdentifiers = tabWidgetAndIndividualTabGuids
                    .First(y => y.TabWidgetIdentifer == indexRange.TabWidgetIdentifier)
                    .TabIdentifiers;

                orderedEditableAreas.AddRange(
                    editableAreas.GetRange(indexRange.Min, indexRange.Max - indexRange.Min + 1)
                    .OrderBy(x => orderedTabIdentifiers.IndexOf(x.Identifier)));
            }

            pageBuilderWidgets.EditableAreas = orderedEditableAreas;

            UpdatePageBuilderWidgets(treeNode, pageBuilderWidgets);
        }

        private void RemoveEditableAreasForDeletedTabWidgets(TreeNode treeNode)
        {
            var originalPageBuilderWidgets = treeNode.GetOriginalPageBuilderWidgets();

            if (originalPageBuilderWidgets is null)
            {
                return;
            }

            var originalTabGuids = GetTabWidgetGuids(originalPageBuilderWidgets);

            var currentPageBuilderWidgets = treeNode.GetPageBuilderWidgets();

            if (currentPageBuilderWidgets is null)
            {
                return;
            }

            var currentTabGuids = GetTabWidgetGuids(currentPageBuilderWidgets);

            var deletedTabWidgetEditableAreaIdentifiers = originalTabGuids.Except(currentTabGuids)
                .Select(x => x.ToString());

            if (deletedTabWidgetEditableAreaIdentifiers.Count() == 0)
            {
                return;
            }

            var editableAreas = currentPageBuilderWidgets.EditableAreas.ToList();
            editableAreas.RemoveAll(x => deletedTabWidgetEditableAreaIdentifiers.Contains(x.Identifier));

            currentPageBuilderWidgets.EditableAreas = editableAreas;

            UpdatePageBuilderWidgets(treeNode, currentPageBuilderWidgets);
        }

        private static void UpdatePageBuilderWidgets(TreeNode treeNode, PageBuilderWidgets pageBuilderWidgets)
        {
            var updatedPageBuilderWidgetsJson = JsonConvert.SerializeObject(
                                pageBuilderWidgets,
                                new JsonSerializerSettings
                                {
                                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                                });

            treeNode.SetValue("DocumentPageBuilderWidgets", updatedPageBuilderWidgetsJson);
        }

        private void EnsureTabWidgetNumbering(TreeNode treeNode)
        {
            var pageBuilderWidgets = treeNode.GetPageBuilderWidgets();

            if (pageBuilderWidgets is null)
            {
                return;
            }

            var tabWidgets = GetTabWidgets(pageBuilderWidgets);

            if (tabWidgets.Count() > 0)
            {
                int i = 1;
                tabWidgets.ToList().ForEach(x =>
                {
                    var widgetProperties = x.PropertiesDictionary;

                    if (widgetProperties == null)
                    {
                        return;
                    }

                    if (widgetProperties.TryGetValue(indexKey, out object _))
                    {
                        widgetProperties[indexKey] = i++.ToString();
                    }
                    else
                    {
                        widgetProperties.Add(indexKey, i++.ToString());
                    }

                    x.Properties = JObject.FromObject(widgetProperties);
                });

                UpdatePageBuilderWidgets(treeNode, pageBuilderWidgets);
            }
        }

        private List<Variant> GetTabWidgets(PageBuilderWidgets pageBuilderWidgets)
        {
            return pageBuilderWidgets
                .GetWidgetVariants(WidgetIdentifier.TabWidget)
                .Where(x => !string.IsNullOrWhiteSpace(x.PropertiesDictionary[tabNamesKey]?.ToString()))
                .ToList();
        }

        private List<Guid> GetTabWidgetGuids(PageBuilderWidgets pageBuilderWidgets)
        {
            return GetTabWidgets(pageBuilderWidgets)
                .SelectMany(x => JsonConvert.DeserializeObject<List<NameAndGuid>>(x.PropertiesDictionary[tabNamesKey].ToString()).Select(y => y.Guid))
                .ToList();
        }
    }
}
