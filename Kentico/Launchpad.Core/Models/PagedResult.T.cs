using Launchpad.Core.Abstractions.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Launchpad.Core.Models
{

	public class PagedResult<T>
	{
		public int PageIndex;
		public int PageSize;
		public IEnumerable<Facet> Facets;
		public IEnumerable<T> Items;
		public int RowEnd;
		public int RowStart;
		public IPagedSpecification Specification;
		public int Total;
		public int TotalPages;
		
		public PagedResult()
		{

		}

		public PagedResult(IEnumerable<T> items, int total, IPagedSpecification pagedSpecification)
		{			
			PageIndex = pagedSpecification.PageIndex;
			PageSize = pagedSpecification.PageSize;
			Items = items;
			Specification = pagedSpecification;
			Total = total;

			// Compute paging data
			int rowStart = 0;
			int rowEnd = 0;
			int pageCount = 0;

			if (Items != null && Items.Any())
			{
				// Rows are 1-based (not 0-based like indices)
				rowStart = PageSize * PageIndex + 1;
				rowEnd = rowStart + Items.Count() - 1;

				if (PageSize > 0)
				{
					pageCount = (int)Math.Ceiling((decimal)total / PageSize);
				}
			}

			RowEnd = rowEnd;
			RowStart = rowStart;
			TotalPages = pageCount;
		}
	}

}
