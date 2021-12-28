/*
 * Built with Common Launchpad 2.0.2
 */

using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using System;

namespace Launchpad.Web.Models.Common.Sections
{
	public class BaseSectionProperties : ISectionProperties
	{
		public virtual string[] AreaClasses { get; set; } = { "col-12" };
		public virtual string SectionModifier { get; set; } = "";
		public virtual bool UseRow { get; set; } = true;

		[EditingComponent(TextInputComponent.IDENTIFIER, Order = 0, Label = "Id/Anchor")]
		[EditingComponentProperty(nameof(TextInputProperties.DefaultValue), "")]
		public virtual string Id { get; set; }

		[EditingComponent(DropDownComponent.IDENTIFIER, Order = 1, Label = "Background Class")]
		[EditingComponentProperty(nameof(DropDownProperties.DataSource), ";None\r\nbg-primary;Primary\r\nbg-secondary;Secondary\r\nbg-tertiary;Tertiary\r\nbg-quaternary;Quaternary")]
		[EditingComponentProperty(nameof(DropDownProperties.DefaultValue), "")]
		public virtual string BackgroundClass { get; set; }

		[EditingComponent(CheckBoxComponent.IDENTIFIER, Order = 5, Label = "Full Width")]
		[EditingComponentProperty(nameof(CheckBoxProperties.DefaultValue), false)]
		public virtual bool FullWidth { get; set; }

		[EditingComponent(TextInputComponent.IDENTIFIER, Order = 10, Label = "Custom Class (Override)")]
		[EditingComponentProperty(nameof(TextInputProperties.DefaultValue), "")]
		public virtual string ClassOverride { get; set; }

		[EditingComponent(DropDownComponent.IDENTIFIER, Order = 11, Label = "Show on Desktop or Mobile/Tablet Only")]
		[EditingComponentProperty(nameof(DropDownProperties.DataSource), ";Default\r\nd-none d-xl-block;Desktop Only\r\nd-xl-none;Mobile/Tablet Only")]
		[EditingComponentProperty(nameof(DropDownProperties.DefaultValue), "")]

		public virtual string SectionScreenDisplay { get; set; }


		[EditingComponent(DropDownComponent.IDENTIFIER, Order = 15, Label = "Add Section Padding")]
		[EditingComponentProperty(nameof(DropDownProperties.DataSource), ";None\r\n1;Both (Top and bottom)\r\n2;Padding Top\r\n3;Padding Bottom")]
		[EditingComponentProperty(nameof(DropDownProperties.DefaultValue), "")]
		public virtual string AddSectionPadding { get; set; }

		[EditingComponent(DropDownComponent.IDENTIFIER, Order = 20, Label = "Widget Alignment (Standard Section)", Tooltip = "The widget alignment property is used to align widgets with in a standard section only.")]
		[EditingComponentProperty(nameof(DropDownProperties.DataSource), ";None\r\n1;Flex Start\r\n2;Flex End\r\n3;Center\r\n4;Space Between\r\n5;Space Around\r\n6;Space Evenly")]
		[EditingComponentProperty(nameof(DropDownProperties.DefaultValue), "")]
		public virtual string WidgetAlignment { get; set; }

		public virtual string SectionClass()
		{
			string paddingType = "";
			switch (AddSectionPadding)
			{
				case "1":
					paddingType = "section";
					break;
				case "2":
					paddingType = "section no-padding-bottom";
					break;
				case "3":
					paddingType = "section no-padding-top";
					break;
				default:
					break;
			}

			string widgetAlignment = "";
			switch (WidgetAlignment)
			{
				case "1":
					widgetAlignment = "wa-flex-start";
					break;
				case "2":
					widgetAlignment = "wa-flex-end";
					break;
				case "3":
					widgetAlignment = "wa-center";
					break;
				case "4":
					widgetAlignment = "wa-space-between";
					break;
				case "5":
					widgetAlignment = "wa-space-around";
					break;
				case "6":
					widgetAlignment = "wa-space-evenly";
					break;
				default:
					break;
			}

			string sc = String.Join(" ", new string[] { SectionModifier, ClassOverride, paddingType, widgetAlignment, BackgroundClass });
			return sc;
		}

		public virtual string ContainerClass()
		{
			if (FullWidth)
			{
				return "full-width";
			}
			return "container";
		}
	}
}
