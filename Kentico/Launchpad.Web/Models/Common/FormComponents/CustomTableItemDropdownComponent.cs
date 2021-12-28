/*
 * Built with Common Launchpad 2.0.2
 */

using CMS.CustomTables;
using CMS.Helpers;
using Kentico.Components.Web.Mvc.FormComponents;
using Kentico.Forms.Web.Mvc;
using Kentico.Web.Mvc;
using Launchpad.Web.Models.Common.FormComponents;
using System;
using System.Collections.Generic;
using System.Data;

[assembly: RegisterFormComponent(CustomTableItemDropdownComponent.IDENTIFIER, typeof(CustomTableItemDropdownComponent), "Dropdown with custom table data options", IconClass = "icon-menu")]
namespace Launchpad.Web.Models.Common.FormComponents
{
	public class CustomTableItemDropdownComponent : SelectorFormComponent<CustomTableItemDropdownComponentProperties>
	{
		public const string IDENTIFIER = nameof(CustomTableItemDropdownComponent);

		protected override IEnumerable<HtmlOptionItem> GetHtmlOptions()
		{
			var query = CustomTableItemProvider.GetItems(Properties.CustomTableClassName);

			if (typeof(IObjectSelectorWhereConditionProvider).IsAssignableFrom(Properties.WhereConditionProviderType))
			{
				var whereConditionProvider = (IObjectSelectorWhereConditionProvider)Activator.CreateInstance(Properties.WhereConditionProviderType);
				query = query.Where(whereConditionProvider.Get());
			}

			if (Properties.TopN > 0)
			{
				query = query.TopN(Properties.TopN);
			}

			if (string.IsNullOrWhiteSpace(Properties.OrderBy))
			{
				Properties.OrderBy = Properties.DisplayColumnName;
			}

			if (!string.IsNullOrWhiteSpace(Properties.OrderBy))
			{
				if (Properties.IsOrderByDescending)
				{
					query = query.OrderByDescending(Properties.OrderBy);
				}
				else
				{
					query = query.OrderBy(Properties.OrderBy);
				}
			}

			List<HtmlOptionItem> items = new List<HtmlOptionItem>();

			if (!DataHelper.DataSourceIsEmpty(query))
			{
				foreach (DataRow dataRow in query.Tables[0].Rows)
				{
					// default to column 1 if no 'DisplayColumnName' provided
					var text = string.IsNullOrWhiteSpace(Properties.DisplayColumnName) ?
						dataRow[1]?.ToString() :
						dataRow[Properties.DisplayColumnName]?.ToString();

					// default to column 0 if no 'ReturnColumnName' provided
					var value = string.IsNullOrWhiteSpace(Properties.ReturnColumnName) ?
						dataRow[0]?.ToString() :
						dataRow[Properties.ReturnColumnName]?.ToString();

					var selected = dataRow[Properties.ReturnColumnName]?.ToString() == Properties.DefaultValue;

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