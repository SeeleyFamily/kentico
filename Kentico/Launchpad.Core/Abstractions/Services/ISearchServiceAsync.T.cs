using Launchpad.Core.Abstractions.Models.Document;
using Launchpad.Core.Abstractions.Models.Summary;
using Launchpad.Core.Abstractions.Specifications;
using Launchpad.Core.Models;
using Launchpad.Core.Models.Summary;
using System;
using System.Threading.Tasks;


namespace Launchpad.Core.Abstractions.Services
{

	public interface ISearchServiceAsync : ISearchService
	{
		/// <summary>
		/// Asynchronously seaches an index for documents and returns a <see cref="PagedResult{T}"/> result.
		/// </summary>
		Task<PagedResult<SummaryItem>> SearchAsync( ISearchIndexSpecification specification );

		/// <summary>
		/// Asynchronously seaches an index for documents and returns a <see cref="PagedResult{T}"/> result, using an optional mapping method.
		/// </summary>
		Task<PagedResult<TSummaryItem>> SearchAsync<TSummaryItem, TMapDto>(ISearchIndexSpecification specification, Func<TMapDto, TSummaryItem> mapper = null)
			where TSummaryItem : class, ISummaryItem, new()
			where TMapDto : class, IDocumentDto;
	}

}