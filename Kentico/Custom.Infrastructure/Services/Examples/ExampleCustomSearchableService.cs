using Custom.Core.Models.Examples;
using Custom.Core.Specifications.Examples;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Models;

namespace Custom.Infrastructure.Services.Examples
{
	// Searchable Service is the most generic interface available for usage...
	// It is the base interface for a service class that will find and return paged results of any type of model...
	// It is typically used for any type of listing and its respective api controller...
	// The generic type has no constraints

	// We typically do not use this implementation as the majority of our implementations will be using one of the other sub types
	// You should not use this base implementation if the content source are Kentico Page Types

	// Common usages
	// We may use ISearchableService when creating a custom searchable service for perhaps consuming data from an external API..

	// Other usages
	// It can also be used for Kentico Custom Data Tables or Modules (if they need to be searchable) but likely not recommended...
	class ExampleCustomSearchableService : ISearchableService<ExampleCustomModel, ExampleCustomSpecification>
	{
		public PagedResult<ExampleCustomModel> Find(ExampleCustomSpecification specification)
		{
			// For these base level services, the implementation is up to the developer
			throw new System.NotImplementedException();
		}
	}
}
