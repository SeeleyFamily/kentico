using CMS.DocumentEngine;
using CMS.Taxonomy;
using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Extensions;
using Launchpad.Core.Models;
using Launchpad.Infrastructure.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Launchpad.Infrastructure.Services
{
	public class CategoryService : ICategoryService, IPerApplicationService
	{
		#region Fields
		private readonly ICacheService cacheService;
		private readonly ISiteContextConfiguration siteContextConfiguration;
		private readonly string delimeter = "|";
		#endregion

		public CategoryService
		(
			ICacheService cacheService,
			ISiteContextConfiguration siteContextConfiguration
		)
		{
			this.cacheService = cacheService;
			this.siteContextConfiguration = siteContextConfiguration;
		}


		public Dictionary<int, int[]> GetDocumentCategories()
		{
			Dictionary<int, int[]> result = cacheService.GetFromCache(cs =>
			{
				IEnumerable<DocumentCategoryInfo> documentCategoryInfos = DocumentCategoryInfo.Provider.Get().ToList();
				var keys = documentCategoryInfos.Select(x => x.DocumentID).Distinct();
				return keys.ToDictionary(
										x => x,
										x => documentCategoryInfos.Where(y => x == y.DocumentID).Select(y => y.CategoryID).ToArray()
										);
			},
				cacheKey: $"{nameof(GetDocumentCategories)}",
				cacheDependencies: new string[]
				{
					"cms.documentcategory|all"
				},
				alwaysCache: true
			);
			return result;
		}

		public IEnumerable<Category> GetDocumentCategories(PageNode pageNode)
		{
			return GetDocumentCategories(pageNode.DocumentID);
		}

		public IEnumerable<Category> GetDocumentCategoriesWithoutAllCache(int documentId)
		{
			IEnumerable<Category> documentCategories = cacheService.GetFromCache(cs =>
			{
				var currentDocumentCategories = new List<Category>();
				var categoryInfos = DocumentCategoryInfoProvider.GetDocumentCategories(documentId).ToList();
				var currentDocumentCategoryIDs = categoryInfos.Select(x => x.CategoryID);
				var allCategories = GetCategories();

				currentDocumentCategories.AddRange(
					allCategories.Where(x => currentDocumentCategoryIDs.Contains(x.Id))
				);

				return currentDocumentCategories;
			},
				cacheKey: $"{nameof(GetCategories)}|{documentId}",
				cacheDependencies: new string[]
				{
					// $"cms.documentcategory|byid|{documentId}", // This cache dependency did not work as expected based on the Kentico Dependency Documentation
					$"documentid|{documentId}", // Instead, we have an event log on the CMS side that will trigger a save on the tree node when a category is added...
				}
			);
			return documentCategories;
		}

		public IEnumerable<Category> GetDocumentCategories(int documentId)
		{
			IEnumerable<Category> documentCategories = cacheService.GetFromCache(cs =>
			{
				var currentDocumentCategories = new List<Category>();
				var allDocumentCategories = GetDocumentCategories();
				var allCategories = GetCategories();
				int[] categoryIDs;
				if (allDocumentCategories.TryGetValue(documentId, out categoryIDs))
				{
					currentDocumentCategories.AddRange(
						categoryIDs.Select(x => allCategories.SingleOrDefault(y => x == y.Id))
					);
				}
				return currentDocumentCategories;
			},
				cacheKey: $"{nameof(GetCategories)}|{documentId}",
				cacheDependencies: new string[]
				{
					// $"cms.documentcategory|byid|{documentId}", // This cache dependency did not work as expected based on the Kentico Dependency Documentation
					$"documentid|{documentId}", // Instead, we have an event log on the CMS side that will trigger a save on the tree node when a category is added...
				}
			);
			return documentCategories;
		}


		public string GetDelimitedCategoryGuids(IEnumerable<Category> categories)
		{
			return string.Join(delimeter, categories.Select(x => x.Guid.ToString()));
		}


		public string GetDelimetedCategoryDisplayNames(IEnumerable<Category> categories)
		{
			return string.Join(delimeter, categories.Select(x => x.DisplayName.ToString()));
		}


		public string GetDelimitedCategoryCodeNamePaths(IEnumerable<Category> categories)
		{
			return string.Join(delimeter, categories.Select(x => x.CodeNamePath.ToString()));
		}


		public IEnumerable<Category> GetCategories()
		{
			IEnumerable<Category> result = cacheService.GetFromCache(cs =>
			{
				var categoryInfos = CategoryInfo.Provider.Get()
					.OnSite(siteContextConfiguration.SiteId, true)
					.OrderBy(nameof(CategoryInfo.CategoryLevel), nameof(CategoryInfo.CategoryOrder)).TypedResult;

				var categories = categoryInfos.Select(ci =>
				{
					return new Category
					{
						Id = ci.CategoryID,
						Guid = ci.CategoryGUID,
						DisplayName = ci.CategoryDisplayName,
						CodeName = ci.CategoryName,
						DisplayNamePath = ci.CategoryNamePath,
						CodeNamePath = ci.CategoryName,
						Level = ci.CategoryLevel,
					};
				}).ToList();

				// Set the Parent, Children, and CodeNamePath
				categories.ForEach(c =>
				{
					var categoryInfo = categoryInfos.Where(ci => ci.CategoryName == c.CodeName).FirstOrDefault();
					if (categoryInfo != null)
					{
						var parentCategoryInfo = categoryInfos.Where(ci => ci.CategoryID == categoryInfo.CategoryParentID).FirstOrDefault();
						if (parentCategoryInfo != null)
						{
							c.Parent = categories.Where(x => x.CodeName == parentCategoryInfo.CategoryName).FirstOrDefault();
							c.CodeNamePath = $"{c.Parent.CodeNamePath}/{c.CodeNamePath}";
						}
						var childrenCategoryInfos = categoryInfos.Where(ci => ci.CategoryParentID == categoryInfo.CategoryID).OrderBy(ci => ci.CategoryOrder);
						if (childrenCategoryInfos.Any())
						{
							var childrenCategoryNames = childrenCategoryInfos.Select(cci => cci.CategoryName);
							c.Children = categories.Where(x => childrenCategoryNames.Contains(x.CodeName));
						}
					}
				});

				// Ensure CodeNamePath Starts and Ends with /
				categories.ForEach(c =>
				{
					c.CodeNamePath = c.CodeNamePath.GetWrappedCodeName();
				});

				return categories;
			},
				cacheKey: $"{nameof(GetCategories)}",
				cacheDependencies: new string[]
				{
					"cms.category|all"
				},
				alwaysCache: true
			);
			return result;
		}


		public IEnumerable<Category> GetCategories(IEnumerable<string> codeNames)
		{
			var categories = GetCategories();
			return categories.Where(x => codeNames.Contains(x.CodeName));
		}


		public IEnumerable<Category> GetCategories(IEnumerable<Guid> guids)
		{
			var categories = GetCategories();
			return categories.Where(x => guids.Contains(x.Guid));
		}


		public Category GetCategory(string codeName)
		{
			return GetCategories().Where(x => x.CodeName == codeName).FirstOrDefault();
		}

		public Category GetCategory(Guid categoryGuid)
		{
			return GetCategories().Where(x => x.Guid == categoryGuid).FirstOrDefault();
		}

		public IEnumerable<Category> GetCategoriesByParentCategory(string parentCategoryCodeName)
		{
			var result = new List<Category>();
			var parentCategory = GetCategory(parentCategoryCodeName);
			if (parentCategory != null)
			{
				result.AddRange(parentCategory.Children);
			}
			return result;

		}

		public IEnumerable<Category> GetCategoriesByParentCategory(Guid parentCategoryGuid)
		{
			var result = new List<Category>();
			var parentCategory = GetCategory(parentCategoryGuid);
			if (parentCategory != null)
			{
				result.AddRange(parentCategory.Children);
			}
			return result;

		}

		public double GetCategoryRelatedRanking(IEnumerable<string> categoryCodeNames1, IEnumerable<string> categoryCodeNames2)
		{
			var categoryRelatedRankingDictionary = ExplodeCategoryCodeNames(categoryCodeNames1);
			categoryRelatedRankingDictionary = ExplodeCategoryCodeNames(categoryCodeNames1);

			var relevantValues = categoryRelatedRankingDictionary.Where(x => categoryCodeNames2.Contains(x.Key));
			var ranking = relevantValues.Select(x => x.Value).Sum(x => x);
			return ranking;
		}

		private Dictionary<string, double> ExplodeCategoryCodeNames(IEnumerable<string> categoryCodeNames)
		{
			categoryCodeNames = categoryCodeNames.Where(x => !(categoryCodeNames.Any(y => y.Contains(x) && y != x))).ToList();
			Dictionary<string, double> allCategoryCodeNames = new Dictionary<string, double>();
			foreach (var categoryCodeName in categoryCodeNames)
			{
				if (!allCategoryCodeNames.ContainsKey(categoryCodeName))
				{
					allCategoryCodeNames.Add(categoryCodeName, 1);
				}
				var codeNameParts = categoryCodeName.Trim('/').Split('/');
				var partCount = codeNameParts.Count();
				for (var i = 0; i < partCount - 1; i++)
				{
					var currentPartCount = partCount - 1 - i;
					var parentCategoryCodeName = $"/{string.Join("/", codeNameParts.Take(currentPartCount))}/";
					if (!allCategoryCodeNames.ContainsKey(parentCategoryCodeName))
					{
						double rankingValue = Math.Round((double)currentPartCount / (double)partCount, 3);
						allCategoryCodeNames.Add(parentCategoryCodeName, rankingValue);
					}
				}

			}

			return allCategoryCodeNames;
		}


	}
}
