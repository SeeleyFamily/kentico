/*
 * Built with Common Launchpad 2.0.2
 */

using CMS.DataEngine;
using Kentico.Forms.Web.Mvc;

namespace Launchpad.Web.Models.Common.FormComponents
{
	public class DynamicTextInputComponentProperties : FormComponentProperties<string>
	{
		[DefaultValueEditingComponent(TextInputComponent.IDENTIFIER)]
		public override string DefaultValue
		{
			get;
			set;
		}

		[EditingComponent(TextInputComponent.IDENTIFIER, Label = "Placeholder")]
		public override string Placeholder
		{
			get;
			set;
		}


		[EditingComponent(TextInputComponent.IDENTIFIER, Label = "Custom Tooltip")]
		public string CustomTooltip { get; set; }

		[EditingComponent(DropDownComponent.IDENTIFIER, Label = "Data Type")]
		[EditingComponentProperty(nameof(DropDownProperties.DataSource), ";Text\r\nemail;E-mail\r\nphone;Phone Number\r\nzipcode;Zipcode")]
		[EditingComponentProperty(nameof(DropDownProperties.DefaultValue), "")]
		public string ValidationType { get; set; }

		public DynamicTextInputComponentProperties()
			: base(FieldDataType.Text, 250)
		{
		}
	}
}