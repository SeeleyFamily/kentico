using CMS.DocumentEngine;
using Launchpad.Core.Abstractions.Specifications;
using Launchpad.Core.Extensions;

namespace Launchpad.Infrastructure.Extensions.Query
{
	public static class ClassNameQueryExtensions
	{
		public static DocumentQuery<T> ApplyClassNameSpecification<T>(this DocumentQuery<T> query, IDocumentSpecification specification)
			where T : TreeNode, new()
		{
			if (specification == null)
			{
				return query;
			}

			// Excluded node ID filter
			if (!specification.ExcludedClassNames.IsNullOrEmpty())
			{
				query.WhereNotIn("ClassName", specification.ExcludedClassNames);
			}

			// Included node ID filter
			if (!specification.ClassNames.IsNullOrEmpty())
			{
				query.WhereIn("ClassName", specification.ClassNames);
			}

			return query;
		}

		public static MultiDocumentQuery ApplyClassNameSpecification(this MultiDocumentQuery query, IDocumentSpecification specification)
		{
			if (specification == null)
			{
				return query;
			}

			// Excluded node ID filter
			if (!specification.ExcludedClassNames.IsNullOrEmpty())
			{
				query.WhereNotIn("ClassName", specification.ExcludedClassNames);
			}

			// Included node ID filter
			if (!specification.ClassNames.IsNullOrEmpty())
			{
				query.WhereIn("ClassName", specification.ClassNames);
			}

			return query;
		}
	}
}

