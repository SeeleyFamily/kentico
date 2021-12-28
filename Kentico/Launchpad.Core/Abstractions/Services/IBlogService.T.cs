using Launchpad.Core.Models.Summary;
using Launchpad.Core.Specifications;


namespace Launchpad.Core.Abstractions.Services
{

	public interface IBlogService<TSummaryItem, TDocumentSpecification> : ISearchableSummaryDocumentService<TSummaryItem, TDocumentSpecification>
		where TSummaryItem : BlogSummaryItem
		where TDocumentSpecification : BlogSpecification
	{


	}

}
