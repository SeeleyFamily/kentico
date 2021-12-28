/*
 * Built with Common Launchpad 2.0.2
 */

using CMS.DocumentEngine.Types.Common;
using Launchpad.Core.Abstractions.Providers;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Models.Summary;
using Launchpad.Core.Specifications;
using Launchpad.Infrastructure.Kentico.Web.Attributes;

namespace Launchpad.Web.Models.Common.ViewModels
{

	[RegisterForPageType(EventListing.CLASS_NAME)]
	public class EventListingViewModel : EventListingViewModel<EventSummaryItem, EventSpecification, IEventService>
	{


		public EventListingViewModel
		(
			ILayoutProvider layoutProvider,
			IEventService eventService
		)
			: base(layoutProvider, eventService)
		{
			SearchableService = eventService;
		}


		protected override void PopulateSpecification()
		{
			Specification = new EventSpecification(HttpContext.Request.QueryString)
			{
				Path = Node.NodeAliasPath,
				IncludeUpcoming = true,
				IncludePast = true,
			};
		}


	}

}
