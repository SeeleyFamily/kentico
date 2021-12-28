/*
 * Built with Common Launchpad 2.0.2
 */

using Kentico.Forms.Web.Mvc;
using Launchpad.Web.Models.Common.FormComponents;

[assembly: RegisterFormComponent(DynamicDropdownComponent.IDENTIFIER, typeof(DynamicDropdownComponent), "Dynamic Dropdown Input", Description = "Modified dropdown field with tooltip options and additional styling", IconClass = "icon-chevron-down-square")]
namespace Launchpad.Web.Models.Common.FormComponents
{
	public class DynamicDropdownComponent : FormComponent<DynamicDropdownComponentProperties, string>
	{
		public const string IDENTIFIER = "DynamicDropdownComponent";
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

		public DynamicDropdownComponent()
		{
		}
	}
}