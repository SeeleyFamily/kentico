using Custom.Core.Models.Summary;
using Custom.Core.Specifications.Examples;
using Custom.Infrastructure.Abstractions.Examples;
using Launchpad.Api.Controllers;
using System.Web.Http;

namespace Custom.Api.Controllers.Examples
{
	public class ExampleCustomSearchableSummaryDocumentServiceController : ListingApiController<ExampleCustomSummaryItem, ExampleCustomDocumentSpecification, IExampleCustomSearchableSummaryDocumentService>
	{

		public ExampleCustomSearchableSummaryDocumentServiceController
		(
			IExampleCustomSearchableSummaryDocumentService exampleCustomSearchableSummaryDocumentService
		)
		{
			this.SearchableService = exampleCustomSearchableSummaryDocumentService;
		}

		[HttpGet]
		public new IHttpActionResult Get([FromUri] ExampleCustomDocumentSpecification specification)
		{
			return base.Get(specification);
		}
	}
}