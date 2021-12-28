using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Models.Summary;
using Launchpad.Core.Specifications;
using System.Web.Http;


namespace Launchpad.Api.Controllers
{
	public class PeopleController : ListingApiController<PeopleSummaryItem, PeopleSpecification, IPeopleService>
	{
		public PeopleController
		(
			IPeopleService peopleService
		)
		{
			this.SearchableService = peopleService;
		}

		[HttpGet]
		public new IHttpActionResult Get([FromUri] PeopleSpecification specification)
		{
			return base.Get(specification);
		}
	}
}
