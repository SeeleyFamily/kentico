using Launchpad.Core.Abstractions.Configuration;


namespace Launchpad.Core.Configuration
{

	/// <summary>
	/// An implementation of <see cref="ICacheConfiguration"/>, providing properties defining cache parameters.
	/// </summary>
	public class CacheConfiguration : ICacheConfiguration
	{
		public int CacheMinutes { get; set; } = 15;
		public bool IsCached { get; set; } = true;
		public string NamePrefix { get; set; }
		public int SiteID { get; set; }
		public string Culture { get; set; }
	}

}
