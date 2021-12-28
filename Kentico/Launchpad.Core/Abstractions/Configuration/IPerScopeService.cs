


namespace Launchpad.Core.Abstractions.Configuration
{
	/// <summary>
	/// Marks a service class as discoverable for Dependency Injection service registration, with a default per web request lifetime.
	/// </summary>
	public interface IPerScopeService : ILifetimeScope
	{
	}

}
