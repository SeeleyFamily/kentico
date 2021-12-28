using CMS.DocumentEngine;
using CMS.Helpers;
using CMS.Membership;
using Common.Migration.Infrastructure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Common.Migration.MovePageNode
{
	public class Program : BaseProgram
	{
		static void Main(string[] args)
		{
			var consoleApp = new Program();
			consoleApp.Main();
		}

		public Program() : base()
		{
		}

		public override void RunConsoleApp()
		{
			try
			{
				// Console App Business Logic Here;
				MovePageNodes();

			}
			catch (Exception e)
			{
				Messages.Add($"Error: {e.Message}");
			}
		}

		private static void MovePageNodes()
		{
			var siteId = MigrationUtilities.GetSiteId();
			List<string> ErrorMessages = new List<string>();

			string movePageNodeParentNode = ConfigurationManager.AppSettings["MovePageNodeParentNode"];
			string movePageNodeNodes = ConfigurationManager.AppSettings["MovePageNodeNodes"];
			if (!string.IsNullOrWhiteSpace(movePageNodeParentNode) && !string.IsNullOrWhiteSpace(movePageNodeNodes))
			{
				var pageNodes = movePageNodeNodes.Split(',').Select(x => int.Parse(x)).ToList();
				var parentPageNode = int.Parse(movePageNodeParentNode);
				MovePageNodes(parentPageNode, pageNodes);

				return;
			}

			string movePageNodeCreatePlaceholderSetting = ConfigurationManager.AppSettings["MovePageNodeCreatePlaceholder"];
			string movePageNodePlaceholderClassName = ConfigurationManager.AppSettings["MovePageNodePlaceholderClassName"];
			string movePageNodeNodeAliasPath = ConfigurationManager.AppSettings["MovePageNodeNodeAliasPath"];

			bool movePageNodeCreatePlaceholder = false;
			if (!string.IsNullOrWhiteSpace(movePageNodeCreatePlaceholderSetting))
			{
				bool.TryParse(ConfigurationManager.AppSettings["MovePageNodeCreatePlaceholder"], out movePageNodeCreatePlaceholder);
			}

			if (movePageNodeCreatePlaceholder && string.IsNullOrWhiteSpace(movePageNodePlaceholderClassName))
			{
				throw new Exception("Placeholder Class Name can not be empty if create placeholder is set to true");
			}


			TreeProvider tree = new TreeProvider(MembershipContext.AuthenticatedUser);

			var treeNodes = DocumentHelper.GetDocuments()
				.OnSite(siteId, true)
				.WithCoupledColumns()
				.Columns(new string[]
				{
					nameof(TreeNode.DocumentID),
					nameof(TreeNode.NodeID),
					nameof(TreeNode.NodeAliasPath),
					//nameof(TreeNode.DocumentUrlPath),
					"NewNodeAliasPath"
				})
				.Path((!string.IsNullOrWhiteSpace(movePageNodeNodeAliasPath) ? movePageNodeNodeAliasPath : "/"), PathTypeEnum.Children)
				.Where(x => x["NewNodeAliasPath"] != null && !string.IsNullOrWhiteSpace(x["NewNodeAliasPath"].ToString()))
				.OrderBy(x => x["NewNodeAliasPath"].ToString().Length)
				.ToList();

			if (!string.IsNullOrWhiteSpace(movePageNodeNodes))
			{
				var pageNodes = movePageNodeNodes.Split(',').Select(x => int.Parse(x)).ToList();
				treeNodes = DocumentHelper.GetDocuments()
					.OnSite(siteId, true)
					.WithCoupledColumns()
					.Columns(new string[]
					{
						nameof(TreeNode.DocumentID),
						nameof(TreeNode.NodeID),
						nameof(TreeNode.NodeAliasPath),
						//nameof(TreeNode.DocumentUrlPath),
						"NewNodeAliasPath"
					})
					.Where(x => pageNodes.Contains(x.NodeID)).ToList();
			}


			var rootNode = DocumentHelper.GetDocuments().OnSite(siteId, true).Where(x => x.ClassName == "CMS.Root").FirstOrDefault();
			foreach (var node in treeNodes)
			{
				try
				{
					var newNodeAliasPath = node["NewNodeAliasPath"].ToString();
					if (string.IsNullOrWhiteSpace(newNodeAliasPath))
					{
						throw new Exception("New Node Alias Path is Null or Whitespace");
					}
					// CHECK IF NODE EXISTS ALREADY
					var existingNode = DocumentHelper.GetDocuments()
						.OnSite(siteId, true)
						.Path(newNodeAliasPath, PathTypeEnum.Single)
						.ToList();
					if (existingNode.Any())
					{
						throw new Exception("Node already exists");
					}


					var currentNodeAliasPath = node.NodeAliasPath;
					var currentDocumentUrlPath = node.DocumentCustomData[ "DocumentUrlPath" ]?.ToString();
					if (!newNodeAliasPath.Equals(currentNodeAliasPath))
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
				Console.WriteLine($"There were {ErrorMessages.Count()} errors.");
				Console.WriteLine(ErrorMessages.Join("\n"));
			}

		}

		private static void MovePageNodes(int parentPageNode, List<int> pageNodes)
		{
			TreeProvider tree = new TreeProvider(MembershipContext.AuthenticatedUser);

			var parentNode = DocumentHelper.GetDocument(parentPageNode, "en-US", tree);
			if (parentNode != null)
			{
				var nodes = DocumentHelper.GetDocuments().Where(x => pageNodes.Contains(x.NodeID)).ToList();
				foreach (var node in nodes)
				{
					try
					{
						DocumentHelper.MoveDocument(node, parentNode, tree, true);
					}
					catch (Exception)
					{

					}
				}
			}
		}
	}
}
