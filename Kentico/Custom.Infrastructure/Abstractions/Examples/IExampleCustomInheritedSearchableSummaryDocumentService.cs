using Custom.Core.Models.Summary;
using Custom.Core.Specifications.Examples;

namespace Custom.Infrastructure.Abstractions.Examples
{
	public interface IExampleCustomInheritedSearchableSummaryDocumentService : IExampleCustomBaseSearchableSummaryDocumentService<ExampleCustomInheritedSummaryItem, ExampleCustomInheritedDocumentSpecification>
	{
	}
}
