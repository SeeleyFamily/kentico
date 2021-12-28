/*
 * Built with Common Launchpad 2.0.2
 */

using CMS.DataEngine;
using Kentico.Forms.Web.Mvc;
using Launchpad.Core.Extensions;
using System.Collections.Generic;

namespace Launchpad.Web.Models.Common.FormComponents
{
	public class DynamicRadioInputComponentProperties : FormComponentProperties<string>
	{
		[DefaultValueEditingComponent(TextInputComponent.IDENTIFIER)]
		public override string DefaultValue
		{
			get;
			set;
		}

		[EditingComponent(TextAreaComponent.IDENTIFIER)]
		public string Options { get; set; }

		[EditingComponent(TextInputComponent.IDENTIFIER, Label = "Custom Tooltip")]
		public string CustomTooltip { get; set; }

		public DynamicRadioInputComponentProperties()
			: base(FieldDataType.Text, 250)
		{
		}

		public Dictionary<string, string> GetOptions()
		{
			return Options.GenerateOptionsDictionary();
		}
	}
}