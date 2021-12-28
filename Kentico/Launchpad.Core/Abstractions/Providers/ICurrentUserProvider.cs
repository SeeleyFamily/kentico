using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Core.Abstractions.Models;


namespace Launchpad.Core.Abstractions.Providers
{

	/// <summary>
	/// An interface for getting and setting the current <see cref="IUser"/>, using a default DI scope of per web request.
	/// </summary>
	public interface ICurrentUserProvider : IPerScopeService
	{
		/// <summary>
		/// Retrieves the current <see cref="IUser"/>.
		/// </summary>
		IUser GetCurrentUser( );


		/// <summary>
		/// Sets the current <see cref="IUser"/>.
		/// </summary>
		void SetCurrentUser( IUser user );
	}

}
