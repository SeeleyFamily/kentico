using Launchpad.Core.Models.Summary;
using Launchpad.Core.Specifications;

namespace Launchpad.Core.Abstractions.Services
{
	public interface IContentService<TSummaryItem, TDocumentSpecification> : ISearchableSummaryDocumentService<TSummaryItem, TDocumentSpecification>
		where TSummaryItem : ContentSummaryItem
		where TDocumentSpecification : ContentSpecification
	{
	}  
}
