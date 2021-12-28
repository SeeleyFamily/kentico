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

	public class EventService<TSummaryItem, TDocumentSpecification> :
		SearchableSummaryDocumentService<TSummaryItem, TDocumentSpecification>,
		IEventService<TSummaryItem, TDocumentSpecification>,
		IPerScopeService
		where TSummaryItem : EventSummaryItem, new()
		where TDocumentSpecification : EventSpecification, new()
	{


		#region Fields
		private readonly IDocumentService<EventDetail> eventDetailDocumentService;
		#endregion


		public EventService(
			ICategoryService categoryService,
			IDocumentService<EventDetail> eventDetailDocumentService
		) : base(categoryService)
		{
			this.eventDetailDocumentService = eventDetailDocumentService;
		}


		public override IEnumerable<PageNode> GetPageNodes(TDocumentSpecification specification)
		{
			List<PageNode> pageNodes = new List<PageNode>();

			pageNodes.AddRange(eventDetailDocumentService.Get().Select(x => x.ToPageNode()));

			return pageNodes;
		}


		public override TSummaryItem ToSummaryItem(PageNode pageNode)
		{
			var summaryItem = base.ToSummaryItem(pageNode);
			return summaryItem;
		}


	}

}
