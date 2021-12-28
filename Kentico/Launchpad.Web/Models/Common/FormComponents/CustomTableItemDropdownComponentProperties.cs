/*
 * Built with Common Launchpad 2.0.2
 */

using Kentico.Forms.Web.Mvc;
using System;

namespace Launchpad.Web.Models.Common.FormComponents
{
	public class CustomTableItemDropdownComponentProperties : SelectorProperties
	{
		public virtual Type WhereConditionProviderType { get; set; }
		public virtual string CustomTableClassName { get; set; }
		public virtual int TopN { get; set; }
		public virtual string OrderBy { get; set; }
		public virtual bool IsOrderByDescending { get; set; }
		public virtual string ReturnColumnName { get; set; }
		public virtual string DisplayColumnName { get; set; }
	}
}
