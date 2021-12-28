using System.Collections.Generic;
using System.Threading.Tasks;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Abstractions.Specifications;
using Launchpad.Core.Models;
using Launchpad.Core.Models.Summary;
using Launchpad.Infrastructure.Models.DataTransfer;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;


namespace Launchpad.Infrastructure.Abstractions.Services
{

	public interface IAzureSearchService : ISearchServiceAsync, ISearchableService<SummaryItem, ISearchIndexSpecification>
	{
		SearchParameters CreateSearchParameters( ISearchIndexSpecification specification );
		ISearchIndexClient GetClient( ISearchIndexSpecification specification );
		Task<Result> UploadDocuments<T>( IEnumerable<T> documents, string indexName ) where T : UploadDocumentDto;
	}

}
