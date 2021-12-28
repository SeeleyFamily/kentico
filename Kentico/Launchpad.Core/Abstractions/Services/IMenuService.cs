using Launchpad.Core.Models;
using System;
using System.Collections.Generic;


namespace Launchpad.Core.Abstractions.Services
{

	/// <summary>
	/// Defines methods used to retrieve collections of <see cref="MenuItem"/> objects representing a site's various menus.
	/// </summary>
	public interface IMenuService
	{
		/// <summary>
		/// Retrieves the breadcrumb <see cref="PageNode"/> collection for a given Node ID.
		/// </summary>
		Breadcrumbs GetBreadcrumbs(int nodeId);
		IEnumerable<MenuItem> GetFooterMenu();
		IEnumerable<MenuItem> GetMenu(string menuType);
		IEnumerable<MenuItem> GetMenu(Guid menuGuid);
		IEnumerable<MenuItem> GetNavigationMenu();
		IEnumerable<MenuItem> GetSubFooterMenu();
		IEnumerable<MenuItem> GetUtilityMenu();
		IEnumerable<MenuItem> GetSideNavMenu(
			PageNode currentNode,
			int nestingLevel,
			string[] classNames = null,
			string[] excludeClassNames = null,
			string rootMenuClassName = null
		);
	}

}
