using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Core.Models;


namespace Launchpad.Core.Abstractions.Providers
{

	/// <summary>
	/// An interface for getting and setting the current <see cref="PageNode"/>, using a default DI scope of per web request.
	/// </summary>
	public interface ICurrentNodeProvider : IPerScopeService
	{
		PageNode GetCurrentNode( );
		void SetCurrentNode( PageNode node );
		void SetCurrentNode( string nodeAliasPath );
	}

}
