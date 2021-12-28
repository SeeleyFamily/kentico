using Launchpad.Core.Models.Summary;
using Launchpad.Core.Specifications;


namespace Launchpad.Core.Abstractions.Services
{

	public interface IEventService<TSummaryItem, TDocumentSpecification> : ISearchableSummaryDocumentService<TSummaryItem, TDocumentSpecification>
		where TSummaryItem : EventSummaryItem
		where TDocumentSpecification : EventSpecification
	{
	}

}
