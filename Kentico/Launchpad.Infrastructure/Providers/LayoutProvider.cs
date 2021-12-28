using System.Web;
using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Core.Abstractions.Providers;
using Launchpad.Core.Abstractions.Services;


namespace Launchpad.Infrastructure.Providers
{

	public class LayoutProvider : ILayoutProvider, IPerScopeService
	{
		#region Fields
		private readonly IBannerService bannerService;
		private readonly ICurrentNodeProvider currentNodeProvider;
		private readonly ICurrentSiteProvider currentSiteProvider;
		private readonly HttpContextBase httpContext;
		private readonly IMenuService menuService;
		private readonly ISettingsService settingsService;
		private readonly IBaseSiteSettingsService baseSiteSettingsService;
		#endregion


		public LayoutProvider
		(
			IBannerService bannerService,
			ICurrentNodeProvider currentNodeProvider,
			ICurrentSiteProvider currentSiteProvider,
			HttpContextBase httpContext,
			IMenuService menuService,
			ISettingsService settingsService,
			IBaseSiteSettingsService baseSiteSettingsService
		)
		{
			this.bannerService = bannerService;
			this.currentNodeProvider = currentNodeProvider;
			this.currentSiteProvider = currentSiteProvider;
			this.httpContext = httpContext;
			this.menuService = menuService;
			this.settingsService = settingsService;
			this.baseSiteSettingsService = baseSiteSettingsService;
		}



		public IBannerService GetBannerService( )
		{
			return bannerService;
		}


		public ICurrentNodeProvider GetCurrentNodeProvider( )
		{
			return currentNodeProvider;
		}

		public ICurrentSiteProvider GetCurrentSiteProvider()
		{
			return currentSiteProvider;
		}

		public HttpContextBase GetHttpContext( )
		{
			return httpContext;
		}


		public IMenuService GetMenuService( )
		{
			return menuService;
		}

		public ISettingsService GetSettingsService( )
		{
			return settingsService;
		}

		public IBaseSiteSettingsService GetSiteSettingsService()
		{
			return baseSiteSettingsService;
		}

	}

}