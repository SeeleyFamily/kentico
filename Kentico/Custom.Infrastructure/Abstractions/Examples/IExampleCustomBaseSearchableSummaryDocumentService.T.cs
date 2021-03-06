using Custom.Core.Models.Summary;
using Custom.Core.Specifications.Examples;
using Launchpad.Core.Abstractions.Services;

namespace Custom.Infrastructure.Abstractions.Examples
{
	public interface IExampleCustomBaseSearchableSummaryDocumentService<TSummaryItem, TDocumentSpecification> : ISearchableSummaryDocumentService<TSummaryItem, TDocumentSpecification>
		where TSummaryItem: ExampleCustomSummaryItem
		where TDocumentSpecification: ExampleCustomDocumentSpecification
	{
	}
}
