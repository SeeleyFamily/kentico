/*
 * Built with Common Launchpad 2.0.2
 */

using Kentico.Forms.Web.Mvc;
using Launchpad.Web.Models.Common.FormComponents;

[assembly: RegisterFormComponent(StateDropdownComponent.IDENTIFIER, typeof(StateDropdownComponent), "US States Dropdown", Description = "Dropdown to select ftrom a list of US States", IconClass = "icon-map")]
namespace Launchpad.Web.Models.Common.FormComponents
{
	public class StateDropdownComponent : FormComponent<StateDropdownComponentProperties, string>
	{
		public const string IDENTIFIER = "StateDropdownComponent";

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

		public StateDropdownComponent()
		{
		}
	}
}