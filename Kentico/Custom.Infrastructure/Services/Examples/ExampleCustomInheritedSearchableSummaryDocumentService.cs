using CMS.DocumentEngine.Types.Common;
using Custom.Core.Models.Summary;
using Custom.Core.Specifications.Examples;
using Custom.Infrastructure.Abstractions.Examples;
using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Models;
using Launchpad.Infrastructure.Extensions;

namespace Custom.Infrastructure.Services.Examples
{

	public class ExampleCustomInheritedSearchableSummaryDocumentService :
		ExampleCustomBaseSearchableSummaryDocumentService<ExampleCustomInheritedSummaryItem, ExampleCustomInheritedDocumentSpecification>,
		IExampleCustomInheritedSearchableSummaryDocumentService,
		IPerScopeService
	{

		public ExampleCustomInheritedSearchableSummaryDocumentService(
			ICategoryService categoryService,
			IDocumentService<PeopleProfile> peopleProfileDocumentService
		) : base(categoryService, peopleProfileDocumentService)
		{		
		}

		public override ExampleCustomInheritedSummaryItem ToSummaryItem(PageNode pageNode)
		{
			var summaryItem = base.ToSummaryItem(pageNode);

			var firstName = pageNode.Fields.GetStringValue(nameof(PeopleProfile.FirstName));
			var lastName = pageNode.Fields.GetStringValue(nameof(PeopleProfile.LastName));
			summaryItem.CustomSummaryItemProperty2 = $"{lastName} {firstName}";

			return summaryItem;
		}

	}
}
