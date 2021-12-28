/*
 * Built with Common Launchpad 2.0.2
 */

using Kentico.Forms.Web.Mvc;
using Launchpad.Web.Models.Common.FormComponents;
using System;
using System.Collections.Generic;

[assembly: RegisterFormComponent(MultipleCheckboxComponent.IDENTIFIER, typeof(MultipleCheckboxComponent), "Multiple Checkbox Input", Description = "Allows users add more than one checkbox from a list of options", IconClass = "icon-cb-check-preview")]
namespace Launchpad.Web.Models.Common.FormComponents
{
	public class MultipleCheckboxComponent : FormComponent<MultipleCheckboxComponentProperties, string>
	{
		public const string IDENTIFIER = "MultipleCheckboxComponent";

		[BindableProperty]
		public string Value { get; set; } = "";

		[BindableProperty]
		public IEnumerable<string> CheckboxComponents { get; set; }

		public override bool CustomAutopostHandling => true;
		public void GetCheckboxComponents()
		{
			if (!string.IsNullOrEmpty(Value))
			{
				CheckboxComponents = Value.Split('|');
			}
		}
		public override string GetValue()
		{
			if (CheckboxComponents != null)
			{
				return String.Join("|", CheckboxComponents);
			}
			return "";
		}
		public override void SetValue(string value)
		{
			Value = value;
		}
	}
}