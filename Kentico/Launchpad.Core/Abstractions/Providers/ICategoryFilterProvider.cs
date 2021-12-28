using Launchpad.Core.Models;
using System;
using System.Collections.Generic;

namespace Launchpad.Core.Abstractions.Providers
{
	public interface ICategoryFilterProvider
	{
		Filter GetParentCategoryFilter(Category parentCategory, string specification, int levels = 1, IEnumerable<string> allowedOptions = null, bool useIdValue = false);
		Filter GetParentCategoryFilter(string parentCategoryCodeName, string specification, int levels = 1, IEnumerable<string> allowedOptions = null, bool useIdValue = false);
		Filter GetParentCategoryFilter(Guid parentCategoryGuid, string specification, int levels = 1, IEnumerable<string> allowedOptions = null, bool useIdValue = false);
		Filter GetCategoryFilter(IEnumerable<Category> categories, string specification, IEnumerable<string> allowedOptions = null, bool useIdValue = false);
		Filter GetCategoryFilter(IEnumerable<string> categoryCodeNames, string specification, IEnumerable<string> allowedOptions = null, bool useIdValue = false);
		Filter GetCategoryFilter(IEnumerable<Guid> categoryGuids, string specification, IEnumerable<string> allowedOptions = null, bool useIdValue = false);
		IEnumerable<string> GetAvailableCategories<T>(PagedResult<T> pagedResult);
	}
}
