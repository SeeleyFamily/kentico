/*
 * Built with Common Launchpad 2.0.2
 */

using Kentico.Forms.Web.Mvc;
using System;

namespace Launchpad.Web.Models.Common.Widgets
{
	public class BaseWidgetProperties
	{		
		[EditingComponent(DropDownComponent.IDENTIFIER, Order = 100, Label = "Add Widget Padding")]
		[EditingComponentProperty(nameof(DropDownProperties.DataSource), ";None\r\n1;Both (Top and bottom)\r\n2;Padding Top\r\n3;Padding Bottom")]
		[EditingComponentProperty(nameof(DropDownProperties.DefaultValue), "")]
		public virtual string AddWidgetPadding { get; set; }

		public virtual string WidgetClass()
		{
			string paddingType = "";
			switch (AddWidgetPadding)
			{
				case "1":
					paddingType = "widget-spacing";
					break;
				case "2":
					paddingType = "widget-spacing no-margin-bottom";
					break;
				case "3":
					paddingType = "widget-spacing no-margin-top";
					break;
				default:
					break;
			}

			string widgetVariantClass = !string.IsNullOrWhiteSpace(WidgetVariant) ? $"variant {WidgetVariant}" : string.Empty;

			string wc = String.Join(" ", new string[] { paddingType, widgetVariantClass });
			return wc;			
		}

		public virtual string WidgetVariant { get; set; } = string.Empty;
	}
}