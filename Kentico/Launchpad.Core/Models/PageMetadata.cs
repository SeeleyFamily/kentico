using Launchpad.Core.Enums;


namespace Launchpad.Core.Models
{

	public class PageMetadata
	{
		public string CanonicalUrl { get; set; }
		public string Description { get; set; }
		public string ImageUrl { get; set; }
		public IndexDirective IndexDirective { get; set; }
		public string Title { get; set; }
		public string Url { get; set; }
		public string MobileWebAppTitle { get; set; }
		public string OgTitle { get; set; }
		public string OgDescription { get; set; }
		public string OgImage { get; set; }
		public string OgUrl { get; set; }
		public string TwitterCard { get; set; }
		public string TwitterSite { get; set; }
		public string TwitterCreator { get; set; }
		public string Schema { get; set; }
	}

}
