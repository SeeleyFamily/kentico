using Launchpad.Core.Abstractions.Configuration;


namespace Launchpad.Core.Configuration
{

	/// <summary>
	/// An implementation of <see cref="SiteContextConfiguration"/>, providing properties defining site context.
	/// </summary>
	public class SiteContextConfiguration : ISiteContextConfiguration
	{
		public int SiteId { get; set; } = 0;
		public string SiteName { get; set; } = "";
	}

}
