


namespace Launchpad.Core.Services
{

	/// <summary>
	/// An interface defining Resource service calls for resource strings.
	/// </summary>
	public interface IResourceService
	{
		string GetString( string key, string culture = null, bool useCache = true );
		void LoadCache( );
	}

}