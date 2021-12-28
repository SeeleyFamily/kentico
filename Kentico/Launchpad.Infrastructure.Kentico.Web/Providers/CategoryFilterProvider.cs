using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Core.Abstractions.Providers;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Models;
using Launchpad.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Launchpad.Infrastructure.Kentico.Web.Providers
{
	public class CategoryFilterProvider : ICategoryFilterProvider, IPerScopeService
	{

		#region fields				
		private readonly ICategoryService categoryService;
		#endregion


		public CategoryFilterProvider(
			ICategoryService categoryService
		)
		{
			this.categoryService = categoryService;
		}


		public IEnumerable<string> GetAvailableCategories<T>(PagedResult<T> pagedResult)
		{
			var availableCategoryValues = new List<string>();
			var categoryFacet = pagedResult.GetFacet(nameof(PageNode.CategoryCodeNamePaths));
			if (categoryFacet != null && categoryFacet.Values != null && categoryFacet.Values.Any())
			{
				availableCategoryValues = categoryFacet.Values.Select(x => x.ToString()).ToList();
			}
			return availableCategoryValues;
		}


		private FilterOption CreateCategoryFilterOption(Category category, int maxLevel, IEnumerable<string> allowedOptions = null, bool useIdValue = false)
		{
			var filterOption = new FilterOption()
			{
				Name = category.DisplayName,
				Value = useIdValue ? category.Id.ToString() : category.CodeName
			};
			if (allowedOptions != null && allowedOptions.Any())
			{
				// Comparison is using CodeNamePath
				// The expected usage is to pass the CodeNamePath from the object query
				// The values are expected to start and end with a / to prevent - false positive results where the full string contains a substring
				// We also use Starts With to prevent nested substring false positives.
				if (!allowedOptions.Any(x => x.ToLower().StartsWith(category.CodeNamePath.ToLower())))
				{
					return null;
				}
			}
			// Handle sub categories && sub filter options
			if (category.Level < maxLevel && category.Children != null && category.Children.Any())
			{
				var childFilterOptions = new List<FilterOption>();
				foreach (var childCategory in category.Children)
				{
					var childFilterOption = CreateCategoryFilterOption(childCategory, maxLevel, allowedOptions, useIdValue);
					if (childFilterOption != null)
					{
						childFilterOptions.Add(childFilterOption);
					}
				}
				filterOption.Options = childFilterOptions;
			}
			return filterOption;
		}


		/// <summary>
		/// Method to produce a filter based on a category. The specified category will be the top level parent category and its children will be the options.
		/// Levels default to 1 to product the basic filter options. Increasing levels will produce a filter with more sub filter options based on the level order of the subcategories and their respective subcategories.
		/// The allowed options will trim the filter options down to values that those available in the enumerable.
		/// Common usage is restricting options to the values available by the content.		
		/// </summary>		
		public Filter GetParentCategoryFilter(Category parentCategory, string specification, int levels = 1, IEnumerable<string> allowedOptions = null, bool useIdValue = false)
		{
			// Default to 1 level
			if (levels < 1)
			{
				levels = 1;
			}

			var filter = new Filter()
			{
				Label = parentCategory.DisplayName,
				Specification = specification,
			};

			var filterOptions = CreateCategoryFilterOption(parentCategory, parentCategory.Level + levels, allowedOptions, useIdValue);
			if (filterOptions != null && filterOptions.Options != null)
			{
				filter.Options = filterOptions.Options;
			}

			return filter;
		}


		public Filter GetParentCategoryFilter(string parentCategoryCodeName, string specification, int levels = 1, IEnumerable<string> allowedOptions = null, bool useIdValue = false)
		{
			var parentCategory = categoryService.GetCategory(parentCategoryCodeName);
			return GetParentCategoryFilter(parentCategory, specification, levels, allowedOptions, useIdValue);
		}


		public Filter GetParentCategoryFilter(Guid parentCategoryGuid, string specification, int levels = 1, IEnumerable<string> allowedOptions = null, bool useIdValue = false)
		{
			var parentCategory = categoryService.GetCategory(parentCategoryGuid);
			return GetParentCategoryFilter(parentCategory, specification, levels, allowedOptions, useIdValue);
		}


		public Filter GetCategoryFilter(IEnumerable<Category> categories, string specification, IEnumerable<string> allowedOptions = null, bool useIdValue = false)
		{
			var filter = new Filter()
			{
				Label = "",
				Specification = specification,
			};

			List<FilterOption> options = new List<FilterOption>();

			foreach (var category in categories)
			{
				if (category == null)
				{
					continue;
				}
				if (allowedOptions != null && allowedOptions.Any())
				{
					// Comparison is using CodeNamePath
					// The expected usage is to pass the CodeNamePath from the object query
					// The values are expected to start and end with a / to prevent - false positive results where the full string contains a substring
					// We also use Starts With to prevent nested substring false positives.
					if (!allowedOptions.Any(x => x.ToLower().StartsWith(category.CodeNamePath.ToLower())))
					{
						continue;
					}
				}
				var filterOption = new FilterOption()
				{
					Name = category.DisplayName,
					Value = useIdValue ? category.Id.ToString() : category.CodeName
				};
				options.Add(filterOption);


			}
			filter.Options = options;
			return filter;
		}


		public Filter GetCategoryFilter(IEnumerable<string> categoryCodeNames, string specification, IEnumerable<string> allowedOptions = null, bool useIdValue = false)
		{
			var categories = categoryService.GetCategories(categoryCodeNames);
			return GetCategoryFilter(categories, specification, allowedOptions, useIdValue);
		}


		public Filter GetCategoryFilter(IEnumerable<Guid> categoryGuids, string specification, IEnumerable<string> allowedOptions = null, bool useIdValue = false)
		{
			var categories = categoryService.GetCategories(categoryGuids);
			return GetCategoryFilter(categories, specification, allowedOptions, useIdValue);
		}


	}
}
