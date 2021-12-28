using System;
using System.Configuration;
using System.Web;
using CMS.Localization;
using Kentico.Content.Web.Mvc;
using Kentico.Web.Mvc;
using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Core.Configuration;


namespace Launchpad.Infrastructure.Kentico.Web.Configuration
{

	public static class DocumentQueryConfigurationFactory
	{

		/// <summary>
		/// Creates a <see cref="IDocumentQueryConfiguration"/> implementation based on a provided <see cref="HttpContextBase"/> instance, using Kentico's web-specific contexts. Do not use this
		/// factory outside web clients.
		/// </summary>
		public static IDocumentQueryConfiguration CreateConfiguration(HttpContextBase httpContext)
		{
			// Determine whether the preview feature is enabled and the current request is a preview request
			bool isPreviewFeatureEnabled = (httpContext.Kentico().GetFeature<IPreviewFeature>() is IPreviewFeature);
			bool isPreviewRequest = isPreviewFeatureEnabled && httpContext.Kentico().Preview().Enabled;

			ISiteContextConfiguration siteContextConfigurationFactory = SiteContextConfigurationFactory.CreateConfiguration();
			// Get the site Id
			int siteId = siteContextConfigurationFactory.SiteId;
			string siteName = siteContextConfigurationFactory.SiteName;

			// Gated? Check Permissions?
			bool checkPermissions = Boolean.TryParse(ConfigurationManager.AppSettings["query:checkpermissions"], out checkPermissions) ? checkPermissions : false;


			// Culture
			string culture = "en-US";

			if (httpContext.Handler != null && LocalizationContext.CurrentCulture?.CultureCode is string cultureCode)
			{
				culture = cultureCode;
			}


			try
			{
				return new DocumentQueryConfiguration
				{
					CheckPermissions = checkPermissions,
					Culture = culture,
					IsPreview = isPreviewRequest,
					SiteId = siteId,
					SiteName = siteName
				};
			}
			catch (Exception)
			{
				// This case can occur because there's no web request, such as app startup, so provide a default configuration, too
				return new DocumentQueryConfiguration();
			}
		}

	}

}
