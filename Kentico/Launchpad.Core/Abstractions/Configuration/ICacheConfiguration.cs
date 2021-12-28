


namespace Launchpad.Core.Abstractions.Configuration
{

	/// <summary>
	/// An interface defining properties for caching strategies.
	/// </summary>
	public interface ICacheConfiguration
	{
		int CacheMinutes { get; set; }
		bool IsCached { get; set; }
		string NamePrefix { get; set; }
		int SiteID { get; set; }
		string Culture { get; set; }
	}

}
