/*
 * Built with Common Launchpad 2.0.2
 */

using Kentico.Components.Web.Mvc.FormComponents;
using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Models;
using Launchpad.Web.Models.Custom.Widgets;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Launchpad.Web.Models.Common.Widgets
{
	public class MenuWidgetProperties : WidgetProperties, IWidgetProperties
	{
		private readonly IMenuService menuService;
		// Assigns a selector component to the Pages property
		[EditingComponent(PageSelector.IDENTIFIER, Order = 4, Label = "Menu Folder")]
		// Returns a list of page selector items (node GUIDs)
		public IList<PageSelectorItem> MenuFolder { get; set; }

		public MenuWidgetProperties()
		{
			this.menuService = DependencyResolver.Current.GetService<IMenuService>();
		}

		public IEnumerable<MenuItem> GetMenu()
		{
			PageSelectorItem page = MenuFolder?.FirstOrDefault();
			if (page != null)
			{
				return menuService.GetMenu(page.NodeGuid);
			}
			return new List<MenuItem>();
		}
	}
}