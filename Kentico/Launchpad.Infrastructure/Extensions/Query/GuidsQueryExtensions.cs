
using CMS.DocumentEngine;
using Launchpad.Core.Abstractions.Specifications;
using Launchpad.Core.Extensions;

namespace Launchpad.Infrastructure.Extensions.Query
{
	public static class GuidsQueryExtensions
	{
		public static DocumentQuery<T> ApplyGuidsSpecification<T>(this DocumentQuery<T> query, IDocumentSpecification specification)
			where T : TreeNode, new()
		{
			if (specification == null)
			{
				return query;
			}

			// Excluded node GUID filter
			if (!specification.ExcludedGuids.IsNullOrEmpty())
			{
				query.WhereNotIn("NodeGUID", specification.ExcludedGuids);
			}

			// Included node GUID filter
			if (!specification.Guids.IsNullOrEmpty())
			{
				query.WhereIn("NodeGUID", specification.Guids);
			}

			return query;
		}

		public static MultiDocumentQuery ApplyGuidsSpecification(this MultiDocumentQuery query, IDocumentSpecification specification)
		{
			if (specification == null)
			{
				return query;
			}

			// Excluded node GUID filter
			if (!specification.ExcludedGuids.IsNullOrEmpty())
			{
				query.WhereNotIn("NodeGUID", specification.ExcludedGuids);
			}

			// Included node GUID filter
			if (!specification.Guids.IsNullOrEmpty())
			{
				query.WhereIn("NodeGUID", specification.Guids);
			}

			return query;
		}
	}
}
