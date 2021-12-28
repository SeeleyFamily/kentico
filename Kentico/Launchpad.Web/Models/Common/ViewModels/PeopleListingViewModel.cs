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

	[RegisterForPageType(PeopleListing.CLASS_NAME)]
	public class PeopleListingViewModel : PeopleListingViewModel<PeopleSummaryItem, PeopleSpecification, IPeopleService>
	{


		public PeopleListingViewModel
		(
			ILayoutProvider layoutProvider,
			IPeopleService peopleService
		)
			: base(layoutProvider, peopleService)
		{
			SearchableService = peopleService;
		}


		protected override void PopulateSpecification()
		{
			Specification = new PeopleSpecification(HttpContext.Request.QueryString)
			{
				Path = Node.NodeAliasPath,
			};
		}


	}

}
