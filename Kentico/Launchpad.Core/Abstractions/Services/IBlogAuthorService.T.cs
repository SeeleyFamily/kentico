using Launchpad.Core.Models.Summary;
using Launchpad.Core.Specifications;


namespace Launchpad.Core.Abstractions.Services
{

	public interface IBlogAuthorService<TSummaryItem, TDocumentSpecification> : ISearchableSummaryDocumentService<TSummaryItem, TDocumentSpecification>
		where TSummaryItem : BlogAuthorSummaryItem
		where TDocumentSpecification : BlogAuthorSpecification
	{


	}

}
