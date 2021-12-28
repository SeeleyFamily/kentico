using Microsoft.Azure.Search;


namespace Launchpad.Infrastructure.Abstractions.Factories
{

	public interface IAzureSearchClientFactory
	{
		ISearchIndexClient GetClient( string indexName, bool isQueryOnly = true );
	}

}
