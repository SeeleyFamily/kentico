using Launchpad.Core.Models.Summary;
using Launchpad.Core.Specifications;


namespace Launchpad.Core.Abstractions.Services
{

	public interface IPeopleService<TSummaryItem, TDocumentSpecification> : ISearchableSummaryDocumentService<TSummaryItem, TDocumentSpecification>
		where TSummaryItem : PeopleSummaryItem
		where TDocumentSpecification : PeopleSpecification
	{
	}

}
