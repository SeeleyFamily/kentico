using CMS.DocumentEngine;
using Launchpad.Core.Abstractions.Specifications;

namespace Launchpad.Infrastructure.Extensions.Query
{
	public static class NodeLevelQueryExtensions
	{
		public static DocumentQuery<T> ApplyNodeLevelSpecification<T>(this DocumentQuery<T> query, IDocumentSpecification specification)
			where T : TreeNode, new()
		{
			if (specification == null)
			{
				return query;
			}

			if (specification.NodeLevel > 0)
			{
				query.WhereLessOrEquals("NodeLevel", specification.NodeLevel);
			}

			return query;
		}

		public static MultiDocumentQuery ApplyNodeLevelSpecification(this MultiDocumentQuery query, IDocumentSpecification specification)
		{
			if (specification == null)
			{
				return query;
			}

			if (specification.NodeLevel > 0)
			{
				query.WhereLessOrEquals("NodeLevel", specification.NodeLevel);
			}

			return query;
		}
	}
}
