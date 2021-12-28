using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Models.Summary;
using Launchpad.Core.Specifications;
using System.Web.Http;


namespace Launchpad.Api.Controllers
{

	public class BlogController : ListingApiController<BlogSummaryItem, BlogSpecification, IBlogService>
	{


		public BlogController
		(
			IBlogService blogService
		)
		{
			this.SearchableService = blogService;
		}



		[HttpGet]
		public new IHttpActionResult Get([FromUri] BlogSpecification specification)
		{
			return base.Get(specification);
		}


	}

}
