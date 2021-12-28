using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Models;
using Launchpad.Core.Models.Summary;
using Launchpad.Core.Specifications;
using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Http;


namespace Launchpad.Api.Controllers
{

	public class SearchController : ApiController
	{
		#region Fields
		private readonly ISearchServiceAsync service;
		#endregion


		public SearchController
		(
			ISearchServiceAsync service
		)
		{
			this.service = service;
		}



		[HttpGet]
		public async Task<IHttpActionResult> Get( [FromUri] SearchIndexSpecification specification )
		{
			specification.IndexName = ConfigurationManager.AppSettings[ "Search:Index:Global" ];

			try
			{
				PagedResult<SummaryItem> result = await service.SearchAsync( specification );
				return Ok( result );
			}
			catch( Exception e )
			{
				// TODO: Log?
				return InternalServerError( e );
			}
		}

	}

}
