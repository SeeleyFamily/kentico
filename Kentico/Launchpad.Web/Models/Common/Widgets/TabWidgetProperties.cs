/*
 * Built with Common Launchpad 2.0.2
 */

using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using Launchpad.Web.Models.Common.FormComponents;
using Launchpad.Web.Models.Custom.Widgets;

namespace Launchpad.Web.Models.Common.Widgets
{
	public class TabWidgetProperties : WidgetProperties, IWidgetProperties
	{
		[EditingComponent(NameListComponent.IDENTIFIER, Order = 0, Label = "Tab Names")]
		public string TabNames { get; set; }

		public int Index { get; set; }
	}
}