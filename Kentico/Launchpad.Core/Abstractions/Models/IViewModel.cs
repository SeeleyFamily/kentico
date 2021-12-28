using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Core.Models;


namespace Launchpad.Core.Abstractions.Models
{

	/// <summary>
	/// The base view model abstraction, with a default DI scope of per web request.
	/// </summary>
	public interface IViewModel : IPerScopeService
	{
		#region Properties
        PageNode Node { get; }
        bool UsePageBuilder { get; }
		string ViewPath { get; }
		bool ShouldNotRender { get; } // property to force a view not to render
		#endregion



		/// <summary>
		/// Entry point for populating a view model's properties. Should only be called once, typically by Dependency Injection factories. Do not call this method manually.
		/// </summary>
		void InitPopulate( );

	}

}
