/*
 * Built with Common Launchpad 2.0.2
 */

using CMS.DataEngine;
using Kentico.Forms.Web.Mvc;

namespace Launchpad.Web.Models.Common.FormComponents
{
	public class VideoInputComponentProperties : FormComponentProperties<string>
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

		public VideoInputComponentProperties()
			: base(FieldDataType.Text, 200)
		{
		}
	}
}