using System.Collections.Generic;
using System.Linq;


namespace Launchpad.Core.Models
{

	public class MenuItem
	{
		public MenuItem Parent { get; set; } // determine if menu parent is filled out anywhere
		public IEnumerable<MenuItem> Children { get; set; }
		public string Image { get; set; }
		public string ImagePosition { get; set; }
		public bool IsCard { get; set; }
		public bool IsColumn { get; set; }
		public bool IsCurrentPage { get; set; }
		public bool IsExternal { get; set; }
		public string Label { get; set; }
		public string LabelMobile { get; set; }
		public string Text { get; set; }
		public string Url { get; set; }
		public string CustomClass { get; set; }
		public bool HideMobileOverviewLink { get; set; }
		public string ParentName { get; set; } // determine if menu ParentName is filled out anywhere - this may be used for data attribution


		public bool HasChildren => Children != null && Children.Count() > 0;
	}

}
