using CMS.DocumentEngine;
using Launchpad.Core.Abstractions.Specifications;
using Launchpad.Core.Extensions;

namespace Launchpad.Infrastructure.Extensions.Query
{
	public static class NodesQueryExtension
	{
		public static DocumentQuery<T> ApplyNodesSpecification<T>(this DocumentQuery<T> query, IDocumentSpecification specification)
			where T : TreeNode, new()
		{
			if (specification == null)
			{
				return query;
			}

			// Excluded node ID filter
			if (!specification.ExcludedNodes.IsNullOrEmpty())
			{
				query.WhereNotIn("NodeID", specification.ExcludedNodes);
			}

			// Included node ID filter
			if (!specification.Nodes.IsNullOrEmpty())
			{
				query.WhereIn("NodeID", specification.Nodes);
			}

			return query;
		}

		public static MultiDocumentQuery ApplyNodesSpecification(this MultiDocumentQuery query, IDocumentSpecification specification)
		{
			if (specification == null)
			{
				return query;
			}

			// Excluded node ID filter
			if (!specification.ExcludedNodes.IsNullOrEmpty())
			{
				query.WhereNotIn("NodeID", specification.ExcludedNodes);
			}

			// Included node ID filter
			if (!specification.Nodes.IsNullOrEmpty())
			{
				query.WhereIn("NodeID", specification.Nodes);
			}

			return query;
		}
	}
}
