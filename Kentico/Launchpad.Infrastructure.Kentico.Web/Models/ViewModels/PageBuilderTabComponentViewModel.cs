using Launchpad.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Launchpad.Infrastructure.Kentico.Web.Models.ViewModels
{
    public class PageBuilderTabComponentViewModel
    {
        public Guid TabWidgetVariantIdentifier { get; set; }
        public PageBuilderWidgets PageBuilderWidgets { get; set; }
        public List<NameAndGuid> TabNameAndGuids { get; }
        public int Index { get; set; }
        public bool HasTabs => TabNameAndGuids != null && TabNameAndGuids.Count > 0;
        public List<PageBuilderTabViewModel> PageBuilderTabViewModels
        {
            get
            {
                var tabs = TabNameAndGuids
                    .Select(x => new PageBuilderTabViewModel
                    {
                        TabName = x.Name,
                        PageBuilderViewModel = new PageBuilderViewModel(PageBuilderWidgets)
                        {
                            AreaIdentifier = x.Guid.ToString()
                        }
                    })
                    .ToList();

                if (tabs.Count > 0)
                {
                    tabs[0].IsFirstTab = true;
                }

                return tabs;
            }
        }

        public PageBuilderTabComponentViewModel(PageBuilderWidgets pageBuilderWidgets, string tabNames)
        {
            PageBuilderWidgets = pageBuilderWidgets;

            TabNameAndGuids = JsonConvert.DeserializeObject<List<NameAndGuid>>(tabNames);
        }
    }
}