/*
 * Built with Common Launchpad 2.0.2
 */

using Kentico.Forms.Web.Mvc;
using Launchpad.Web.Models.Common.FormComponents;

[assembly: RegisterFormComponent(DynamicRadioInputComponent.IDENTIFIER, typeof(DynamicRadioInputComponent), "Dynamic Radio Input", Description = "Modified radio input field with tooltip options", IconClass = "icon-rb-check")]
namespace Launchpad.Web.Models.Common.FormComponents
{
	public class DynamicRadioInputComponent : FormComponent<DynamicRadioInputComponentProperties, string>
	{
		public const string IDENTIFIER = "DynamicRadioInputComponent";
		public override bool CustomAutopostHandling => true;

		[BindableProperty]
		public string Value { get; set; } = "";

		public override string GetValue()
		{
			return Value;
		}
		public override void SetValue(string value)
		{
			Value = value;
		}

		public DynamicRadioInputComponent()
		{
		}
	}
}