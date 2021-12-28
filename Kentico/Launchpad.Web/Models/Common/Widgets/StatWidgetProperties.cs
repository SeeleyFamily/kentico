/*
 * Built with Common Launchpad 2.0.2
 */

using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using Launchpad.Web.Models.Custom.Widgets;

namespace Launchpad.Web.Models.Common.Widgets

{
	public class StatWidgetProperties : WidgetProperties, IWidgetProperties
	{
		[EditingComponent(DropDownComponent.IDENTIFIER, Order = 0, Label = "Background Style")]
		[EditingComponentProperty(nameof(DropDownProperties.DataSource), "primary;Primary\r\nsecondary;Secondary\r\ntertiary;Tertiary\r\nquaternary;Quaternary")]
		[EditingComponentProperty(nameof(DropDownProperties.DefaultValue), "primary")]
		public string BackgroundStyle { get; set; }

		[EditingComponent(TextInputComponent.IDENTIFIER, Order = 1, Label = "Stat")]
		public string Stat { get; set; } = "Stat";

		[EditingComponent(TextInputComponent.IDENTIFIER, Order = 2, Label = "Description Text")]
		public string Description { get; set; } = "<p>Description Text</p>";
	}

}