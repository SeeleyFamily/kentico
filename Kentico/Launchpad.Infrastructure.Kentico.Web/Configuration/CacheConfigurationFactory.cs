using CMS.Helpers;
using CMS.Localization;
using Kentico.Content.Web.Mvc;
using Kentico.Web.Mvc;
using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Core.Configuration;
using System.Configuration;
using System.Web;


namespace Launchpad.Infrastructure.Kentico.Web.Configuration
{

	public static class CacheConfigurationFactory
	{

		/// <summary>
		/// Creates a <see cref="ICacheConfiguration"/> implementation based on a provided <see cref="HttpContextBase"/> instance, using Kentico's web-specific contexts. Do not use this
		/// factory outside web clients. For preview requests, disables caching.
		/// </summary>
		public static ICacheConfiguration CreateConfiguration(HttpContextBase httpContext)
		{
			var namePrefix = ConfigurationManager.AppSettings["CMSApplicationName"];

			// Set the current site ID, if there is one
			ISiteContextConfiguration siteContextConfiguration = SiteContextConfigurationFactory.CreateConfiguration();
			int siteID = siteContextConfiguration.SiteId;

			// Get the culture
			var culture = LocalizationContext.CurrentCulture.CultureCode;

			// If cache is disabled manually, go no further
			if (ConfigurationManager.AppSettings["cache:Disabled"] == "true")
			{
				return new CacheConfiguration
				{
					IsCached = false,
					NamePrefix = namePrefix,
					SiteID = siteID,
					Culture = culture,
				};
			}



			// Determine whether the preview feature is enabled and the current request is a preview request
			bool isPreviewFeatureEnabled = (httpContext.Kentico().GetFeature<IPreviewFeature>() is IPreviewFeature);
			bool isPreviewRequest = isPreviewFeatureEnabled && httpContext.Kentico().Preview().Enabled;


			// Set default cache minutes
			string cacheMinutesSetting = ConfigurationManager.AppSettings["cache:DefaultMinutes"];
			int.TryParse(cacheMinutesSetting, out int cacheMinutes);

			if (string.IsNullOrWhiteSpace(cacheMinutesSetting) && cacheMinutes <= 0)
			{
				// If the setting wasn't provided, use a default of 20 minutes
				cacheMinutes = 20;
			}


			// Return the cache configuration
			return new CacheConfiguration
			{
				CacheMinutes = cacheMinutes,
				IsCached = !isPreviewRequest,
				NamePrefix = namePrefix,
				SiteID = siteID,
				Culture = culture,
			};
		}

	}

}
