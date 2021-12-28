/*
 * Built with Common Launchpad 2.0.2
 */

using Kentico.Forms.Web.Mvc;
using Launchpad.Web.Models.Common.FormComponents;

[assembly: RegisterFormComponent(DynamicTextInputComponent.IDENTIFIER, typeof(DynamicTextInputComponent), "Dynamic Text Input", Description = "Modified text input field with inline validation and tooltip options", IconClass = "icon-a-lowercase")]
namespace Launchpad.Web.Models.Common.FormComponents
{
	public class DynamicTextInputComponent : FormComponent<DynamicTextInputComponentProperties, string>
	{
		public const string IDENTIFIER = "DynamicTextInputComponent";

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

		public DynamicTextInputComponent()
		{
		}
	}
}