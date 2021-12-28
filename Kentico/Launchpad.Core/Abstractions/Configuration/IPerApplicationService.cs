


namespace Launchpad.Core.Abstractions.Configuration
{
	/// <summary>
	/// Marks a service class as discoverable for Dependency Injection service registration, with a per application lifetime (only one
	/// of these will exist per application lifetime).
	/// </summary>
	public interface IPerApplicationService : ILifetimeScope
	{
	}

}
