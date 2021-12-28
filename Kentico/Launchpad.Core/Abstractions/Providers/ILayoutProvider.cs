using System.Web;
using Launchpad.Core.Abstractions.Services;


namespace Launchpad.Core.Abstractions.Providers
{

	/// <summary>
	/// Defines an interface for an aggregate provider to be used in base view models and provide functionality necessary for all pages of a site.
	/// </summary>
	public interface ILayoutProvider
	{
		IBannerService GetBannerService( );
		ICurrentNodeProvider GetCurrentNodeProvider( );
		ICurrentSiteProvider GetCurrentSiteProvider( );
		HttpContextBase GetHttpContext( );
		IMenuService GetMenuService( );
		ISettingsService GetSettingsService( );
		IBaseSiteSettingsService GetSiteSettingsService();
	}

}
