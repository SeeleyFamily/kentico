/*
 * Built with Common Launchpad 2.0.2
 */

using CMS.DataEngine;
using Kentico.Forms.Web.Mvc;

namespace Launchpad.Web.Models.Common.FormComponents
{
	public class StateDropdownComponentProperties : FormComponentProperties<string>
	{

		[DefaultValueEditingComponent(TextInputComponent.IDENTIFIER)]
		public override string DefaultValue
		{
			get;
			set;
		}


		[EditingComponent(TextInputComponent.IDENTIFIER, Label = "Custom Tooltip")]
		public string CustomTooltip { get; set; }

		public StateDropdownComponentProperties()
			: base(FieldDataType.Text, 2)
		{
		}

	}
}