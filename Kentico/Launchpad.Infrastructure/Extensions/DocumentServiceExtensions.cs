using Launchpad.Core.Abstractions.Specifications;
using Launchpad.Core.Attributes;
using Launchpad.Core.Constants;
using Launchpad.Core.Enums;
using Launchpad.Core.Extensions;
using Launchpad.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Launchpad.Infrastructure.Extensions
{

	public static class DocumentServiceExtensions
	{


		public static IEnumerable<PageNode> ApplyDocumentSpecification(this IEnumerable<PageNode> pageNodes, IDocumentSpecification specification)
		{
			pageNodes = pageNodes.ApplyIncludedNodesSpecification(specification);
			pageNodes = pageNodes.ApplyExcludedNodesSpecification(specification);
			pageNodes = pageNodes.ApplyIncludedGuidsSpecification(specification);
			pageNodes = pageNodes.ApplyExcludedGuidsSpecification(specification);

			pageNodes = pageNodes.ApplyClassNameSpecification(specification);
			pageNodes = pageNodes.ApplyCategoriesSpecification(specification);
			pageNodes = pageNodes.ApplyPathSpecification(specification);

			pageNodes = pageNodes.ApplySearchTermSpecification(specification);
			return pageNodes;
		}


		public static IEnumerable<PageNode> ApplyPathSpecification(this IEnumerable<PageNode> pageNodes, IDocumentSpecification specification)
		{
			if (!string.IsNullOrWhiteSpace(specification.Path))
			{
				var path = specification.Path.ToLower();
				pageNodes = pageNodes.Where(x =>
					{
						var nodeAliasPath = x.NodeAliasPath.ToLower();
						var documentUrlPath = x.DocumentUrlPath != null ? x.DocumentUrlPath.ToLower() : string.Empty;
						return nodeAliasPath.StartsWith(path) || documentUrlPath.StartsWith(path);
					}
				);
			}
			return pageNodes;
		}


		public static IEnumerable<PageNode> ApplySearchTermSpecification(this IEnumerable<PageNode> pageNodes, IDocumentSpecification specification)
		{
			if (!string.IsNullOrWhiteSpace(specification.SearchTerm))
			{
				var searchTerms = specification.SearchTerm.Split(' ');
				pageNodes = pageNodes.Where(x =>
				{
					var searchBlobString = x.CustomData.GetStringValue(Constants.DocumentCustomDataSearchBlobKey);
					var words = searchBlobString.Split(' ');
					return searchTerms.All(y => words.Contains(y.ToLower()));
				});
			}
			return pageNodes;
		}


		public static IEnumerable<PageNode> ApplyIncludedNodesSpecification(this IEnumerable<PageNode> pageNodes, IDocumentSpecification specification)
		{
			if (!specification.Nodes.IsNullOrEmpty())
			{
				pageNodes = pageNodes.Where(x => specification.Nodes.Contains(x.NodeID));
			}
			return pageNodes;
		}


		public static IEnumerable<PageNode> ApplyExcludedNodesSpecification(this IEnumerable<PageNode> pageNodes, IDocumentSpecification specification)
		{
			if (!specification.ExcludedNodes.IsNullOrEmpty())
			{
				pageNodes = pageNodes.Where(x => !specification.ExcludedNodes.Contains(x.NodeID));
			}
			return pageNodes;
		}


		public static IEnumerable<PageNode> ApplyIncludedGuidsSpecification(this IEnumerable<PageNode> pageNodes, IDocumentSpecification specification)
		{
			if (!specification.Guids.IsNullOrEmpty())
			{
				pageNodes = pageNodes.Where(x => specification.Guids.Contains(x.NodeGuid));
			}
			return pageNodes;
		}


		public static IEnumerable<PageNode> ApplyExcludedGuidsSpecification(this IEnumerable<PageNode> pageNodes, IDocumentSpecification specification)
		{
			if (!specification.ExcludedGuids.IsNullOrEmpty())
			{
				pageNodes = pageNodes.Where(x => !specification.ExcludedGuids.Contains(x.NodeGuid));
			}
			return pageNodes;
		}


		public static IEnumerable<PageNode> ApplyCategoriesSpecification(this IEnumerable<PageNode> pageNodes, IDocumentSpecification specification)
		{
			pageNodes = pageNodes.ApplyIncludedCategoriesSpecification(specification);
			pageNodes = pageNodes.ApplyExcludedCategoriesSpecification(specification);
			return pageNodes;
		}


		public static IEnumerable<PageNode> ApplyIncludedCategoriesSpecification(this IEnumerable<PageNode> pageNodes, IDocumentSpecification specification)
		{
			if (!specification.Categories.IsNullOrEmpty())
			{
				pageNodes = pageNodes.Where(x =>
					{
						var categories = x.Categories;
						if (categories == null)
						{
							categories = new List<Category>();
						}
						return x.Categories.Any(y => specification.Categories.Any(z => y.CodeNamePath.ToLower().Contains(z.ToLower())));
					}
				);
			}

			return pageNodes;
		}


		public static IEnumerable<PageNode> ApplyExcludedCategoriesSpecification(this IEnumerable<PageNode> pageNodes, IDocumentSpecification specification)
		{
			if (!specification.ExcludedCategories.IsNullOrEmpty())
			{
				pageNodes = pageNodes.Where(x =>
					{
						var categories = x.Categories;
						if (categories == null)
						{
							categories = new List<Category>();
						}
						return !x.Categories.Any(y => specification.ExcludedCategories.Any(z => y.CodeNamePath.ToLower().Contains(z.ToLower())));
					}
				);
			}
			return pageNodes;
		}


		public static IEnumerable<PageNode> ApplyClassNameSpecification(this IEnumerable<PageNode> pageNodes, IDocumentSpecification specification)
		{
			pageNodes = pageNodes.ApplyIncludedClassNameSpecification(specification);
			pageNodes = pageNodes.ApplyExcludedClassNameSpecification(specification);
			return pageNodes;
		}


		public static IEnumerable<PageNode> ApplyIncludedClassNameSpecification(this IEnumerable<PageNode> pageNodes, IDocumentSpecification specification)
		{
			if (!specification.ClassNames.IsNullOrEmpty())
			{
				pageNodes = pageNodes.Where(x => specification.ClassNames.Any(y => y.ToLower().Equals(x.NodeClassName.ToLower())));
			}
			return pageNodes;
		}


		public static IEnumerable<PageNode> ApplyExcludedClassNameSpecification(this IEnumerable<PageNode> pageNodes, IDocumentSpecification specification)
		{
			if (!specification.ExcludedClassNames.IsNullOrEmpty())
			{
				pageNodes = pageNodes.Where(x => !specification.ExcludedClassNames.Any(y => y.ToLower().Equals(x.NodeClassName.ToLower())));
			}
			return pageNodes;
		}


		public static IEnumerable<PageNode> ApplyDocumentSortSpecification(this IEnumerable<PageNode> pageNodes, IDocumentSpecification specification)
		{
			pageNodes = ApplyNodeOrderSortSpecification(pageNodes, specification);
			pageNodes = ApplyNodesSortSpecification(pageNodes, specification);
			pageNodes = ApplyGuidsSortSpecification(pageNodes, specification);
			return pageNodes;
		}


		public static IEnumerable<PageNode> ApplyNodeOrderSortSpecification(this IEnumerable<PageNode> pageNodes, IDocumentSpecification specification)
		{
			if (!string.IsNullOrWhiteSpace(specification.Sort))
			{
				if (SortType.NodeOrder.GetAttribute<CodeDisplayNameTypeAttribute>().CodeName.Equals(specification.Sort, StringComparison.InvariantCultureIgnoreCase))
				{
					pageNodes = pageNodes.OrderBy(x => x.NodeParentID).ThenBy(x => x.NodeOrder);
				}
			}
			return pageNodes;
		}


		public static IEnumerable<PageNode> ApplyNodesSortSpecification(this IEnumerable<PageNode> pageNodes, IDocumentSpecification specification)
		{
			if (!string.IsNullOrWhiteSpace(specification.Sort))
			{
				if (SortType.NodeOrder.GetAttribute<CodeDisplayNameTypeAttribute>().CodeName.Equals(specification.Sort, StringComparison.InvariantCultureIgnoreCase))
				{
					if (!specification.Nodes.IsNullOrEmpty())
					{
						pageNodes = pageNodes.OrderBy(x => Array.IndexOf(specification.Nodes, x.NodeID));
					}
				}
			}
			return pageNodes;
		}


		public static IEnumerable<PageNode> ApplyGuidsSortSpecification(this IEnumerable<PageNode> pageNodes, IDocumentSpecification specification)
		{
			if (!string.IsNullOrWhiteSpace(specification.Sort))
			{
				if (SortType.Guids.GetAttribute<CodeDisplayNameTypeAttribute>().CodeName.Equals(specification.Sort, StringComparison.InvariantCultureIgnoreCase))
				{
					if (!specification.Guids.IsNullOrEmpty())
					{
						pageNodes = pageNodes.OrderBy(x => Array.IndexOf(specification.Guids, x.NodeGuid));
					}
				}
			}
			return pageNodes;
		}


		public static IEnumerable<PageNode> ApplyFeaturedSortSpecification(this IEnumerable<PageNode> pageNodes, IFeaturedSpecification specification)
		{
			if (specification.FeaturedGuids.IsNullOrEmpty())
			{
				return pageNodes;
			}

			List<PageNode> featuredPageNodes = new List<PageNode>();
			List<PageNode> defaultPageNodes = new List<PageNode>();
			if (!specification.FeaturedGuids.IsNullOrEmpty())
			{
				var featuredNodesLength = specification.FeaturedGuids.Length;
				foreach (var pageNode in pageNodes)
				{
					if (specification.FeaturedGuids.Contains(pageNode.NodeGuid))
					{
						pageNode.FeatureOrder = featuredNodesLength - Array.IndexOf(specification.FeaturedGuids, pageNode.NodeGuid);
						featuredPageNodes.Add(pageNode);
					}
					else
					{
						defaultPageNodes.Add(pageNode);
					}
				}
			}

			List<PageNode> sortedPageNodes = new List<PageNode>();
			sortedPageNodes.AddRange(featuredPageNodes.OrderByDescending(x => x.FeatureOrder));
			sortedPageNodes.AddRange(defaultPageNodes);

			return sortedPageNodes;
		}


	}

}
