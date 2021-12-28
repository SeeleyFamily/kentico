using System;
using System.Configuration;
using System.Web.Mvc;
using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Extensions;
using Launchpad.Core.Services;
using Launchpad.Infrastructure.Abstractions.Services;


namespace Launchpad.Web
{

	/// <summary>
	/// Configures cache during application startup.
	/// </summary>
	public static class CacheConfig
    {
		#region Fields
		private static bool IsPrecacheDisabled = false;
		private static readonly string[] PrecachePageTypes = !string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings.GetStringValue("cache:StartupPageTypes")) ? ConfigurationManager.AppSettings.GetStringValue("cache:StartupPageTypes").Split(',') : new string[] { };
		private static readonly string[] PrecacheSpecificPages = !string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings.GetStringValue("cache:StartupPageNodeAliases")) ? ConfigurationManager.AppSettings.GetStringValue("cache:StartupPageNodeAliases").Split(',') : new string[] { };
		private static readonly string[] PrecacheCustomTables = !string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings.GetStringValue("cache:StartupCustomTables")) ? ConfigurationManager.AppSettings.GetStringValue("cache:StartupCustomTables").Split(',') : new string[] { };
		#endregion



		/// <summary>
		/// Caches important queries and lookups during application startup.
		/// </summary>
		public static void StartupCache( IDependencyResolver dependencyResolver )
        {
			// Is precache disabled?
			IsPrecacheDisabled = ConfigurationManager.AppSettings[ "cache:DisableStartupCache" ] is string setting && Boolean.TryParse( setting, out bool isStartupCacheDisabled ) && isStartupCacheDisabled;

			if( IsPrecacheDisabled )
			{
				return;
			}

			// Only perform startup caching if caching hasn't been disabled
			ICacheConfiguration cacheConfiguration = dependencyResolver.GetService<ICacheConfiguration>();

			if( !cacheConfiguration.IsCached )
			{
				return;
			}



			// Get the necessary services
		//	ICacheService cacheService = dependencyResolver.GetService<ICacheService>();
			IDocumentService documentService = dependencyResolver.GetService<IDocumentService>();
		//	ICustomTableService customTableService = dependencyResolver.GetService<ICustomTableService>();
			IKenticoUserService userService = dependencyResolver.GetService<IKenticoUserService>();
			IMenuService menuService = dependencyResolver.GetService<IMenuService>();
			IRedirectService redirectService = dependencyResolver.GetService<IRedirectService>();
			IResourceService resourceService = dependencyResolver.GetService<IResourceService>();

			try
			{
				// Cache the home page always
				documentService.GetHomePage();

				// Cache the menus always
				menuService.GetFooterMenu();
				menuService.GetNavigationMenu();
				menuService.GetSubFooterMenu();
				menuService.GetUtilityMenu();


				// Cache important page types, such as overview, landing pages
				foreach( string className in PrecachePageTypes )
				{
					// This method caches by default
					documentService.GetByType( className );
				}


				// Cache important specific pages
				foreach( string nodeAliasPath in PrecacheSpecificPages )
				{
					// This method caches by default
					documentService.Get( nodeAliasPath );
				}
				
				// Cache Redirects
				redirectService.GetRedirects();

				// Cache Resource Strings
				resourceService.LoadCache();


				// Load the anonymous user and its ACL?
				if( Boolean.Parse( ConfigurationManager.AppSettings[ "security:enabled" ] ) is bool isSecurityEnabled && isSecurityEnabled )
				{
					int siteId = int.Parse( ConfigurationManager.AppSettings[ "SiteId" ] );
					userService.GetPublicAnonymousUser( siteId );
				}
			}
			catch( Exception )
			{
				// TODO: Log warning/error
			}
        }


		// Session Cache occurs once as the first session is created
		// Required due to certain data calls form Kentico requiring a request object (likely due to checking of license files);
		public static void SessionCache(IDependencyResolver dependencyResolver)
		{
			// Precache disabled?
			if( IsPrecacheDisabled )
			{
				return;
			}


			// Only perform session caching if caching hasn't been disabled
			ICacheConfiguration cacheConfiguration = dependencyResolver.GetService<ICacheConfiguration>();

			if (!cacheConfiguration.IsCached)
			{
				return;
			}

			// Get the necessary services			
			ICustomTableService customTableService = dependencyResolver.GetService<ICustomTableService>();
			
			try
			{							
				// Cache important custom tables
				foreach (string className in PrecacheCustomTables)
				{
					customTableService.GetByType(className);
				}
			}
			catch (Exception)
			{
				// TODO: Log warning/error
			}
		}

	}

}
