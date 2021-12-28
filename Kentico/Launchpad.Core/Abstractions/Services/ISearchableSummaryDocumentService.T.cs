using Launchpad.Core.Abstractions.Models.Summary;
using Launchpad.Core.Abstractions.Specifications;
using Launchpad.Core.Models;
using System.Collections.Generic;

namespace Launchpad.Core.Abstractions.Services
{

	/// <summary>
	/// An interface which provides strict requirements on the implementation of a searchable service.
	/// A searchable service takes a specification and returns a paged result with a set of items <see cref="TSummaryItem"/>
	/// The implementation must handle the getting, filtering, and paging of the specified resulting set.
	/// </summary>
	public interface ISearchableSummaryDocumentService<TSummaryItem, TDocumentSpecification>
		: ISearchableSummaryService<TSummaryItem, TDocumentSpecification>
		where TSummaryItem : ISummaryItem
		where TDocumentSpecification : IDocumentSpecification
	{
		// Gets all page nodes
		IEnumerable<PageNode> GetPageNodes();

		// Gets all summary items
		IEnumerable<TSummaryItem> GetSummaryItems();

		new PagedResult<TSummaryItem> Find(TDocumentSpecification specification);

		IEnumerable<PageNode> GetPageNodes(TDocumentSpecification specification);

		IEnumerable<PageNode> ApplySpecifications(IEnumerable<PageNode> pageNodes, TDocumentSpecification specification);

		IEnumerable<PageNode> ApplySortSpecifications(IEnumerable<PageNode> pageNodes, TDocumentSpecification specification);

		TSummaryItem ToSummaryItem(PageNode pageNode);
	}
}
