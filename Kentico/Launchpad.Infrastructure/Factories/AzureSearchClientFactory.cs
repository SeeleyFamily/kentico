using System.Collections.Generic;
using CMS.Search;
using CMS.Search.Azure;
using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Infrastructure.Abstractions.Factories;
using Microsoft.Azure.Search;


namespace Launchpad.Infrastructure.Factories
{

	public class AzureSearchClientFactory : IAzureSearchClientFactory, IPerApplicationService
	{
		#region Fields
		private readonly IDictionary<string, ISearchIndexClient> clientDictionary;
		#endregion


		public AzureSearchClientFactory
		(
			
		)
		{
			clientDictionary = new Dictionary<string, ISearchIndexClient>();
		}



		public ISearchIndexClient GetClient( string indexName, bool isQueryOnly = true )
		{
			string cacheKey = $"{indexName}-{( isQueryOnly ? "read" : "write" )}";


			// Get the client from cache?
			if( clientDictionary.ContainsKey( cacheKey ))
			{
				return clientDictionary[ cacheKey ];
			}



			// Find the index in Kentico
			SearchIndexInfo index = SearchIndexInfoProvider.GetSearchIndexInfo( indexName );

			if( index == null )
			{
				return null;
			}


			// Which key to use depends on read/write access
			string key = isQueryOnly ? index.IndexQueryKey : index.IndexAdminKey;


			// Create, cache and return the search client
			ISearchIndexClient client = new SearchIndexClient( index.IndexSearchServiceName, NamingHelper.GetValidIndexName( indexName ), new SearchCredentials( key ) );
			clientDictionary.Add( cacheKey, client );

			return client;
		}

	}

}
