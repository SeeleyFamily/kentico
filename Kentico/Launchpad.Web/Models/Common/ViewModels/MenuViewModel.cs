/*
 * Built with Common Launchpad 2.0.2
 */

using Launchpad.Core.Models;
using System.Collections.Generic;


namespace Launchpad.Web.Models.Common.ViewModels
{

	public class MenuViewModel
	{
		public IEnumerable<MenuItem> FooterMenu { get; set; }
		public IEnumerable<MenuItem> NavigationMenu { get; set; }
		public IEnumerable<MenuItem> SubFooterMenu { get; set; }
		public IEnumerable<MenuItem> UtilityMenu { get; set; }
	}

}