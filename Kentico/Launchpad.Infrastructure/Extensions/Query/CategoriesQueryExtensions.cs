using CMS.DocumentEngine;
using Launchpad.Core.Abstractions.Specifications;
using Launchpad.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace Launchpad.Infrastructure.Extensions.Query
{
	public static class CategoriesQueryExtensions
	{
		public static DocumentQuery<T> ApplyCategoriesSpecification<T>(this DocumentQuery<T> query, ICategoriesSpecification specification)
		where T : TreeNode, new()
		{
			if (specification == null)
			{
				return query;
			}

			// Categories Filter
			if (specification.Categories != null && specification.Categories.Any())
			{
				query.InEnabledCategories(specification.Categories);
			}

			return query;
		}

		public static MultiDocumentQuery ApplyCategoriesSpecification(this MultiDocumentQuery query, ICategoriesSpecification specification)
		{
			if (specification == null)
			{
				return query;
			}

			// Categories Filter
			if (specification.Categories != null && specification.Categories.Any())
			{
				query.InEnabledCategories(specification.Categories);
			}

			return query;

		}

		public static IEnumerable<T> ApplyCategoriesSpecification<T>(this IEnumerable<T> result, ICategoriesSpecification specification)
			where T : PageNode, new()
		{
			if (specification == null)
			{
				return result;
			}

			// TODO
			// NOT IMPLEMENTED

			return result;
		}
	}
}
