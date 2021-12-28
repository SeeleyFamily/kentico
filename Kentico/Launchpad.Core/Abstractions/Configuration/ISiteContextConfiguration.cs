


namespace Launchpad.Core.Abstractions.Configuration
{

	/// <summary>
	/// An interface defining properties for site context.
	/// </summary>
	public interface ISiteContextConfiguration
	{
		int SiteId { get; set; }
		string SiteName { get; set; }
	}

}
