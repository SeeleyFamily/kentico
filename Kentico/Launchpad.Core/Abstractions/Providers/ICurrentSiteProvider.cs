using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Core.Models;

namespace Launchpad.Core.Abstractions.Providers
{

	/// <summary>
	/// An interface for getting and setting the current <see cref="Site"/>, using a default DI scope of per web request.
	/// </summary>
	public interface ICurrentSiteProvider : IPerScopeService
	{
		/// <summary>
		/// Retrieves the current <see cref="Site"/>.
		/// </summary>
		Site GetCurrentSite( );		
	}

}
