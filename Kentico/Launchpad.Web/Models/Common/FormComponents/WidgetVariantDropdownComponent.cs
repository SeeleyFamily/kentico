/*
 * Built with Common Launchpad 2.0.2
 */

using CMS.CustomTables;
using CMS.CustomTables.Types.Common;
using CMS.Helpers;
using Kentico.Forms.Web.Mvc;
using Kentico.Web.Mvc;
using Launchpad.Web.Models.Common.FormComponents;
using System.Collections.Generic;
using System.Data;

[assembly: RegisterFormComponent(WidgetVariantDropdownComponent.IDENTIFIER, typeof(WidgetVariantDropdownComponent), "Dropdown with widget variant options", IconClass = "icon-menu")]
namespace Launchpad.Web.Models.Common.FormComponents
{
	public class WidgetVariantDropdownComponent : SelectorFormComponent<WidgetVariantDropdownComponentProperties>
	{
		public const string IDENTIFIER = nameof(WidgetVariantDropdownComponent);

		protected override IEnumerable<HtmlOptionItem> GetHtmlOptions()
		{
			var query = CustomTableItemProvider.GetItems<WidgetVariantItem>();
			if (!string.IsNullOrWhiteSpace(Properties.WidgetName))
			{
				query = query.WhereEquals(nameof(WidgetVariantItem.WidgetName), Properties.WidgetName);
			}

			List<HtmlOptionItem> items = new List<HtmlOptionItem>();

			items.Add(new HtmlOptionItem
			{
				Text = "Select an widget variant",
				Value = string.Empty,
				Selected = (Properties.DefaultValue == string.Empty)
			}
			);

			if (!DataHelper.DataSourceIsEmpty(query))
			{
				foreach (DataRow dataRow in query.Tables[0].Rows)
				{
					var text = dataRow[nameof(WidgetVariantItem.VariantName)]?.ToString();

					var value = dataRow[nameof(WidgetVariantItem.CssClass)]?.ToString();

					var selected = (value == Properties.DefaultValue);

					items.Add(new HtmlOptionItem
					{
						Text = text,
						Value = value,
						Selected = selected
					});
				}
			}

			return items;
		}
	}
}