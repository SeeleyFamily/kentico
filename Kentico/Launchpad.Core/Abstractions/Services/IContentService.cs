using Launchpad.Core.Models.Summary;
using Launchpad.Core.Specifications;


namespace Launchpad.Core.Abstractions.Services
{
	public interface IContentService : ISearchableSummaryDocumentService<ContentSummaryItem, ContentSpecification>
	{
	}
}
