using CMS.Core;
using CMS.DocumentEngine;
using CMS.Helpers;
using CMS.SiteProvider;
using CMSAppCustom.Models;
using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Core.Configuration;
using Launchpad.Core.Constants;
using Launchpad.Core.Extensions;
using Launchpad.Core.Models;
using Launchpad.Core.Utilities;
using Launchpad.Infrastructure.Extensions;
using Launchpad.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Launchpad.Infrastructure.Kentico.DocumentCustomData.Services
{
	public class DocumentCustomDataModuleService
	{
		#region Properties
		public List<string> PreviewablePageTypes { get; set; } = new List<string>();
		public string PreviewablePageTypesString { get; set; }
		#endregion

		private CategoryService _CategoryService
		{
			get
			{
				var siteContextConfiguration = new SiteContextConfiguration()
				{
					SiteId = SiteContext.CurrentSiteID,
					SiteName = SiteContext.CurrentSiteName
				};

				var cacheConfiguration = new Lazy<ICacheConfiguration>(() => new CacheConfiguration());
				var cacheService = new CacheService(cacheConfiguration);

				return new CategoryService(cacheService, siteContextConfiguration);
			}
		}

		private CommonDocumentCustomDataModuleService commonDocumentCustomDataModuleService;
		private CustomDocumentCustomDataModuleService customDocumentCustomDataModuleService;

		public DocumentCustomDataModuleService()
		{
			commonDocumentCustomDataModuleService = new CommonDocumentCustomDataModuleService();
			customDocumentCustomDataModuleService = new CustomDocumentCustomDataModuleService();

			var previewablePageTypesAppSetting = ConfigurationManager.AppSettings.GetStringValue("PreviewablePageTypes");
			PreviewablePageTypesString = previewablePageTypesAppSetting;
			if (!string.IsNullOrWhiteSpace(previewablePageTypesAppSetting))
			{
				PreviewablePageTypes = previewablePageTypesAppSetting.Split(',').ToList();
			}
		}


		internal void UpdateAfterEventHandler(object sender, DocumentEventArgs e)
		{
			if (e.Node != null)
			{
				var workflow = e.Node.GetWorkflow();
				if (workflow == null)
				{
					UpdateDocumentCustomDataEvent(e.Node);
				}
			}
		}

		internal void InsertAfterEventHandler(object sender, DocumentEventArgs e)
		{
			UpdateDocumentCustomDataEvent(e.Node);
		}

		internal void CheckInAfterEventHandler(object sender, WorkflowEventArgs e)
		{
			UpdateDocumentCustomDataEvent(e.Document);
		}

		internal void PublishBeforeEventHandler(object sender, WorkflowEventArgs e)
		{
			UpdateDocumentCustomDataEvent(e.Document);
		}

		internal void UpdateDocumentCustomData()
		{
			var previewableDocumentQuery = DocumentHelper.GetDocuments().OnCurrentSite().Columns("NodeID");
			var classNames = PreviewablePageTypesString;
			if (!string.IsNullOrWhiteSpace(classNames))
			{
				var classNamesList = classNames.Split(',').Join("','");
				previewableDocumentQuery = previewableDocumentQuery.Where($"ClassName in ('{ classNamesList }')");
			}
			var previewableNodes = previewableDocumentQuery.ToList();
			foreach (var node in previewableNodes)
			{
				var nodeWithColumns = DocumentHelper.GetDocuments().WithCoupledColumns()
														.WhereEquals("NodeID", node.NodeID)
														.TopN(1)
														.FirstOrDefault();
				UpdateDocumentCustomDataEvent(nodeWithColumns);
			}
		}

		public void UpdateDocumentCustomDataEvent(TreeNode node)
		{
			if (!AllowUpdateDocumentCustomData(node))
			{
				return;
			}

			bool doUpdate = false;

			try
			{
				CustomDataObject customDataObject = new CustomDataObject();

				// Categories
				var categoryNames = "";
				var categoryDisplayNames = "";
				var categoryCodeNamePaths = "";
				//var categories = _CategoryService.GetDocumentCategories(node.DocumentID); 
				// updated to not use all cache which would be a huge performance hit
				// special case - this is because this handler occurs on save which would cache clear anyway				
				var categories = _CategoryService.GetDocumentCategoriesWithoutAllCache(node.DocumentID);
				if (!categories.IsNullOrEmpty())
				{
					categoryNames = string.Join("|", categories.Select(x => x.CodeName));
					categoryDisplayNames = string.Join("|", categories.Select(x => x.DisplayName));
					categoryCodeNamePaths = string.Join("|", categories.Select(x => x.CodeNamePath));
				}

				var previewTitle = node.GetStringValue(nameof(Preview.PreviewTitle), "");
				var previewText = node.GetStringValue(nameof(Preview.PreviewText), "");
				var previewImage = node.GetStringValue(nameof(Preview.PreviewImage), "");
				var previewCtaLabel = node.GetStringValue(nameof(Preview.PreviewCtaLabel), "");
				var hidePreviewText = node.GetBooleanValue(nameof(Preview.HidePreviewText), false);
				var previewNavigationLabel = node.GetStringValue(nameof(Preview.PreviewNavigationLabel), "");


				var previewUrl = string.Empty;
				DateTime? previewDate = null;

				var searchBlobValues = new List<string>();

				/// Breadcrumbs are seperated by pipe symbol(|)
				/// Item left to colon (:) is document name and Item right to colon(:) is document url path
				var breadcrumbs = GetBreadcrumbs(node);

				// POPULATE MODEL
				customDataObject.CategoryNames = categoryNames;
				customDataObject.CategoryDisplayNames = categoryDisplayNames;
				customDataObject.CategoryCodeNamePaths = categoryCodeNamePaths;

				customDataObject.Preview = new Preview();
				customDataObject.Preview.PreviewTitle = previewTitle;
				customDataObject.Preview.PreviewText = previewText;
				customDataObject.Preview.PreviewImage = previewImage;
				customDataObject.Preview.PreviewCtaLabel = previewCtaLabel;
				customDataObject.Preview.HidePreviewText = hidePreviewText;
				customDataObject.Preview.PreviewUrl = previewUrl;
				customDataObject.Preview.PreviewDate = previewDate;
				customDataObject.Preview.PreviewNavigationLabel = previewNavigationLabel;

				customDataObject.SearchBlobValues = searchBlobValues;

				customDataObject.Breadcrumbs = breadcrumbs;


				// POPULATE COMMON NAME SPACE TYPES
				doUpdate = commonDocumentCustomDataModuleService.UpdateCommonDocumentCustomData(ref node, ref customDataObject) || doUpdate;
				// POPULATE CUSTOM NAME SPACE TYPES
				doUpdate = customDocumentCustomDataModuleService.UpdateCustomDocumentCustomData(ref node, ref customDataObject) || doUpdate;

				// EXTRACT MODEL
				categoryNames = customDataObject.CategoryNames;
				categoryDisplayNames = customDataObject.CategoryDisplayNames;
				categoryCodeNamePaths = customDataObject.CategoryCodeNamePaths;
				previewTitle = customDataObject.Preview.PreviewTitle;
				previewText = customDataObject.Preview.PreviewText;
				previewImage = customDataObject.Preview.PreviewImage;
				previewCtaLabel = customDataObject.Preview.PreviewCtaLabel;
				hidePreviewText = customDataObject.Preview.HidePreviewText;
				previewNavigationLabel = customDataObject.Preview.PreviewNavigationLabel;
				previewUrl = customDataObject.Preview.PreviewUrl;
				previewDate = customDataObject.Preview.PreviewDate;
				searchBlobValues = customDataObject.SearchBlobValues;

				// DEFAULTS
				//
				//

				previewTitle = !string.IsNullOrWhiteSpace(previewTitle) ? previewTitle : node.DocumentName;
				//previewUrl = !string.IsNullOrWhiteSpace(previewUrl) ? previewUrl : node.DocumentCustomData[Constants.DocumentUrlPath]?.ToString();
				// this was causing doUpdate to be true every time by comparing null to empty string
				previewUrl = CoalesceUtility.CoalesceWithoutWhitespace(previewUrl, node.DocumentCustomData[Constants.DocumentUrlPath]?.ToString(), string.Empty);

				previewCtaLabel = !string.IsNullOrWhiteSpace(previewCtaLabel) ? previewCtaLabel : LabelConstants.ReadMore;

				// Search Blob
				searchBlobValues.AddRange(
						new List<string>
						{
								previewTitle,
								previewText,
								previewCtaLabel,
						}
				);
				if (!categories.IsNullOrEmpty())
				{
					searchBlobValues.AddRange(categories.Select(x => x.CodeNamePath));
					searchBlobValues.AddRange(categories.Select(x => x.DisplayNamePath));
				}
				var searchBlob = GetSearchBlob(searchBlobValues);


				// UPDATE
				//
				//

				// CATEGORIES
				//				
				doUpdate = node.DocumentCustomData.UpdateCustomDataStringValue(nameof(PageNode.CategoryNames), categoryNames) || doUpdate;
				doUpdate = node.DocumentCustomData.UpdateCustomDataStringValue(nameof(PageNode.CategoryDisplayNames), categoryDisplayNames) || doUpdate;
				doUpdate = node.DocumentCustomData.UpdateCustomDataStringValue(nameof(PageNode.CategoryCodeNamePaths), categoryCodeNamePaths) || doUpdate;

				// PREVIEW
				//
				// Preview Title				
				doUpdate = node.DocumentCustomData.UpdateCustomDataStringValue(nameof(Preview.PreviewTitle), previewTitle) || doUpdate;
				// Preview Text
				doUpdate = node.DocumentCustomData.UpdateCustomDataStringValue(nameof(Preview.PreviewText), previewText) || doUpdate;
				// Hide Preview Text
				doUpdate = node.DocumentCustomData.UpdateCustomDataBoolValue(nameof(Preview.HidePreviewText), hidePreviewText) || doUpdate;
				// Preview Image
				doUpdate = node.DocumentCustomData.UpdateCustomDataStringValue(nameof(Preview.PreviewImage), previewImage) || doUpdate;
				// Preview Cta Label
				doUpdate = node.DocumentCustomData.UpdateCustomDataStringValue(nameof(Preview.PreviewCtaLabel), previewCtaLabel) || doUpdate;
				// Preview Url
				doUpdate = node.DocumentCustomData.UpdateCustomDataStringValue(nameof(Preview.PreviewUrl), previewUrl) || doUpdate;
				// Preview Date				
				doUpdate = node.DocumentCustomData.UpdateCustomDataDateTimeValue(nameof(Preview.PreviewDate), previewDate) || doUpdate;
				// Preview Navigation Label				
				doUpdate = node.DocumentCustomData.UpdateCustomDataStringValue(nameof(Preview.PreviewNavigationLabel), previewNavigationLabel) || doUpdate;

				// Search Blob
				doUpdate = node.DocumentCustomData.UpdateCustomDataStringValue(Constants.DocumentCustomDataSearchBlobKey, searchBlob) || doUpdate;

				//Breadcrumbs
				doUpdate = node.DocumentCustomData.UpdateCustomDataStringValue(nameof(Breadcrumbs), breadcrumbs) || doUpdate;

				// Force No Update
				var forceNoUpdate = node.DocumentCustomData.GetBooleanValue(Constants.DocumentCustomDataForceNoUpdateKey);
				doUpdate = doUpdate && !forceNoUpdate;

				// Finally Update
				//
				if (doUpdate)
				{
					node.SubmitChanges(true);
				}
			}
			catch (Exception e)
			{
				Service.Resolve<IEventLogService>().LogInformation("UpdateDocumentCustomDataEvent", $"DocumentId: {node.DocumentID}", e.Message + "\n" + e.StackTrace);
			}
		}

		public string GetSearchBlob(IEnumerable<string> searchBlobs)
		{
			// strip html and lower
			var searchBlob = string.Join(" ", searchBlobs.Select(x => x.StripHTML())).ToLower();

			// replace non digit characters
			var charArray = searchBlob.ToCharArray();
			for (int i = 0; i < charArray.Length; i++)
			{
				var currentChar = charArray[i];
				if (!char.IsLetterOrDigit(currentChar) && !char.IsWhiteSpace(currentChar))
				{
					charArray[i] = ' ';
				}
			}
			searchBlob = new string(charArray);

			IEnumerable<string> terms = searchBlob.Split(' ');
			terms = terms.Select(x => x.Trim()).Where(x => x != string.Empty);
			terms = terms.Distinct();
			// TODO REMOVE STOP WORDS HERE
			terms = terms.OrderBy(x => x);
			return string.Join(" ", terms);
		}

		private bool AllowUpdateDocumentCustomData(TreeNode node)
		{
			bool allowUpdate = true;

			// Prevent Containers From Happening			
			var documentForeignKeyValue = node.GetIntegerValue(Constants.DocumentForeignKeyValueColumnName, int.MinValue);
			if (documentForeignKeyValue == int.MinValue)
			{
				allowUpdate = false;
			}
			return allowUpdate;
		}

		/// <summary>
		/// Breadcrumbs are seperated by pipe symbol(|)
		/// Item left to colon (:) is document name and Item right to colon(:) is document url path
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public string GetBreadcrumbs( TreeNode node )
		{
			string breadcrumbsKvpFormat = string.Empty;

			List<PageNode> breadcrumbs = new List<PageNode>
			{
				new PageNode
				{
					DocumentName = "Home",
					DocumentUrlPath = "/",
					NodeAliasPath = "/",
				}
			};

			// following code may be redundent
			/*
			// Columns
			string[] columns = new string[] { "V.DocumentID", "V.DocumentName", "V.NodeAliasPath", "V.NodeID", "V.NodeGUID", "V.NodeLevel", "V.NodeOrder", "V.NodeParentID", "V.NodeSiteID" };

			// Get the anchor node
			TreeNode anchor = DocumentHelper.GetDocuments()
											.TopN(1)
											.Columns(columns)
											.WhereEquals("NodeID", node.NodeID)
											.FirstOrDefault();

			if (anchor == null)
			{
				return string.Empty;
			}
			*/

			if( node.NodeAliasPath != "/home" )
			{
				// Return the documents on path converted to page nodes
				var crumbs = node.DocumentsOnPath
								   .Where( n => n.HasUrl() && n.NodeID != node.NodeID )
								   .Select( n => n.ToPageNode() )
								   .ToArray();

				if( crumbs != null && crumbs.Any() )
				{
					// Add the rest of the breadcrumbs
					breadcrumbs.AddRange( crumbs );
				}


				// Add the node itself as the final breadcrumb
				breadcrumbs.Add( node.ToPageNode() );
			}

			List<string> breadcrumbsKVP = new List<string>();

			if( breadcrumbs != null && breadcrumbs.Count() > 2 )
			{
				foreach( var item in breadcrumbs )
				{

					breadcrumbsKVP.Add( $"{item.DocumentName}:{item.DocumentUrlPath}" );
				}
			}

			return string.Join( "|", breadcrumbsKVP ) ?? string.Empty;

		}
	}
}
