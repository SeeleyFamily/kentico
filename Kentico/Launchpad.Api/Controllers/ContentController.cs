using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Models.Summary;
using Launchpad.Core.Specifications;
using System.Web.Http;


namespace Launchpad.Api.Controllers
{

	public class ContentController : ListingApiController<ContentSummaryItem, ContentSpecification, IContentService>
	{
		public ContentController
		(
			IContentService contentService
		)
		{
			this.SearchableService = contentService;
		}



		[HttpGet]
		public new IHttpActionResult Get([FromUri] ContentSpecification specification)
		{
			return base.Get(specification);
		}

	}

}
