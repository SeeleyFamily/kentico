using System;
using System.Threading.Tasks;
using System.Web.Http;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Models;
using Launchpad.Core.Specifications;


namespace Launchpad.Api.Controllers
{

	public class MapController : ApiController
	{
		#region Fields
		private readonly IGoogleMapService service;
		#endregion


		public MapController
		(
			IGoogleMapService service
		)
		{
			this.service = service;
		}



		[HttpGet]
		public async Task<IHttpActionResult> Get( [FromUri] QuerySpecification specification )
		{
			if( String.IsNullOrWhiteSpace( specification.Query ) )
			{
				return BadRequest();
			}


			try
			{
				MapLocation result = await service.GetMapLocation( specification );
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
