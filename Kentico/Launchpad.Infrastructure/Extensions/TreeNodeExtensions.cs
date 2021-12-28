using CMS.Base;
using CMS.DocumentEngine;
using CMS.DocumentEngine.Types.Common;
using Launchpad.Core.Constants;
using Launchpad.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Launchpad.Infrastructure.Extensions
{

	public static class TreeNodeExtensions
	{
		#region Fields
		private static readonly List<string> defaultFields = TreeNode.New().Properties.Union(new string[] { "Fields" }).ToList();
		#endregion



		public static T ApplyMetadataSettings<T>(this T node)
			where T : TreeNode
		{
			if (node == null)
			{
				return null;
			}


			void applyInherited(string columnName)
			{
				if (String.IsNullOrWhiteSpace((string)node[columnName]))
				{
					node[columnName] = node.GetInheritedValue(columnName);
				}
			}


			// Apply inherited values if not supplied already
			applyInherited("DocumentPageTitle");
			applyInherited("DocumentPageDescription");


			return node;
		}


		public static Address GetAddress(this TreeNode node)
		{
			decimal? latitude = null;
			decimal? longitude = null;

			if (node.GetValue(nameof(Address.Latitude)) != null)
			{
				latitude = node.GetValue<decimal>(nameof(Address.Latitude), 0M);
			}

			if (node.GetValue(nameof(Address.Longitude)) != null)
			{
				longitude = node.GetValue<decimal>(nameof(Address.Longitude), 0M);
			}


			return new Address
			{
				Address1 = node.GetStringValue("Address1", String.Empty),
				Address2 = node.GetStringValue("Address2", String.Empty),
				City = node.GetStringValue("City", String.Empty),
				State = new State
				{
					Abbreviation = node.GetStringValue("State", String.Empty)
				},
				Zipcode = node.GetStringValue("Zipcode", String.Empty),

				Latitude = latitude,
				Longitude = longitude
			};
		}


		/// <summary>
		/// Converts a <see cref="TreeNode"/> to its model <typeparamref name="T"/> equivalent.
		/// </summary>
		public static T ToModel<T>(this TreeNode node)
			where T : PageNode, new()
		{
			if (node == null)
			{
				return null;
			}


			return CreateModel<T>(node);
		}


		/// <summary>
		/// Converts a <see cref="TreeNode"/> to its <see cref="PageNode"/> model equivalent.
		/// </summary>
		public static PageNode ToPageNode(this TreeNode node)
		{
			if (node == null)
			{
				return null;
			}


			return CreateModel<PageNode>(node);
		}
	

		private static T CreateModel<T>(TreeNode node)
			where T : PageNode, new()
		{
			T model = new T
			{
				AclID = node.NodeACLID,
				DatePublished = node.DocumentLastPublished,
				DocumentID = node.DocumentID,
				DocumentModifiedWhen = node.DocumentModifiedWhen,
				DocumentName = node.DocumentName,
				DocumentUrlPath = node.DocumentCustomData[ Constants.DocumentUrlPath ]?.ToString(),
				DocumentCulture = node.DocumentCulture,
				NodeAliasPath = node.NodeAliasPath,
				NodeClassName = node.NodeClassName,
				NodeGuid = node.NodeGUID,
				NodeID = node.NodeID,
				NodeLevel = node.NodeLevel,
				NodeOrder = node.NodeOrder,
				NodeParentID = node.NodeParentID,
				NodeSiteID = node.NodeSiteID
			};


			// Fill metadata
			model.Metadata = new PageMetadata
			{
				Description = node.DocumentPageDescription,
				Title = node.DocumentPageTitle,
				Url = $"{node.Site.SitePresentationURL.TrimEnd('/')}{model.DocumentUrlPath}",
			};


			// Fill custom properties
			model.Fields = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);

			// Fill document custom data
			model.CustomData = node.DocumentCustomData.ConvertToHashtable();

			// Fill preview
			var previewTitle = node.DocumentCustomData.GetStringValue(nameof(Preview.PreviewTitle));
			var previewText = node.DocumentCustomData.GetStringValue(nameof(Preview.PreviewText));
			var previewImage = node.DocumentCustomData.GetStringValue(nameof(Preview.PreviewImage));
			var previewCtaLabel = node.DocumentCustomData.GetStringValue(nameof(Preview.PreviewCtaLabel));
			var previewUrl = node.DocumentCustomData.GetStringValue(nameof(Preview.PreviewUrl));
			var previewDate = node.DocumentCustomData.GetDateTimeValue(nameof(Preview.PreviewDate));
			var previewNavigationLabel = node.DocumentCustomData.GetStringValue(nameof(Preview.PreviewNavigationLabel));
			
			model.Preview = new Preview()
			{
				PreviewTitle = previewTitle,
				PreviewText = previewText,
				PreviewImage = previewImage,
				PreviewCtaLabel = previewCtaLabel,
				PreviewUrl = previewUrl,
				PreviewDate = previewDate,
				PreviewNavigationLabel = previewNavigationLabel
			};

			// Fill Categories
			var categoryDisplayNames = node.DocumentCustomData.GetStringValue(nameof(PageNode.CategoryDisplayNames));
			var categoryNames = node.DocumentCustomData.GetStringValue(nameof(PageNode.CategoryNames));
			var categoryCodeNamePaths = node.DocumentCustomData.GetStringValue(nameof(PageNode.CategoryCodeNamePaths));

			model.CategoryDisplayNames = categoryDisplayNames.Split('|').Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
			model.CategoryNames = categoryNames.Split('|').Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
			model.CategoryCodeNamePaths = categoryCodeNamePaths.Split('|').Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

			// Fill Page Builder
			model.PageBuilderWidgets = node.GetPageBuilderWidgets();

			// Note: Providing actual type conversions performs slightly better than the default reflection path
			switch (node)
			{
				case Home customNode:
					FillFields(model.Fields, customNode.Fields);
					break;


				default:
					FillFields(model.Fields, node);
					break;
			}

			// Fill properties that are not from the PageNode Model
			FillProperties(model);


			return model;
		}
		

		private static void FillFields(Dictionary<string, object> dictionary, IHierarchicalObject container)
		{
			foreach (string field in container.Properties.Except(defaultFields))
			{
				dictionary.Add(field, container[field]);
			}
		}

		private static void FillProperties<T>(T model)
			where T : PageNode
		{
			// Custom Logic to check if the Node is Content Only
			// Content Only meaning that this Node should not resolve // return a 404
			// This is not the same as TreeNode.NodeIsContentOnly
			// IsContentOnly is a defined field on the BasePageFields Inherited Model			
			model.IsContentOnly = model.Fields.GetBoolValue(nameof(BasePage.IsContentOnly));

			// Rel Canonical Data is part of the fields and must be added to the metadata here			
			model.Metadata.CanonicalUrl = model.Fields.GetStringValue(nameof(BasePage.RelCanonical));


			// MobileWebAppTitle
			model.Metadata.MobileWebAppTitle = model.Fields.GetStringValue(nameof(BasePage.MobileWebAppTitle));

			// Social Share Fields
			model.Metadata.OgTitle = model.Fields.GetStringValue(nameof(BasePage.OgTitle));
			model.Metadata.OgDescription = model.Fields.GetStringValue(nameof(BasePage.OgDescription));
			model.Metadata.OgImage = model.Fields.GetStringValue(nameof(BasePage.OgImage));
			model.Metadata.OgUrl = model.Fields.GetStringValue(nameof(BasePage.OgUrl));
			model.Metadata.TwitterCard = model.Fields.GetStringValue(nameof(BasePage.TwitterCard));
			model.Metadata.TwitterSite = model.Fields.GetStringValue(nameof(BasePage.TwitterSite));
			model.Metadata.TwitterCreator = model.Fields.GetStringValue(nameof(BasePage.TwitterCreator));
			model.Metadata.Schema = model.Fields.GetStringValue(nameof(BasePage.Schema));
		}

		public static PageBuilderWidgets GetPageBuilderWidgets(this TreeNode node)
        {
            var pageBuilderWidgetsJson = node["DocumentPageBuilderWidgets"];

            return PageBuilderWidgetsToModel(pageBuilderWidgetsJson);
        }

		public static PageBuilderWidgets GetOriginalPageBuilderWidgets(this TreeNode node)
        {
            var pageBuilderWidgetsJson = node.GetOriginalValue("DocumentPageBuilderWidgets");

            return PageBuilderWidgetsToModel(pageBuilderWidgetsJson);
        }

        private static PageBuilderWidgets PageBuilderWidgetsToModel(object pageBuilderWidgetsJson)
        {
            if (pageBuilderWidgetsJson != null)
            {
                var pageBuilderWidgets = JsonConvert.DeserializeObject<PageBuilderWidgets>(pageBuilderWidgetsJson.ToString());
                foreach (var editableAreas in pageBuilderWidgets.EditableAreas)
                {
                    foreach (var section in editableAreas.Sections)
                    {
                        section.PropertiesDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonConvert.SerializeObject(section.Properties));
                        foreach (var zone in section.Zones)
                        {
                            foreach (var widget in zone.Widgets)
                            {
                                foreach (var variant in widget.Variants)
                                {
                                    variant.PropertiesDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonConvert.SerializeObject(variant.Properties));
                                }
                            }
                        }
                    }
                }

                return pageBuilderWidgets;
            }

            return null;
        }
    }
}
