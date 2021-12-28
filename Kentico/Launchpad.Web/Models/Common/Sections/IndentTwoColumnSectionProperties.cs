/*
 * Built with Common Launchpad 2.0.2
 */

using Kentico.Forms.Web.Mvc;
using Launchpad.Web.Models.Custom.Sections;

namespace Launchpad.Web.Models.Common.Sections
{
	public class IndentTwoColumnSectionProperties : SectionProperties
	{
		public override string[] AreaClasses => GetAreaClasses();


		[EditingComponent(DropDownComponent.IDENTIFIER, Order = 8, Label = "Column Offset")]
		[EditingComponentProperty(nameof(DropDownProperties.DataSource), "1;None\r\n2;Left\r\n3;Right")]
		[EditingComponentProperty(nameof(DropDownProperties.DefaultValue), "")]
		public string SectionOffset { get; set; } = "1";

		public string[] GetAreaClasses()
		{
			if (SectionOffset == "2")
			{
				return new string[] { "offset-sm-1 col-sm-3", "col-sm-5" };
			}
			else if (SectionOffset == "3")
			{
				return new string[] { "offset-sm-1 col-sm-5", "col-sm-3" };
			}
			return new string[] { "offset-sm-1 col-sm-4", "col-sm-4" };
		}

	}
}