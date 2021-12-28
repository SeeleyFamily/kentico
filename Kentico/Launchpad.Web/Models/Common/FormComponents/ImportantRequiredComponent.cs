/*
 * Built with Common Launchpad 2.0.2
 */

using Kentico.Forms.Web.Mvc;
using Launchpad.Web.Models.Common.FormComponents;

[assembly: RegisterFormComponent(ImportantRequiredComponent.IDENTIFIER, typeof(ImportantRequiredComponent), "Honeypot Field", Description = "Add a honeypot validation fields to the form", IconClass = "icon-times-circle")]
namespace Launchpad.Web.Models.Common.FormComponents
{
	public class ImportantRequiredComponent : FormComponent<ImportantRequiredComponentProperties, string>
	{
		public const string IDENTIFIER = "ImportantRequiredComponent";

		[BindableProperty]
		public string Value { get; set; } = string.Empty;

		public ImportantRequiredComponent()
		{

		}

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