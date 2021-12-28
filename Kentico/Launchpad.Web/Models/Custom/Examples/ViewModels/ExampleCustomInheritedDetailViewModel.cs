using CMS.DocumentEngine.Types.Common;
using Launchpad.Core.Abstractions.Providers;
using Launchpad.Infrastructure.Kentico.Web.Attributes;
using Launchpad.Web.Models.Common.ViewModels;

namespace Launchpad.Web.Models.Custom.Examples.ViewModels
{
	// This example is used to show how to add small changes to an existing common page type / commmon page type view model

	// In order to register this view model with the page type class name, remove the following line
	// The base / inherited class already has this registration attribute
	[RegisterForPageType(PeopleProfile.CLASS_NAME + "_REMOVE_THIS_")]
	public class ExampleCustomInheritedDetailViewModel : PeopleProfileViewModel
	{
		#region Properties				
		public override string ViewPath { get; protected set; } = $"PeopleProfile/Index";  // Override the View Path here or use the View Engine Custom Path to override.
		public string CustomDetailProperty { get; protected set; }
		#endregion

		public ExampleCustomInheritedDetailViewModel
		(
			ILayoutProvider layoutProvider
		)
		: base(layoutProvider)
		{
		
		}

		protected override void Populate()
		{
			base.Populate();
			PopulateCustomDetailProperty();
		}

		protected void PopulateCustomDetailProperty()
		{
			CustomDetailProperty = "Custom Detail Property is set here.";
		}
	}
}