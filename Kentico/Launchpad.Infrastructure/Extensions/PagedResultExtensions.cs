using Launchpad.Core.Abstractions.Specifications;
using Launchpad.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Launchpad.Infrastructure.Extensions
{
	public static class PagedResultExtensions
	{
		/// <summary>
		/// This paged result extension takes a conversion function to convert the type of <see cref="PagedResult{T}"/> to <see cref="PagedResult{T2}"/>
		/// </summary>				
		public static PagedResult<T2> ConvertTo<T, T2>(this PagedResult<T> pagedResult, Func<T, T2> ToModel)
		{
			return new PagedResult<T2>
			{
				PageIndex = pagedResult.PageIndex,
				PageSize = pagedResult.PageSize,
				Items = pagedResult.Items.Select(x => ToModel(x)).ToList(),
				RowEnd = pagedResult.RowEnd,
				RowStart = pagedResult.RowStart,
				Specification = pagedResult.Specification,
				Total = pagedResult.Total,
				TotalPages = pagedResult.TotalPages,
				Facets = pagedResult.Facets,
			};
		}

		public static PagedResult<T> ToPagedResult<T>(this IEnumerable<T> result, IPagedSpecification specification)
		{
			// Set defaults
			int pageIndex = specification.PageIndex;
			int pageSize = specification.PageSize;

			var pagedResults = result;

			if (pageIndex >= 0 && pageSize > 0)
			{
				// Set query to page
				pagedResults = pagedResults.Skip(pageIndex * pageSize).Take(pageSize);

			}

			// Execute the query and return its results so we have paging data in the query object
			T[] results = pagedResults.ToArray();


			// Compute paging data
			int rowStart = 0;
			int rowEnd = 0;
			int pageCount = 0;
			int totalCount = result.Count();
			if (result.Any())
			{
				// Rows are 1-based (not 0-based like indices)
				rowStart = pageSize * pageIndex + 1;
				rowEnd = rowStart + pageSize - 1;

				if (pageSize > 0)
				{
					pageCount = (int)Math.Ceiling((decimal)totalCount / pageSize);
				}
			}


			return new PagedResult<T>
			{
				PageIndex = specification.PageIndex,
				PageSize = specification.PageSize,
				Items = results,
				RowEnd = rowEnd,
				RowStart = rowStart,
				Specification = specification,
				Total = totalCount,
				TotalPages = pageCount
			};
		}

		public static Facet GetFacet<T>(this PagedResult<T> pagedResult, string facetName)
		{
			var facets = pagedResult.Facets;
			if (facets != null && facets.Any())
			{
				var facet = facets.Where(x => x.Name == facetName).FirstOrDefault();
				if (facet != null && facet.Values != null)
				{
					return facet;
				}
			}
			return null;
		}
	}
}
