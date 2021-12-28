using System;
using System.Configuration;
using CMS.SiteProvider;
using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Core.Configuration;


namespace Launchpad.Infrastructure.Kentico.Web.Configuration
{

	public static class SiteContextConfigurationFactory
	{

		/// <summary>
		/// Creates a <see cref="ISiteContextConfiguration"/> implementation using Kentico's web-specific <see cref="SiteContext"/>. Do not use this
		/// factory outside web clients.
		/// </summary>
		public static ISiteContextConfiguration CreateConfiguration()
		{
			int siteId = 0;
			string siteName = "";


			// Local function to prevent SiteID and Name from ever being 0/blank
			void SetDefault()
			{
				// Get site ID from the application config settings
				siteId = int.TryParse( ConfigurationManager.AppSettings[ "SiteId" ], out siteId ) ? siteId : 0;
				siteName = ConfigurationManager.AppSettings[ "SiteName" ];
			};


			// Set the current site ID, if there is one
			try
			{
				// This can fail on app startup or requests that don't involve httpcontext / web requests
				siteId = SiteContext.CurrentSiteID;
				siteName = SiteContext.CurrentSiteName;

				if( siteId == 0 || String.IsNullOrWhiteSpace( siteName ) )
				{
					SetDefault();
				}
			}
			catch (Exception)
			{
				SetDefault();
			}


			// Return the cache configuration
			return new SiteContextConfiguration()
			{
				SiteId = siteId,
				SiteName = siteName
			};
		}

	}

}
