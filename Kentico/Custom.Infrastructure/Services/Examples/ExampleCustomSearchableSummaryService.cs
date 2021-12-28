using Custom.Core.Models.Summary;
using Custom.Core.Specifications.Examples;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Models;

namespace Custom.Infrastructure.Services.Examples
{
	// Searchable Summary Service is exactly the same as the Searchable Service
	// Except the return model must be an implementation of ISummaryItem	
	class ExampleCustomSearchableSummaryService : ISearchableSummaryService<ExampleCustomSummaryItem, ExampleCustomSpecification>
	{
		public PagedResult<ExampleCustomSummaryItem> Find(ExampleCustomSpecification specification)
		{
			// For these base level services, the implementation is up to the developer
			throw new System.NotImplementedException();
		}
	}
}
