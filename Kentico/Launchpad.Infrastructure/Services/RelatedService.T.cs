using CMS.Helpers;
using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Extensions;
using Launchpad.Core.Models;
using Launchpad.Core.Specifications;
using Launchpad.Infrastructure.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Launchpad.Infrastructure.Services
{
	public class RelatedService<T> : IRelatedService<T>, IPerScopeService
		where T : PageNode
	{
		#region fields
		private readonly ICacheService cacheService;
		private readonly ICategoryService categoryService;
		private readonly IDocumentService documentService;
		private readonly IDocumentQueryConfiguration queryConfiguration;
		private readonly IPreviewService previewService;
		#endregion

		public RelatedService
		(
			ICacheService cacheService,
			ICategoryService categoryService,
			IDocumentService documentService,
			IDocumentQueryConfiguration queryConfiguration,
			IPreviewService previewService
		)
		{
			this.cacheService = cacheService;
			this.categoryService = categoryService;
			this.documentService = documentService;
			this.queryConfiguration = queryConfiguration;
			this.previewService = previewService;
		}

		public IEnumerable<T> GetRelatedDocuments(PageNode pageNode, IEnumerable<T> allPageNodes, int count, string path = "/", string[] includedClassNames = null, string[] excludedCategories = null, string[] specifiedCategoryCodeNamePaths = null)
		{
			// first get the categories from node or get it from the fallback
			var categoryCodeNamePaths = pageNode.CategoryCodeNamePaths;
			if (!specifiedCategoryCodeNamePaths.IsNullOrEmpty())
			{
				categoryCodeNamePaths = specifiedCategoryCodeNamePaths;
			}
			if (categoryCodeNamePaths.IsNullOrEmpty())
			{
				var fallbackNode = allPageNodes.Where(x => x.NodeID == pageNode.NodeID).FirstOrDefault();
				if (fallbackNode != null)
				{
					categoryCodeNamePaths = fallbackNode.CategoryCodeNamePaths;
				}
				if (categoryCodeNamePaths == null)
				{
					var retrievedCategories = categoryService.GetDocumentCategories(pageNode);
					categoryCodeNamePaths = retrievedCategories.Select(x => x.CodeNamePath);
				}
			}

			// fall back to all nodes on the specified path ordered by most recently published
			if (categoryCodeNamePaths.IsNullOrEmpty())
			{
				return allPageNodes
					.Where(x => x.NodeID != pageNode.NodeID)
					.Where(x => x.DocumentUrlPath?.Contains(path) ?? false)
					.Where(x =>
					{
						if (includedClassNames == null)
						{
							return true;
						}
						else
						{
							return includedClassNames.Contains(x.NodeClassName);
						}
					})
					.OrderByDescending(x => x.DatePublished).Take(count).ToList();
			}

			categoryCodeNamePaths = categoryCodeNamePaths.Where(x =>
			{
				if (excludedCategories == null)
				{
					return true;
				}
				return excludedCategories.All(y =>
				{
					return !x.ToLower().Contains(y.ToLower());
				});
			}).ToList();

			List<T> relatedPageNodes = allPageNodes
				.Where(x => x.NodeID != pageNode.NodeID)
				.Where(x => x.DocumentUrlPath?.Contains(path) ?? false)
				.Where(x =>
				{
					if (includedClassNames == null)
					{
						return true;
					}
					else
					{
						return includedClassNames.Contains(x.NodeClassName);
					}
				})
				.Where(x => !x.CategoryCodeNamePaths.IsNullOrEmpty())
				.ToList();

			List<T> orderedRelatedPageNodes = relatedPageNodes
				.OrderByDescending(x =>
				{
					return categoryService.GetCategoryRelatedRanking(categoryCodeNamePaths, x.CategoryCodeNamePaths);
				})
				.ThenByDescending(x => x.DatePublished).Take(count).ToList();

			return orderedRelatedPageNodes;
		}

		public IEnumerable<T> GetRelatedDocuments(int nodeId, RelatedSpecification relatedSpecification, string[] categoryCodeNamePaths = null)
		{
			var pageNode = documentService.Get(nodeId);
			return GetRelatedDocuments(pageNode, relatedSpecification, categoryCodeNamePaths);

		}

		public IEnumerable<T> GetRelatedDocuments(Guid nodeGuid, RelatedSpecification relatedSpecification, string[] categoryCodeNamePaths = null)
		{
			var pageNode = documentService.Get(nodeGuid);
			return GetRelatedDocuments(pageNode, relatedSpecification, categoryCodeNamePaths);

		}

		public IEnumerable<T> GetRelatedDocuments(PageNode pageNode, RelatedSpecification relatedSpecification, string[] categoryCodeNamePaths = null)
		{
			if(pageNode == null)
			{
				// this can occur on preview mode of archived content
				// no related content for archived content
				return new List<T>();
			}
			IEnumerable<T> getRelatedDocuments(CacheSettings cs)
			{
				List<T> relatedDocuments = new List<T>();
				foreach (var guid in relatedSpecification.FeaturedGuids)
				{
					if (guid == pageNode.NodeGuid)
					{
						break;
					}
					var featuredNode = documentService.Get(guid);
					if (featuredNode != null)
					{
						relatedDocuments.Add((T)featuredNode);
					}
				}
				if (relatedDocuments.Count() < relatedSpecification.PageSize)
				{
					var allPreviewNodes = previewService.GetPreviewNodes().Select(x => (T)x);

					// Remove overridden related content
					allPreviewNodes = allPreviewNodes.Where(x => !relatedSpecification.FeaturedGuids.Contains(x.NodeGuid));

					var topRelatedPathPageNodes = GetRelatedDocuments(pageNode,
						allPreviewNodes,
						relatedSpecification.PageSize - relatedDocuments.Count(),
						relatedSpecification.Path,
						relatedSpecification.ClassNames,
						relatedSpecification.ExcludedCategories,
						categoryCodeNamePaths);
					relatedDocuments.AddRange(topRelatedPathPageNodes);
				}

				return relatedDocuments.Take(relatedSpecification.PageSize).ToList();
			}
			var finalRelatedDocuments = cacheService.GetFromCache(getRelatedDocuments, $"finalRelatedDocuments|{pageNode.NodeGuid}",
				//alwaysCache: true,
				cacheDependencies: new string[] {
					$"nodeid|{pageNode.NodeID}".ToLower()
					}); ;
			return finalRelatedDocuments;
		}
	}
}
