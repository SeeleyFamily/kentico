/*
 * Built with Common Launchpad 2.0.2
 */

using Kentico.Forms.Web.Mvc;
using Launchpad.Web.Models.Common.FormComponents;

[assembly: RegisterFormComponent(WysiwygInputComponent.IDENTIFIER, typeof(WysiwygInputComponent), "Dynamic Area or Wysiwyg", Description = "Allows users write in a text area with font formatting controls", IconClass = "icon-bubble-o")]
namespace Launchpad.Web.Models.Common.FormComponents
{
	public class WysiwygInputComponent : FormComponent<WysiwygInputComponentProperties, string>
	{
		public const string IDENTIFIER = "WysiwygInputComponent";

		[BindableProperty]
		public string Value { get; set; } = "";

		public override bool CustomAutopostHandling => true;


		public override string GetValue()
		{
			return Value;
		}
		public override void SetValue(string value)
		{
			Value = value;
		}
	}
}