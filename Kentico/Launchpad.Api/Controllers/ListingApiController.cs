using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Abstractions.Specifications;
using System;
using System.Web.Http;


namespace Launchpad.Api.Controllers
{
	/// <summary>
	/// The listing api controller contains a <see cref=" TListingSummaryService"/> which exposes the following methods:
	/// <see cref="TListingSummaryService.Find(TSpecification)"/> for the full model
	/// <see cref="TListingSummaryService.GetSummaryItems(TSpecification)(TSpecification)"/> for the summary item model
	/// </summary>	
	public abstract class ListingApiController<T, TSpecification, TSearchableService> : ApiController
		where T : class
		where TSpecification : ISpecification
		where TSearchableService : ISearchableService<T, TSpecification>

	{
		#region Properties
		protected virtual TSearchableService SearchableService { get; set; }
		#endregion

		protected virtual IHttpActionResult Get(TSpecification specification)
		{
			try
			{
				return Ok(SearchableService.Find(specification));
			}
			catch (Exception e)
			{
				// TODO: Log?
				return InternalServerError(e);
			}
		}
	}

}
