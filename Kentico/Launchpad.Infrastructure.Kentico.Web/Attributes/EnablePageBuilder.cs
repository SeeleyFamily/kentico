using System;


namespace Launchpad.Infrastructure.Kentico.Web.Attributes
{
	/// <summary>
	/// Marks a view model class as requiring PageBuilder functionality. Global filters will then ensure Kentico PageBuilder functionality is enabled.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
	public class EnablePageBuilder : Attribute
	{
	}

}
