using CMS.DocumentEngine;
using Launchpad.Core.Abstractions.Specifications;
using Launchpad.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Launchpad.Infrastructure.Extensions.Query
{
	public static class PathQueryExtensions
	{
		public static DocumentQuery<T> ApplyPathSpecification<T>(this DocumentQuery<T> query, IPathSpecification specification)
			where T : TreeNode, new()
		{
			if (specification == null)
			{
				return query;
			}

			// Children path filter
			if (!String.IsNullOrWhiteSpace(specification.Path))
			{
				PathTypeEnum pathType = specification.IncludeDocumentForPath ? PathTypeEnum.Section : PathTypeEnum.Children;
				query.Path(specification.Path, PathTypeEnum.Children);
			}

			return query;
		}

		public static MultiDocumentQuery ApplyPathSpecification(this MultiDocumentQuery query, IPathSpecification specification)
		{
			if (specification == null)
			{
				return query;
			}

			// Children path filter
			if (!String.IsNullOrWhiteSpace(specification.Path))
			{
				PathTypeEnum pathType = specification.IncludeDocumentForPath ? PathTypeEnum.Section : PathTypeEnum.Children;
				query.Path(specification.Path, pathType);
			}

			return query;
		}

		public static IEnumerable<T> ApplyPathSpecification<T>(this IEnumerable<T> result, IPathSpecification specification)
			where T : PageNode, new()
		{
			if (specification == null)
			{
				return result;
			}
			// Children path filter
			if (!string.IsNullOrWhiteSpace(specification.Path))
			{
				result = result.Where(x => !string.IsNullOrWhiteSpace(x.NodeAliasPath) && x.NodeAliasPath.ToLower().StartsWith(specification.Path.ToLower()));
			}

			return result;
		}

	}
}
