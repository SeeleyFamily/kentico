using CMS.DataEngine;
using CMS.DocumentEngine;
using CMS.Helpers;
using CMS.SiteProvider;
using Launchpad.Core.Extensions;
using Launchpad.Infrastructure.Extensions;
using Launchpad.Infrastructure.Services;
using System.Collections.Generic;
using System.Linq;

namespace Launchpad.Infrastructure.Kentico.CMS.Services
{
	public class DocumentUrlPathModuleService
	{
		#region fields
		private readonly CustomCmsModuleLoggingService customCmsModuleLoggingService;
		#endregion

		public DocumentUrlPathModuleService()
		{
			this.customCmsModuleLoggingService = new CustomCmsModuleLoggingService();
		}

		public void InsertBefore(object sender, DocumentEventArgs e)
		{
			customCmsModuleLoggingService.LogInformation("DocumentUrlPathModuleService", "InsertBefore");
			HandleDocumentUrlPath(e.Node, e.TreeProvider);
		}

		public void InsertAfter(object sender, DocumentEventArgs e)
		{
			customCmsModuleLoggingService.LogInformation("DocumentUrlPathModuleService", "InsertAfter");
			CheckChildDocumentUrlPath(e.Node, e.TreeProvider);
		}

		public void UpdateBefore(object sender, DocumentEventArgs e)
		{
			customCmsModuleLoggingService.LogInformation("DocumentUrlPathModuleService", "UpdateBefore");
			HandleDocumentUrlPath(e.Node, e.TreeProvider);
		}

		public void UpdateAfter(object sender, DocumentEventArgs e)
		{
			customCmsModuleLoggingService.LogInformation("DocumentUrlPathModuleService", "UpdateAfter");
			CheckChildDocumentUrlPath(e.Node, e.TreeProvider);
		}

		public void PublishBefore(object sender, WorkflowEventArgs e)
		{
			customCmsModuleLoggingService.LogInformation("DocumentUrlPathModuleService", "PublishBefore");
			HandleDocumentUrlPath(e.Document, e.TreeProvider);
		}

		public void PublishAfter(object sender, WorkflowEventArgs e)
		{
			customCmsModuleLoggingService.LogInformation("DocumentUrlPathModuleService", "PublishAfter");
			CheckChildDocumentUrlPath(e.Document, e.TreeProvider);
		}

		public void HandleDocumentUrlPath(TreeNode node, TreeProvider tree, bool forceUpdate = false)
		{
			// Added isContentOnly Check to set the DocumentUrlPath to prevent it from appearing in the Sitemap.xml query
			bool isContentOnly = false;
			var isContentOnlyValue = node["IsContentOnly"];
			if (isContentOnlyValue != null)
			{
				bool.TryParse(node["IsContentOnly"].ToString(), out isContentOnly);
			}

			var dataClass = DataClassInfoProvider.GetDataClassInfo(node.ClassName);
			bool hasClassUrlPattern = false;
			if (dataClass != null && !string.IsNullOrWhiteSpace(dataClass.ClassURLPattern))
			{
				hasClassUrlPattern = true;
			}

			string originalPath = node.DocumentCustomData[Core.Constants.Constants.DocumentUrlPath]?.ToString();
			if (originalPath == null)
			{
				originalPath = ""; // we want to keep this assignemnt because of a comparison later where null != "" and forces an update which is unnecessary
			}
			string finalPath = ""; // we want to keep this assignment to ensure that it is initialized as an empty string for a comparison later

			if (isContentOnly || !hasClassUrlPattern)
			{
				finalPath = "";
			}
			else
			{

				var urlPart = node.NodeAlias.ToLower();
				if (string.IsNullOrWhiteSpace(urlPart))
				{
					urlPart = TreePathUtils.GetUniqueNodeAlias(node.DocumentName, SiteContext.CurrentSiteName, node.NodeParentID, node.NodeID).ToLower();
				}
				List<string> urlParts = new List<string> { urlPart };
				TreeNode parent = GetParent(node);
				while (parent != null && !parent.IsRoot())
				{
					urlParts.Add(parent.NodeAlias);
					parent = GetParent(parent);
				}

				urlParts.Reverse();

				var culture = CultureSiteInfoProvider.GetSiteCultures(SiteContext.CurrentSiteName).Items.Where(x => x.CultureCode == node.DocumentCulture).FirstOrDefault();
				if (culture != null)
				{
					if (!culture.CultureAlias.IsNullOrEmpty())
					{
						finalPath = culture.CultureAlias + "/" + urlParts.Join("/").ToLower();

					}
					else
					{
						finalPath = "/" + urlParts.Join("/").ToLower();

					}
				}
				else
				{
					finalPath = "/" + urlParts.Join("/").ToLower();
				}

			}

			if (originalPath != finalPath)
			{
				node.DocumentCustomData.SetValue(Core.Constants.Constants.DocumentUrlPath, finalPath);
				if (node.NodeHasChildren)
				{
					node.DocumentCustomData.SetValue(Core.Constants.Constants.DocumentUrlPathUpdateChildren, true);
				}

				if (forceUpdate)
				{
					node.Update();
					// forceUpdate only needed in child pages
					if (node.WorkflowStep != null)
					{
						DocumentSynchronizationHelper.LogDocumentChange(node, TaskTypeEnum.UpdateDocument, tree);
					}
				}
			}
		}

		public void CheckChildDocumentUrlPath(TreeNode node, TreeProvider tree)
		{
			// This will actually run on the node and its immediate children no matter what.
			// If a url change is detected, then it will continue down the children nodes via updates event triggers..
			if (node.NodeHasChildren)
			{
				var documentUrlPathUpdateChildren = node.DocumentCustomData.GetBooleanValue(Core.Constants.Constants.DocumentUrlPathUpdateChildren);
				if (documentUrlPathUpdateChildren)
				{
					node.DocumentCustomData.SetValue(Core.Constants.Constants.DocumentUrlPathUpdateChildren, false);
					node.Update();

					// run this same update on all children
					IEnumerable<TreeNode> children = DocumentHelper.GetDocuments().WithCoupledColumns()
															.WhereEquals("NodeParentID", node.NodeID)
															.ToList();
					foreach (TreeNode child in children)
					{
						HandleDocumentUrlPath(child, tree, true);
					}
				}
			}
		}

		public TreeNode GetParent(TreeNode node)
		{
			TreeNode parent = DocumentHelper.GetDocuments()
											.TopN(1)
											.WhereEquals("NodeID", node.NodeParentID)
											.FirstOrDefault();
			if (parent == null)
			{
				return null;
			}
			if (/*parent.IsFolder() ||*/ parent.ClassName.ToLower().Contains("folder")) // K13: IsFolder extension no longer exists
			{
				return GetParent(parent);
			}
			if (parent.IsRoot())
			{
				return null;
			}
			return parent;
		}

	}
}
