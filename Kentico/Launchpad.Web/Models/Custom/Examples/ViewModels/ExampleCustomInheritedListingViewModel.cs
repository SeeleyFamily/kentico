using CMS.DocumentEngine.Types.Common;
using Launchpad.Core.Abstractions.Providers;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Infrastructure.Kentico.Web.Attributes;
using Launchpad.Web.Models.Common.ViewModels;

namespace Launchpad.Web.Models.Custom.Examples.ViewModels
{
	// In order to register this view model with the page type class name, remove the following line
	// The base / inherited class already has this registration attribute
	[RegisterForPageType(PeopleListing.CLASS_NAME + "_Remove_This_")]
	public class ExampleCustomInheritedListingViewModel : PeopleListingViewModel
	{
		#region Properties				
		public override string ViewPath { get; protected set; } = $"_Update_This_/Index"; // Override the View Path here or use the View Engine Custom Path to override.
		public string CustomListingProperty { get; protected set; }
		#endregion

		public ExampleCustomInheritedListingViewModel
		(
			ILayoutProvider layoutProvider,
			IPeopleService peopleService
		)
		: base(layoutProvider, peopleService)
		{
			SearchableService = peopleService;
		}

		protected override void Populate()
		{
			base.Populate();
			PopulateCustomListingProperty();
		}

		protected void PopulateCustomListingProperty()
		{
			CustomListingProperty = "Custom Listing Property is set here.";
		}
	}
}