using CMS.DocumentEngine.Types.Common;
using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Models;
using Launchpad.Core.Models.Summary;
using Launchpad.Core.Specifications;
using Launchpad.Infrastructure.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Launchpad.Infrastructure.Services
{

	public class PeopleService<TSummaryItem, TDocumentSpecification> :
		SearchableSummaryDocumentService<TSummaryItem, TDocumentSpecification>,
		IPeopleService<TSummaryItem, TDocumentSpecification>,
		IPerScopeService
		where TSummaryItem : PeopleSummaryItem, new()
		where TDocumentSpecification : PeopleSpecification, new()
	{


		#region Fields
		private readonly IDocumentService<PeopleProfile> peopleProfileDocumentService;
		#endregion


		public PeopleService(
			ICategoryService categoryService,
			IDocumentService<PeopleProfile> peopleProfileDocumentService
		) : base(categoryService)
		{
			this.peopleProfileDocumentService = peopleProfileDocumentService;
		}


		public override IEnumerable<PageNode> GetPageNodes(TDocumentSpecification specification)
		{
			List<PageNode> pageNodes = new List<PageNode>();

			pageNodes.AddRange(peopleProfileDocumentService.Get().Select(x => x.ToPageNode()));

			return pageNodes;
		}


		public override TSummaryItem ToSummaryItem(PageNode pageNode)
		{
			var summaryItem = base.ToSummaryItem(pageNode);

			var peopleTitle = pageNode.Fields.GetStringValue(nameof(PeopleProfile.Title));
			summaryItem.PeopleTitle = peopleTitle;

			return summaryItem;
		}


	}

}
