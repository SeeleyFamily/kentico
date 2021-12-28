using Custom.Core.Models.Summary;
using Custom.Core.Specifications.Examples;
using Launchpad.Core.Abstractions.Services;

namespace Custom.Infrastructure.Abstractions.Examples
{
	public interface IExampleCustomSearchableSummaryDocumentService : ISearchableSummaryDocumentService<ExampleCustomSummaryItem, ExampleCustomDocumentSpecification>
	{
	}
}
