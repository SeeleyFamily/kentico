using System;


namespace Launchpad.Core.Models
{

	public class Banner
	{
		public string Image { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public string CtaLabel { get; set; }
		public string CtaUrl { get; set; }
		public bool IsCtaUsedToClose { get; set; }
		public string BannerType { get; set; }
		public string NodeAliasPath { get; set; }
		public Guid NodeGuid { get; set; }
		public int NodeID { get; set; }
		public int NodeLevel { get; set; }
		public int NodeOrder { get; set; }
	}

}
