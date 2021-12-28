


namespace Launchpad.Core.Abstractions.Configuration
{
	/// <summary>
	/// Marks a service class as discoverable for Dependency Injection service registration, with a per request lifetime (a new instance will be created each time this 
	/// service is included and requested as a dependency).
	/// </summary>
	public interface IPerRequestService : ILifetimeScope
	{
	}

}
