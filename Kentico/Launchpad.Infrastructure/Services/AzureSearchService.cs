using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Core.Abstractions.Models.Document;
using Launchpad.Core.Abstractions.Models.Summary;
using Launchpad.Core.Abstractions.Specifications;
using Launchpad.Core.Enums;
using Launchpad.Core.Extensions;
using Launchpad.Core.Models;
using Launchpad.Core.Models.Summary;
using Launchpad.Infrastructure.Abstractions.Factories;
using Launchpad.Infrastructure.Abstractions.Services;
using Launchpad.Infrastructure.Extensions;
using Launchpad.Infrastructure.Models.DataTransfer;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Microsoft.Rest.Azure;


namespace Launchpad.Infrastructure.Services
{

	public class AzureSearchService : IAzureSearchService, IPerApplicationService
    {
        #region Fields
        private readonly Lazy<IAzureSearchClientFactory> clientFactory;
        #endregion


        public AzureSearchService
        (
            Lazy<IAzureSearchClientFactory> clientFactory
        )
        {
            this.clientFactory = clientFactory;
        }


        public virtual SearchParameters CreateSearchParameters(ISearchIndexSpecification specification)
        {
            SearchParameters parameters = new SearchParameters
            {
                //Filter = string.Join( " and ", filters ),	// TODO: Specification filters
                IncludeTotalResultCount = true
            };


			// Facets?
			if( specification.Facets?.Any() ?? false )
			{
				parameters.Facets = specification.Facets.ToList();
			}


			// Filters?
			if( !String.IsNullOrWhiteSpace( specification.Filter ) )
			{
				parameters.Filter = specification.Filter;
			}


			// Paging?
			if( specification.PageSize > 0 )
			{
				parameters.Skip = Math.Max( 0, specification.PageIndex ) * specification.PageSize;
				parameters.Top = specification.PageSize;
			}


            return parameters;
        }


        /// <summary>
        /// Passthrough method to <see cref="Search(ISearchIndexSpecification)"/>.
        /// </summary>
        public PagedResult<SummaryItem> Find(ISearchIndexSpecification specification)
        {
            // Passthrough to Search method, use to supply ISearchableService<T, TSpecification> interface in IAzureService
            return Search(specification);
        }


		public virtual ISearchIndexClient GetClient( string indexName , bool isQueryOnly = true )
		{
			return clientFactory.Value.GetClient( indexName, isQueryOnly );
		}


		public virtual ISearchIndexClient GetClient( ISearchIndexSpecification specification )
		{
			return GetClient( specification.IndexName, isQueryOnly: true );
		}


        public virtual PagedResult<SummaryItem> Search(ISearchIndexSpecification specification)
        {
            return Search<SummaryItem, DocumentDto>(specification);
        }


        public virtual PagedResult<TSummaryItem> Search<TSummaryItem, TMapDto>(ISearchIndexSpecification specification, Func<TMapDto, TSummaryItem> mapper = null)
            where TSummaryItem : class, ISummaryItem, new()
            where TMapDto : class, IDocumentDto
        {
            // Avoid unnecessary service trips
            if (specification == null || String.IsNullOrWhiteSpace(specification.Query))
            {
                return new PagedResult<TSummaryItem>(Enumerable.Empty<TSummaryItem>(), 0, specification);
            }


            // Get the index's client
            ISearchIndexClient client = GetClient(specification);

            // Create search parameters
            SearchParameters searchParameters = CreateSearchParameters(specification);

            // Perform the search
            DocumentSearchResult<TMapDto> result = client.Documents.Search<TMapDto>(specification.Query, searchParameters);


            // Return the converted result items in a PagedResult object
            if (mapper == null)
            {
                // Default mapper
                return result.CreatePagedResult(specification, ToSummaryItem<TMapDto, TSummaryItem>);
            }

            return result.CreatePagedResult(specification, mapper);
        }


        public virtual async Task<PagedResult<SummaryItem>> SearchAsync(ISearchIndexSpecification specification)
        {
            return await SearchAsync<SummaryItem, DocumentDto>(specification);
        }


        public virtual async Task<PagedResult<TSummaryItem>> SearchAsync<TSummaryItem, TMapDto>(ISearchIndexSpecification specification, Func<TMapDto, TSummaryItem> mapper = null)
            where TSummaryItem : class, ISummaryItem, new()
            where TMapDto : class, IDocumentDto
        {
            // Avoid unnecessary service trips
            if (specification == null || String.IsNullOrWhiteSpace(specification.Query))
            {
                return new PagedResult<TSummaryItem>(Enumerable.Empty<TSummaryItem>(), 0, specification);
            }


            // Get the index's client
            ISearchIndexClient client = GetClient(specification);

            // Create search parameters
            SearchParameters searchParameters = CreateSearchParameters(specification);

            // Perform the search
            DocumentSearchResult<TMapDto> result = await client.Documents.SearchAsync<TMapDto>(specification.Query, searchParameters).ConfigureAwait(false);


            // Return the converted result items in a PagedResult object
            if (mapper == null)
            {
                // Default mapper
                return result.CreatePagedResult(specification, ToSummaryItem<TMapDto, TSummaryItem>);
            }

            return result.CreatePagedResult(specification, mapper);

        }


		public virtual async Task<Result> UploadDocuments<T>( IEnumerable<T> documents, string indexName )
			where T : UploadDocumentDto
		{
			ISearchIndexClient client = GetClient( indexName, isQueryOnly: false );


			// Set index name if necessary
			foreach( T document in documents.Where( d => string.IsNullOrWhiteSpace( d.Index ) ) )
			{
				document.Index = indexName;
			}


			// Create the batch of documents for merge/upload
			IndexBatch<T> batch = IndexBatch.MergeOrUpload( documents );

			try
			{
				// Push the documents up
				DocumentIndexResult indexResult = await client.Documents.IndexAsync( batch );

				// Return the response
				return new Result
				{
					ResultType = ResultType.Success
				};
			}
			catch( CloudException e )
			{
				// Return an error result
				return new Result
				{
					ResultType = ResultType.Error,
					Message = string.Join( Environment.NewLine, e.Body.Details.Select( ce => ce.Message ) )
				};
			}
			catch( Exception e )
			{
				// Return an error result
				return new Result
				{
					ResultType = ResultType.Error,
					Message = e.Message
				};
			}
		}


        protected virtual TSummaryItem ToSummaryItem<TMapDto, TSummaryItem>(SearchResult<TMapDto> item)
            where TMapDto : class, IDocumentDto
            where TSummaryItem : class, ISummaryItem, new()
        {
            TMapDto result = item.Document;

            TSummaryItem summary = new TSummaryItem
            {
                Id = result.Id,
                Image = result.Image,
                Summary = result.Summary.TruncateToWordWithEllipsis(180),
                Title = result.Title,
                Url = result.Url,
                Breadcrumbs = result.BreadcrumbString.ToBreadcrumbModel()
            };

            return summary;
        }        
    }

}