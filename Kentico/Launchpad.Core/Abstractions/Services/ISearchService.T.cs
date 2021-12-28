using Launchpad.Core.Abstractions.Models.Document;
using Launchpad.Core.Abstractions.Models.Summary;
using Launchpad.Core.Abstractions.Specifications;
using Launchpad.Core.Models;
using Launchpad.Core.Models.Summary;
using System;



namespace Launchpad.Core.Abstractions.Services
{

	public interface ISearchService
	{
		/// <summary>
		/// Seaches an index for documents and returns a <see cref="PagedResult{T}"/> result.
		/// </summary>
		PagedResult<SummaryItem> Search( ISearchIndexSpecification specification );

		/// <summary>
		/// Seaches an index for documents and returns a <see cref="PagedResult{T}"/> result, using an optional mapping method.
		/// </summary>
		PagedResult<TSummaryItem> Search<TSummaryItem, TMapDto>(ISearchIndexSpecification specification, Func<TMapDto, TSummaryItem> mapper = null)
			where TSummaryItem : class, ISummaryItem, new()
			where TMapDto : class, IDocumentDto;
	}

}