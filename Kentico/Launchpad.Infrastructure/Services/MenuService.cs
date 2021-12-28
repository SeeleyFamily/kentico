using CMS.DocumentEngine;
using CMS.Helpers;
using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Abstractions.Specifications;
using Launchpad.Core.Constants;
using Launchpad.Core.Extensions;
using Launchpad.Core.Models;
using Launchpad.Core.Specifications;
using Launchpad.Core.Utilities;
using Launchpad.Infrastructure.Abstractions.Services;
using Launchpad.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using PageTypes = CMS.DocumentEngine.Types.Common;


namespace Launchpad.Infrastructure.Services
{

	public class MenuService : IMenuService, IPerScopeService
	{
		#region Constants
		private const string menuTypeFooter = "FooterNavigationMenu";
		private const string menuTypeNavigation = "GlobalNavigationMenu";
		private const string menuTypeSubFooter = "SubFooterNavigationMenu";
		private const string menuTypeUtility = "UtilityNavigationMenu";
		#endregion


		#region Fields
		private readonly ICacheService cacheService;
		protected readonly IDocumentService documentService;
		private readonly IDocumentQueryConfiguration queryConfiguration;
		#endregion


		public MenuService
		(
			ICacheService cacheService,
			IDocumentService documentService,
			IDocumentQueryConfiguration queryConfiguration
		)
		{
			this.cacheService = cacheService;
			this.documentService = documentService;
			this.queryConfiguration = queryConfiguration;
		}


		public virtual Breadcrumbs GetBreadcrumbs(int nodeId)
		{
			List<PageNode> breadcrumbs = new List<PageNode>
			{
				new PageNode
				{
					DocumentName = "Home",
					DocumentUrlPath = "/",
					NodeAliasPath = "/",
				}
			};

			IEnumerable<PageNode> additional = cacheService.GetFromCache((cs) =>
			{
				// Columns
				string[] columns = new string[] { "V.DocumentID", "V.DocumentName", "V.NodeAliasPath", "V.NodeID", "V.NodeGUID", "V.NodeLevel", "V.NodeOrder", "V.NodeParentID", "V.NodeSiteID", "V.DocumentCustomData" };

				// Get the anchor node
				TreeNode anchor = DocumentHelper.GetDocuments()
												.TopN(1)
												.Columns(columns)
												.WhereEquals("NodeID", nodeId)
												.FirstOrDefault();

				if (anchor == null)
				{
					return Enumerable.Empty<PageNode>();
				}


				// Return the documents on path converted to page nodes
				var crumbs = anchor.DocumentsOnPath
								   .Where(n => n.HasUrl() && n.ClassName != PageTypes.Home.CLASS_NAME)
								   .Select(n => n.ToPageNode())
								   .ToArray();

				return crumbs;
			},

			$"breadcrumbs|{nodeId}");

			if (additional != null && additional.Any())
			{
				// Add the rest of the breadcrumbs
				breadcrumbs.AddRange(additional);
			}

			Breadcrumbs result = new Breadcrumbs()
			{
				Items = breadcrumbs
			};
			return result;
		}


		public virtual IEnumerable<MenuItem> GetFooterMenu()
		{
			return GetMenu(menuTypeFooter);
		}


		public virtual IEnumerable<MenuItem> GetMenu(string menuType)
		{
			IEnumerable<MenuItem> menu = cacheService.GetFromRotatingCache(cs => CreateMenu(menuType, cs), $"menu|{menuType.ToLower()}|{queryConfiguration.Culture}", alwaysCache: true);

			return menu;
		}


		public virtual IEnumerable<MenuItem> GetMenu(Guid guid)
		{
			IEnumerable<MenuItem> menu = cacheService.GetFromRotatingCache(cs => CreateMenu(guid, cs), $"menu|{guid}");

			return menu;
		}


		public virtual IEnumerable<MenuItem> GetNavigationMenu()
		{
			return GetMenu(menuTypeNavigation);
		}


		public virtual IEnumerable<MenuItem> GetSideNavMenu(
			PageNode currentNode,
			int nestingLevel,
			string[] classNames = null,
			string[] excludeClassNames = null,
			string rootMenuClassName = null
			)
		{
			if (currentNode == null)
			{
				return null;
			}

			IDocumentSpecification documentSpecification = new DocumentSpecification
			{
				IncludeDocumentForPath = true,
				NestingLevel = nestingLevel,
				Sort = "NodelLevel, NodeOrder",
				ClassNames = classNames,
				ExcludedClassNames = excludeClassNames,
				NodeLevel = 0
			};

			string cacheKey = $"sidenav|{currentNode.NodeAliasPath}";


			return cacheService.GetFromRotatingCache(cs => CreateSideNavMenu(currentNode, cs, documentSpecification, rootMenuClassName), cacheKey, alwaysCache: true);
		}


		public virtual IEnumerable<MenuItem> GetSubFooterMenu()
		{
			return GetMenu(menuTypeSubFooter);
		}


		public virtual IEnumerable<MenuItem> GetUtilityMenu()
		{
			return GetMenu(menuTypeUtility);
		}


		protected virtual MenuItem Convert(TreeNode node, IEnumerable<TreeNode> nodes)
		{
			MenuItem menuItem = new MenuItem
			{
				Children = Convert(n => n.NodeParentID == node.NodeID, nodes).ToArray()
			};

			switch (node)
			{
				case PageTypes.MenuCard card:
					menuItem.CustomClass = card.CustomClass;
					menuItem.IsCard = true;
					menuItem.Image = card.Image.SanitizeMediaUrl();
					menuItem.ImagePosition = card.ImagePosition;
					menuItem.IsExternal = card.IsExternal;
					menuItem.Label = card.Label;
					menuItem.LabelMobile = card.LabelMobile;
					menuItem.Text = card.Text;
					menuItem.Url = card.LinkUrl;

					var cardPage = documentService.Get(card.Page);
					if (cardPage != null)
					{
						menuItem.Image = !string.IsNullOrWhiteSpace(menuItem.Image) ? menuItem.Image : cardPage.Preview.PreviewImage.SanitizeMediaUrl();
						// menuItem.IsExternal = menuItem.IsExternal ? menuItem.IsExternal : false; // This line doesn't actually do anything
						menuItem.Label = !string.IsNullOrWhiteSpace(menuItem.Label) ? menuItem.Label : cardPage.Preview.PreviewTitle;
						menuItem.Text = !string.IsNullOrWhiteSpace(menuItem.Text) ? menuItem.Text : cardPage.Preview.PreviewCtaLabel;
						menuItem.Url = !string.IsNullOrWhiteSpace(menuItem.Url) ? menuItem.Url : cardPage.Preview.PreviewUrl;
					}

					break;

				case PageTypes.MenuColumn column:
					menuItem.IsColumn = true;
					break;

				case PageTypes.MenuItem item:
					menuItem.IsExternal = item.IsExternal;
					menuItem.Label = item.Label;
					menuItem.LabelMobile = item.LabelMobile;
					menuItem.HideMobileOverviewLink = item.HideMobileOverviewLink;
					menuItem.Text = item.Label;
					menuItem.Url = item.LinkUrl;
					menuItem.CustomClass = item.CustomClass;
					break;
			}


			return menuItem;
		}


		protected virtual IEnumerable<MenuItem> Convert(Func<TreeNode, bool> selector, IEnumerable<TreeNode> nodes)
		{
			return nodes.Where(selector).Select(n => Convert(n, nodes)).ToArray();
		}


		protected virtual IEnumerable<MenuItem> CreateMenu(Guid menuGuid, CacheSettings cacheSettings)
		{
			// Get the path to the navigation menu
			PageTypes.Menu menuNode = DocumentHelper.GetDocuments<PageTypes.Menu>()
													.TopN(1)
													.Columns("NodeAliasPath", "NodeSiteID")
													.WhereEquals("NodeGUID", menuGuid)
													.OrderBy("NodeLevel, NodeOrder")
													.ApplyConfiguration(queryConfiguration);

			return CreateMenu(menuNode, cacheSettings);
		}


		protected virtual IEnumerable<MenuItem> CreateMenu(string menuType, CacheSettings cacheSettings)
		{
			// Get the path to the navigation menu
			PageTypes.Menu menuNode = DocumentHelper.GetDocuments<PageTypes.Menu>()
													.TopN(1)
													.Columns("NodeAliasPath", "NodeSiteID")
													.WhereEquals("MenuType", menuType)
													.OrderBy("NodeLevel, NodeOrder")
													.ApplyConfiguration(queryConfiguration);

			return CreateMenu(menuNode, cacheSettings);
		}


		protected virtual IEnumerable<MenuItem> CreateMenu(PageTypes.Menu menuNode, CacheSettings cacheSettings)
		{

			if (menuNode == null)
			{
				return null;
			}


			// Get the entire menu section
			var nodes = GetMenuTreeNodes();
			nodes = nodes.Where(x => x.NodeAliasPath.ToLower().Contains($"{menuNode.NodeAliasPath.ToLower()}/")).ToArray();

			// Add cache dependencies
			if (cacheSettings != null)
			{
				cacheSettings.CacheDependency = CacheHelper.GetCacheDependency($"node|{menuNode.NodeSiteName}|{menuNode.NodeAliasPath}|childnodes");
			}


			// Convert to menu items
			List<MenuItem> menu = new List<MenuItem>();

			foreach (TreeNode node in nodes.Where(node => node.NodeLevel == nodes.Min(n => n.NodeLevel)))
			{
				menu.Add(Convert(node, nodes));
			}

			return menu;
		}


		protected virtual IEnumerable<TreeNode> GetMenuTreeNodes()
		{
			IEnumerable<TreeNode> treeNodes = cacheService.GetFromCache(cs =>
			{
				// Get the entire menu section
				var nodes = DocumentHelper.GetDocuments()
														 .Columns(nameof(TreeNode.NodeAliasPath), nameof(TreeNode.NodeLevel), nameof(TreeNode.NodeOrder), "Image", "IsExternal", "Label", "LabelMobile", "LinkUrl", "NodeID", "NodeLevel", "NodeParentID", "Text")
														 .Type(PageTypes.MenuCard.CLASS_NAME, q => q.Columns(nameof(PageTypes.MenuCard.Image), nameof(PageTypes.MenuCard.IsExternal), nameof(PageTypes.MenuCard.Label), nameof( PageTypes.MenuCard.LabelMobile ), nameof(PageTypes.MenuCard.LinkUrl), nameof( PageTypes.MenuItem.CustomClass ), nameof(PageTypes.MenuCard.Text)))
														 .Type(PageTypes.MenuItem.CLASS_NAME, q => q.Columns(nameof(PageTypes.MenuItem.IsExternal), nameof(PageTypes.MenuItem.Label), nameof(PageTypes.MenuItem.LabelMobile), nameof(PageTypes.MenuItem.LinkUrl), nameof(PageTypes.MenuItem.CustomClass), nameof(PageTypes.MenuItem.HideMobileOverviewLink)))
														 .Type(PageTypes.MenuColumn.CLASS_NAME)
														 .OrderBy($"{nameof(TreeNode.NodeLevel)}, {nameof(TreeNode.NodeOrder)}")
														 .ApplyConfiguration(queryConfiguration)
														 .ToArray();

				if (nodes == null)
				{
					return null;
				}


				// Add cache dependencies
				cs.AddTypeDependency(queryConfiguration.SiteName.ToLower(), PageTypes.MenuCard.CLASS_NAME.ToLower());
				cs.AddTypeDependency(queryConfiguration.SiteName.ToLower(), PageTypes.MenuItem.CLASS_NAME.ToLower());
				cs.AddTypeDependency(queryConfiguration.SiteName.ToLower(), PageTypes.MenuColumn.CLASS_NAME.ToLower());

				return nodes;
			},

			cacheKey: $"menu|treenodes|all",
			alwaysCache: true);


			return treeNodes;
		}

		protected virtual IEnumerable<TreeNode> GetAllTreeNodes()
		{
			IEnumerable<TreeNode> treeNodes = cacheService.GetFromCache(cs =>
			{
				// Get the entire menu section
				var nodes = DocumentHelper.GetDocuments()
					.Columns(
						nameof(TreeNode.NodeLevel),
						nameof(TreeNode.NodeParentID),
						nameof(TreeNode.NodeAliasPath),
						nameof(TreeNode.DocumentName),
						nameof(TreeNode.NodeID),
						nameof(TreeNode.DocumentCustomData))
					.OrderBy($"{nameof(TreeNode.NodeLevel)}, {nameof(TreeNode.NodeOrder)},")
					.ApplyConfiguration(queryConfiguration)
					.ToArray();

				// filter out nodes that do not have a url..
				nodes = nodes.Where(x =>
				{
					var previewUrl = x.DocumentCustomData.GetStringValue(nameof(Preview.PreviewUrl));
					var hasUrl = x.HasUrl() || !string.IsNullOrWhiteSpace(previewUrl);
					return hasUrl;
				}).ToArray();

				if (nodes == null)
				{
					return null;
				}


				// Add cache dependencies
				cs.AddNodePathChildDependencies(queryConfiguration.SiteName.ToLower(), "/");

				return nodes;
			},

			   cacheKey: $"treenodes|all",
			   alwaysCache: true);


			return treeNodes;

		}

		protected virtual IEnumerable<MenuItem> CreateSideNavMenu(PageNode currentNode, CacheSettings cacheSettings, IDocumentSpecification documentSpecification, string rootMenuClassName = null)
		{
			// Get the top level node for the side nav menu
			string topLevelNodePath = GetTopLevelNodePath(currentNode, rootMenuClassName);
			documentSpecification.Path = topLevelNodePath;

			//// Get the entire menu section
			//MultiDocumentQuery query = 
			//	DocumentHelper.GetDocuments()
			//	.Columns(nameof(TreeNode.NodeLevel), nameof(TreeNode.NodeParentID), nameof(TreeNode.NodeAliasPath), nameof(TreeNode.DocumentName), nameof(TreeNode.NodeID), nameof(TreeNode.DocumentCustomData))
			//	.OrderBy("NodeLevel, NodeOrder")
			//	.ApplyConfiguration(queryConfiguration)
			//	.ApplyDocumentSpecification(documentSpecification);

			var nodes = GetAllTreeNodes();
			nodes = nodes.Where(x => x.NodeAliasPath.ToLower().StartsWith(topLevelNodePath.ToLower()));
			var maxNodeLevel = nodes.Min(y => y.NodeLevel) + documentSpecification.NestingLevel;
			nodes = nodes.Where(x => x.NodeLevel <= maxNodeLevel);


			// Add cache dependencies
			if (cacheSettings != null)
			{
				cacheSettings.AddNodeDependency(queryConfiguration.SiteName, topLevelNodePath);
				cacheSettings.AddNodePathChildDependencies(queryConfiguration.SiteName, topLevelNodePath);
			}


			// Convert to menu items
			return nodes
				.Where(x => x.NodeLevel == nodes.Min(y => y.NodeLevel))
				.Select(x => Convert(x, nodes, currentNode))
				.ToArray();
		}


		protected virtual string GetTopLevelNodePath(PageNode currentNode, string rootMenuClassName = null)
		{
			if (!string.IsNullOrWhiteSpace(rootMenuClassName))
			{
				var rootMenuNode = documentService.GetNearestAncestor(currentNode, rootMenuClassName);
				if (rootMenuNode != null)
				{
					return rootMenuNode.NodeAliasPath;
				}
			}

			string[] pathComponents = currentNode.NodeAliasPath.Split('/');

			if (pathComponents.Length > 0)
			{
				return $@"/{pathComponents[1]}";
			}

			else
			{
				return "/";
			}
		}


		protected virtual MenuItem Convert(TreeNode treeNode, IEnumerable<TreeNode> nodeFamily, PageNode currentPage)
		{
			var previewUrl = treeNode.DocumentCustomData.GetStringValue(nameof(Preview.PreviewUrl));
			var documentUrlPath = treeNode.DocumentCustomData.GetStringValue(Constants.DocumentUrlPath);
			var url = CoalesceUtility.CoalesceWithoutWhitespace(previewUrl, documentUrlPath);

			return new MenuItem
			{
				IsCurrentPage = treeNode.NodeID == currentPage.NodeID,
				Text = CoalesceUtility.CoalesceWithoutWhitespace(treeNode.DocumentCustomData.GetStringValue(nameof(Preview.PreviewNavigationLabel)), treeNode.DocumentName),
				Url = url,
				Children = nodeFamily
						.Where(x => x.NodeParentID == treeNode.NodeID)
						.Select(x => Convert(x, nodeFamily, currentPage))
						.ToArray()
			};
		}

	}

}
