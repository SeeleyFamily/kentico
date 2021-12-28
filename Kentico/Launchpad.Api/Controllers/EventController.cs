using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Models.Summary;
using Launchpad.Core.Specifications;
using System.Web.Http;


namespace Launchpad.Api.Controllers
{
	public class EventController : ListingApiController<EventSummaryItem, EventSpecification, IEventService>
	{
		public EventController
		(
			IEventService eventService
		)
		{
			this.SearchableService = eventService;
		}

		[HttpGet]
		public new IHttpActionResult Get([FromUri] EventSpecification specification)
		{
			return base.Get(specification);
		}
	}
}
