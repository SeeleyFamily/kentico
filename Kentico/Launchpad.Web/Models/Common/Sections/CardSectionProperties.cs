/*
 * Built with Common Launchpad 2.0.2
 */

using Kentico.Forms.Web.Mvc;
using Launchpad.Web.Models.Custom.Sections;

namespace Launchpad.Web.Models.Common.Sections
{
	public class CardSectionProperties : SectionProperties
	{
		public override bool UseRow { get; set; } = false;
		public override string[] AreaClasses
		{
			get => new string[] { "cards " + GridType };
			set => base.AreaClasses = value;
		}

		[EditingComponent(DropDownComponent.IDENTIFIER, Order = 0, Label = "Card Style")]
		[EditingComponentProperty(nameof(DropDownProperties.DataSource), "cards--grid;Grid\r\ncards--list;List")]
		[EditingComponentProperty(nameof(DropDownProperties.DefaultValue), "cards--grid")]
		public string GridType { get; set; } = "cards--grid";

		public override string ContainerClass()
		{
			if (!FullWidth)
			{
				return "container card-container " + BackgroundClass;
			}
			return "container card-container";
		}
	}
}