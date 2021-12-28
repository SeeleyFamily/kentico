using CMS.DocumentEngine.Types.Common;
using Custom.Core.Models.Summary;
using Custom.Core.Specifications.Examples;
using Custom.Infrastructure.Abstractions.Examples;
using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Models;
using Launchpad.Infrastructure.Extensions;
using Launchpad.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Custom.Infrastructure.Services.Examples
{

	public class ExampleCustomBaseSearchableSummaryDocumentService<TSummaryItem, TDocumentSpecification> :
		SearchableSummaryDocumentService<TSummaryItem, TDocumentSpecification>,
		IExampleCustomBaseSearchableSummaryDocumentService<TSummaryItem, TDocumentSpecification>,
		IPerScopeService
		where TSummaryItem : ExampleCustomSummaryItem, new()
		where TDocumentSpecification : ExampleCustomDocumentSpecification, new()
	{

		#region Fields
		private readonly IDocumentService<PeopleProfile> peopleProfileDocumentService;
		#endregion

		public ExampleCustomBaseSearchableSummaryDocumentService(
			ICategoryService categoryService,
			IDocumentService<PeopleProfile> peopleProfileDocumentService
		) : base(categoryService)
		{
			this.peopleProfileDocumentService = peopleProfileDocumentService;
		}

		public override IEnumerable<PageNode> ApplySpecifications(IEnumerable<PageNode> pageNodes, TDocumentSpecification specification)
		{
			if (!string.IsNullOrWhiteSpace(specification.CustomFilterProperty))
			{
				pageNodes = pageNodes.Where(x => x.Fields.GetStringValue(nameof(PeopleProfile.Title)).Equals(specification.CustomFilterProperty, StringComparison.InvariantCultureIgnoreCase));
			}
			return pageNodes;
		}

		public override IEnumerable<PageNode> GetPageNodes(TDocumentSpecification specification)
		{
			List<PageNode> pageNodes = new List<PageNode>();

			pageNodes.AddRange(peopleProfileDocumentService.Get().Select(x => x.ToPageNode()));

			return pageNodes;
		}

		public override TSummaryItem ToSummaryItem(PageNode pageNode)
		{
			var firstName = pageNode.Fields.GetStringValue(nameof(PeopleProfile.FirstName));
			var lastName = pageNode.Fields.GetStringValue(nameof(PeopleProfile.LastName));
			var personTitle = pageNode.Fields.GetStringValue(nameof(PeopleProfile.Title));
			var summaryItem = new TSummaryItem()
			{
				Title = $"{firstName} {lastName}",
				CustomSummaryItemProperty = personTitle,
			};
			return summaryItem;
		}
	}
}
