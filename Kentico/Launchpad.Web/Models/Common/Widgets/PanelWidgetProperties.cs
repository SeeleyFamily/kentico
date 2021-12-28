/*
 * Built with Common Launchpad 2.0.2
 */

using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using Launchpad.Web.Models.Custom.Widgets;

namespace Launchpad.Web.Models.Common.Widgets

{
	public class PanelWidgetProperties : WidgetProperties, IWidgetProperties
	{
		[EditingComponent(TextInputComponent.IDENTIFIER, Order = 1, Label = "Title")]
		public string Title { get; set; } = "Panel Title";

		public string Content { get; set; } = "<p>Description Text</p>";

	}

}