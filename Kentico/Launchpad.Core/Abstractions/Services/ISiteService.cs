using Launchpad.Core.Models;


namespace Launchpad.Core.Abstractions.Services
{

	/// <summary>
	/// An interface defining methods to retrieve <see cref="Site"/> information.
	/// </summary>
	public interface ISiteService
	{
		Site GetSite( int id );
		Site GetSite( string codeName );
	}

}
