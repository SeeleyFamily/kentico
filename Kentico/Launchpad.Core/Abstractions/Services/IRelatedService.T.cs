using Launchpad.Core.Models;
using Launchpad.Core.Specifications;
using System;
using System.Collections.Generic;

namespace Launchpad.Core.Abstractions.Services
{
	public interface IRelatedService<T> where T : PageNode
	{
		IEnumerable<T> GetRelatedDocuments(PageNode pageNode, IEnumerable<T> allPageNodes, int count, string path = "/", string[] includedClassNames = null, string[] excludedCategories = null, string[] specifiedCategoryCodeNamePaths = null);
		IEnumerable<T> GetRelatedDocuments(int nodeId, RelatedSpecification relatedSpecification, string[] categoryCodeNamePaths = null);
		IEnumerable<T> GetRelatedDocuments(Guid nodeGuid, RelatedSpecification relatedSpecification, string[] categoryCodeNamePaths = null);
		IEnumerable<T> GetRelatedDocuments(PageNode pageNode, RelatedSpecification relatedSpecification, string[] categoryCodeNamePaths = null);

	}
}
