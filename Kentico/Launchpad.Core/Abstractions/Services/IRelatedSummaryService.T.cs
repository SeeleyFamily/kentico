using Launchpad.Core.Abstractions.Models.Summary;
using Launchpad.Core.Models;
using Launchpad.Core.Specifications;
using System;
using System.Collections.Generic;

namespace Launchpad.Core.Abstractions.Services
{
	public interface IRelatedSummaryService<T> where T : ISummaryItem
	{
		IEnumerable<int> GetRelatedDocumentIds(int documentId, int count, string path = "/", string[] includedClassNames = null, string[] excludedCategories = null);
		IEnumerable<PageNode> GetRelatedDocuments(int documentId, int count, string path = "/", string[] includedClassNames = null, string[] excludedCategories = null);
		IEnumerable<PageNode> GetRelatedDocuments(int nodeId, RelatedSpecification relatedSpecification, string[] categoryCodeNamePaths = null);
		IEnumerable<PageNode> GetRelatedDocuments(Guid nodeGuid, RelatedSpecification relatedSpecification, string[] categoryCodeNamePaths = null);
		IEnumerable<PageNode> GetRelatedDocuments(PageNode pageNode, RelatedSpecification relatedSpecification, string[] categoryCodeNamePaths = null);
		IEnumerable<T> GetRelatedSummaryItems(int nodeId, RelatedSpecification relatedSpecification, string[] categoryCodeNamePaths = null);
		IEnumerable<T> GetRelatedSummaryItems(Guid nodeGuid, RelatedSpecification relatedSpecification, string[] categoryCodeNamePaths = null);
		IEnumerable<T> GetRelatedSummaryItems(PageNode pageNode, RelatedSpecification relatedSpecification, string[] categoryCodeNamePaths = null);
		IEnumerable<T> ToSummaryItems(IEnumerable<PageNode> pageNodes);
	}
}
