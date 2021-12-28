using CMS.Core;
using CMS.DataEngine;
using CMS.DocumentEngine;
using CMS.Helpers;
using CMS.Membership;
using Launchpad.Core.Constants;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Launchpad.Infrastructure.Kentico.CMS.Services
{
	public class PageNodesModuleService
	{
		private List<string> ErrorMessages { get; set; } = new List<string>();

		public PageNodesModuleService()
		{

		}
		public void MovePageNodes()
		{
			bool movePageNodeCreatePlaceholder = SettingsKeyInfoProvider.GetBoolValue("MovePageNodeCreatePlaceholder");
			string movePageNodePlaceholderClassName = SettingsKeyInfoProvider.GetValue("MovePageNodePlaceholderClassName");
			if (movePageNodeCreatePlaceholder && string.IsNullOrWhiteSpace(movePageNodePlaceholderClassName))
			{
				throw new Exception("Placeholder Class Name can not be empty if create placeholder is set to true");
			}
			string movePageNodeNodeAliasPath = SettingsKeyInfoProvider.GetValue("MovePageNodeNodeAliasPath");


			TreeProvider tree = new TreeProvider(MembershipContext.AuthenticatedUser);

			var treeNodes = DocumentHelper.GetDocuments()
				.OnCurrentSite()
				.WithCoupledColumns()
				.Columns(new string[]
				{
					nameof(TreeNode.DocumentID),
					nameof(TreeNode.NodeID),
					nameof(TreeNode.NodeAliasPath),
					//nameof(TreeNode.DocumentUrlPath),  // K13 TODO: Test that this returns custom data
					"NewNodeAliasPath"
				})
				.Path((!string.IsNullOrWhiteSpace(movePageNodeNodeAliasPath) ? movePageNodeNodeAliasPath : "/"), PathTypeEnum.Children)
				.Where(x => x["NewNodeAliasPath"] != null && !string.IsNullOrWhiteSpace(x["NewNodeAliasPath"].ToString()))
				.OrderBy(x => x["NewNodeAliasPath"].ToString().Length)
				.ToList();

			var rootNode = DocumentHelper.GetDocuments().OnCurrentSite().Where(x => x.ClassName == "CMS.Root").FirstOrDefault();
			foreach (var node in treeNodes)
			{
				try
				{
					var newNodeAliasPath = node["NewNodeAliasPath"].ToString();
					var currentNodeAliasPath = node.NodeAliasPath;
					var currentDocumentUrlPath = node.DocumentCustomData[Constants.DocumentUrlPath]?.ToString();
					if (!newNodeAliasPath.Equals(currentNodeAliasPath) && !newNodeAliasPath.Equals(currentDocumentUrlPath))
					{
						var currNode = DocumentHelper.GetDocument(node.DocumentID, tree);
						if (currNode != null && currNode.NodeID == node.NodeID && currNode.DocumentID == node.DocumentID)
						{
							string[] NewNodeAliasPathList = newNodeAliasPath.ToString().Split('/');


							List<string> ListNewNodeAliasPath = NewNodeAliasPathList.ToList();
							ListNewNodeAliasPath.RemoveAll(x => x == "");

							string[] ArrPathsNewNodeAliasPath = ListNewNodeAliasPath.ToArray();
							var newNodeAlias = ArrPathsNewNodeAliasPath[ArrPathsNewNodeAliasPath.Count() - 1];
							ArrPathsNewNodeAliasPath = ArrPathsNewNodeAliasPath.Take(ArrPathsNewNodeAliasPath.Count() - 1).ToArray();

							if (ArrPathsNewNodeAliasPath.Length == 0)
							{
								// ROOT PAGE
								var targetNode = rootNode;
								if ((currNode != null) && (targetNode != null))
								{
									// Moves the page to the new location, including any child pages
									DocumentHelper.MoveDocument(currNode, targetNode, tree, true);
								}
							}
							else
							{
								var parentNodeAliasPath = ArrPathsNewNodeAliasPath.Join("/");
								var parentNode = tree.SelectNodes().Path("/" + parentNodeAliasPath);
								var targetNode = parentNode.FirstOrDefault();
								if ((currNode != null) && (targetNode != null))
								{
									// Moves the page to the new location, including any child pages
									DocumentHelper.MoveDocument(currNode, targetNode, tree, true);
								}

								if (targetNode == null && movePageNodeCreatePlaceholder)
								{
									for (var i = 0; i < ArrPathsNewNodeAliasPath.Length; i++)
									{
										var currLookupPath = ArrPathsNewNodeAliasPath.Take(i + 1).Join("/");
										var currLookupNode = tree.SelectNodes().Path("/" + currLookupPath).FirstOrDefault();
										var currLookupNodeParent = (i == 0) ? "" : ArrPathsNewNodeAliasPath.Take(i).Join("/");
										var currLookUpNodeParentNode = tree.SelectNodes().Path("/" + currLookupNodeParent).FirstOrDefault();
										if (currLookupNode != null)
										{
											continue;
										}
										else
										{
											// Create the placeholder node here...
											if (currLookUpNodeParentNode != null)
											{
												// Creates a new page of the "CMS.MenuItem" page type
												TreeNode newPage = TreeNode.New(movePageNodePlaceholderClassName, tree);
												var currLookupNodeAlias = ArrPathsNewNodeAliasPath[i];
												// Sets the properties of the new page
												newPage.DocumentName = currLookupNodeAlias;
												newPage.DocumentCulture = "en-us";
												newPage["MigrationType"] = "TEMP";

												// Inserts the new page as a child of the parent page
												newPage.Insert(currLookUpNodeParentNode);
											}
										}
									}

									// TRY AGAIN
									parentNode = tree.SelectNodes().Path("/" + parentNodeAliasPath);
									targetNode = parentNode.FirstOrDefault();
									if ((currNode != null) && (targetNode != null))
									{
										// Moves the page to the new location, including any child pages
										DocumentHelper.MoveDocument(currNode, targetNode, tree, true);
									}
								}
							}
							if (currNode.NodeAlias.ToLower() != newNodeAlias.ToLower())
							{
								currNode.NodeAlias = newNodeAlias;

								// DO SAVE
								currNode.Update();
							}
						}
					}
				}
				catch (Exception e)
				{
					ErrorMessages.Add($"Issue Updating Node: {node.NodeID}, Error: {e.Message}");
				}
			}

			if (ErrorMessages != null && ErrorMessages.Any())
			{
				Service.Resolve<IEventLogService>().LogInformation("PageNodesModuleService", "ErrorMessages", ErrorMessages.Join("\n"));
				throw new Exception($"There were {ErrorMessages.Count()} errors. See the event log for more details.");
			}
		}
	}
}
