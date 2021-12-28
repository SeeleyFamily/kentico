using Custom.Core.Models.Summary;
using Custom.Core.Specifications.Examples;
using Custom.Infrastructure.Abstractions.Examples;
using Launchpad.Api.Controllers;
using System.Web.Http;

namespace Custom.Api.Controllers.Examples
{
	public class ExampleCustomInheritedSearchableSummaryDocumentServiceController : ListingApiController<ExampleCustomInheritedSummaryItem, ExampleCustomInheritedDocumentSpecification, IExampleCustomInheritedSearchableSummaryDocumentService>
	{

		public ExampleCustomInheritedSearchableSummaryDocumentServiceController
		(
			IExampleCustomInheritedSearchableSummaryDocumentService exampleCustomInheritedSearchableSummaryDocumentService
		)
		{
			this.SearchableService = exampleCustomInheritedSearchableSummaryDocumentService;
		}

		[HttpGet]
		public new IHttpActionResult Get([FromUri] ExampleCustomInheritedDocumentSpecification specification)
		{
			return base.Get(specification);
		}
	}
}