using CMS.DocumentEngine.Types.Common;
using Custom.Core.Models.Summary;
using Custom.Core.Specifications.Examples;
using Custom.Infrastructure.Abstractions.Examples;
using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Core.Abstractions.Services;

namespace Custom.Infrastructure.Services.Examples
{

	public class ExampleCustomBaseSearchableSummaryDocumentService :
		ExampleCustomBaseSearchableSummaryDocumentService<ExampleCustomSummaryItem, ExampleCustomDocumentSpecification>,
		IExampleCustomBaseSearchableSummaryDocumentService,
		IPerScopeService		
	{
		public ExampleCustomBaseSearchableSummaryDocumentService(			
			ICategoryService categoryService,
			IDocumentService<PeopleProfile> peopleProfileDocumentService		
		) : base(categoryService, peopleProfileDocumentService)
		{
		}

		
	}
}
