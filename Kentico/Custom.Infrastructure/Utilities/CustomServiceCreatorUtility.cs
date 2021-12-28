using Launchpad.Infrastructure.Services;
using Launchpad.Infrastructure.Utilities;


namespace Custom.Infrastructure.Utilities
{

	/// <summary>
	/// Provides a single factory set of methods for creating dependencies, mainly for testing or CMS modules. Includes any custom
	/// services needing instantiation.
	/// </summary>
	/// <remarks>
	/// DO NOT USE THIS IN THE LAUNCHPAD MVC PROJECTS. Use Dependency Injection instead. This utility is a stopgap intended for use
	/// in automated tests and in CMSApp where DI is difficult currently.
	/// </remarks>
	public class CustomServiceCreatorUtility : ServiceCreatorUtility
	{


	}

}