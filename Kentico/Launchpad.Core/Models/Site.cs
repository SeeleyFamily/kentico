using System;


namespace Launchpad.Core.Models
{

	public class Site
	{
		public string CodeName { get; set; }
		public string Name { get; set; }
		public string PresentationUrl { get; set; }
		public Guid SiteGuid { get; set; }
		public int SiteID { get; set; }
	}

}
