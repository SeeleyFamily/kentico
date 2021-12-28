using CMS.DocumentEngine;
using Launchpad.Core.Abstractions.Specifications;

namespace Launchpad.Infrastructure.Extensions.Query
{
    public static class NestingLevelQueryExtensions
    {
        public static DocumentQuery<T> ApplyNestingLevelSpecification<T>(this DocumentQuery<T> query, IDocumentSpecification specification)
            where T : TreeNode, new()
        {
            if (specification == null || specification.NestingLevel == 0)
            {
                return query;
            }

            return query.NestingLevel(specification.NestingLevel);
        }

        public static MultiDocumentQuery ApplyNestingLevelSpecification(this MultiDocumentQuery query, IDocumentSpecification specification)
        {
            if (specification == null || specification.NestingLevel == 0)
            {
                return query;
            }

            return query.NestingLevel(specification.NestingLevel);
        }
    }
}
