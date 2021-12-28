using Launchpad.Core.Abstractions.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launchpad.Core.Extensions
{
	public static class PagedSpecificationExtensions
	{
		private const int maxPageSize = 1000000;

		public static void Validate(this IPagedSpecification specification)
		{
			if (specification.PageIndex < 0)
			{
				specification.PageIndex = 0;
			}

			if (specification.PageSize < 1)
			{
				specification.PageSize = 1;
			}

			if (specification.PageSize > maxPageSize)
			{
				specification.PageSize = maxPageSize;
			}
		}
	}
}
