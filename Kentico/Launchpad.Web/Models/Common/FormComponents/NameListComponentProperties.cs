/*
 * Built with Common Launchpad 2.0.2
 */

using CMS.DataEngine;
using Kentico.Forms.Web.Mvc;

namespace Launchpad.Web.Models.Common.FormComponents
{
	public class NameListComponentProperties : FormComponentProperties<string>
	{
		[DefaultValueEditingComponent(nameof(NameListComponent), DefaultValue = "")]
		public override string DefaultValue { get; set; }

		public NameListComponentProperties() : base(FieldDataType.LongText)
		{
		}
	}
}
