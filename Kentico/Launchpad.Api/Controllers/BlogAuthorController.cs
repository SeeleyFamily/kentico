using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Models.Summary;
using Launchpad.Core.Specifications;
using System.Web.Http;


namespace Launchpad.Api.Controllers
{

	public class BlogAuthorController : ListingApiController<BlogAuthorSummaryItem, BlogAuthorSpecification, IBlogAuthorService>
	{


		public BlogAuthorController
		(
			IBlogAuthorService blogAuthorService
		)
		{
			this.SearchableService = blogAuthorService;
		}



		[HttpGet]
		public new IHttpActionResult Get([FromUri] BlogAuthorSpecification specification)
		{
			return base.Get(specification);
		}


	}

}
