/*
 * Built with Common Launchpad 2.0.2
 */

using CMS.DataEngine;
using Kentico.Forms.Web.Mvc;
using Launchpad.Core.Constants;

namespace Launchpad.Web.Models.Common.FormComponents
{
	public class ImportantRequiredComponentProperties : FormComponentProperties<string>
	{
		public override string Name { get; set; } = HoneypotConstants.DefaultHoneypotFieldId;
		public override bool SmartField { get; set; }
		public override bool Required { get; set; }
		public override string ExplanationText { get; set; }
		public new string Label { get; set; } = "";
		public override string Tooltip { get; set; }

		public override string DefaultValue
		{
			get;
			set;
		}

		public ImportantRequiredComponentProperties()
		: base(FieldDataType.Text, 1000)
		{

		}
	}
}