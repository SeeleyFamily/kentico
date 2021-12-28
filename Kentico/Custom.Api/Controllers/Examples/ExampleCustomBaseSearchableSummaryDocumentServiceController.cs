using Custom.Core.Models.Summary;
using Custom.Core.Specifications.Examples;
using Custom.Infrastructure.Abstractions.Examples;
using Launchpad.Api.Controllers;
using System.Web.Http;
namespace Custom.Api.Controllers.Examples
{
	public class ExampleCustomBaseSearchableSummaryDocumentServiceController : ListingApiController<ExampleCustomSummaryItem, ExampleCustomDocumentSpecification, IExampleCustomBaseSearchableSummaryDocumentService>
	{

		public ExampleCustomBaseSearchableSummaryDocumentServiceController
		(
			IExampleCustomBaseSearchableSummaryDocumentService exampleCustomBaseSearchableSummaryDocumentService
		)
		{
			this.SearchableService = exampleCustomBaseSearchableSummaryDocumentService;
		}

		[HttpGet]
		public new IHttpActionResult Get([FromUri] ExampleCustomDocumentSpecification specification)
		{
			return base.Get(specification);
		}	
	}
}