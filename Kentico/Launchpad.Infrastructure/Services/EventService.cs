using CMS.DocumentEngine.Types.Common;
using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Models.Summary;
using Launchpad.Core.Specifications;


namespace Launchpad.Infrastructure.Services
{

	public class EventService : EventService<EventSummaryItem, EventSpecification>,
		IEventService,
		IPerScopeService
	{


		public EventService(

			ICategoryService categoryService,
			IDocumentService<EventDetail> eventDetailDocumentService
		) : base(categoryService, eventDetailDocumentService)
		{
		}


	}

}