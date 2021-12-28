using CMS.DataEngine;
using CMS.Module.Redirects;
using CMS.SiteProvider;
using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Core.Configuration;
using Launchpad.Core.Models;
using Launchpad.Infrastructure.Extensions;
using Launchpad.Infrastructure.Services;
using System;
using System.Collections.Generic;

namespace Launchpad.Infrastructure.Kentico.CMS.Services
{
	public class RedirectsModuleService
	{
		private RedirectService _RedirectService {
			get {
				var siteContextConfiguration = new SiteContextConfiguration()
				{
					SiteId = SiteContext.CurrentSiteID,
					SiteName = SiteContext.CurrentSiteName
				};

				var cacheConfiguration = new Lazy<ICacheConfiguration>(() => new CacheConfiguration());
				var cacheService = new CacheService(cacheConfiguration);

				var loggerService = new LoggerService();


				return new RedirectService(cacheService, loggerService, siteContextConfiguration);
			}
		}

		public RedirectsModuleService()
		{
			// SiteContext is not available in the constructor
		}

		public void ClearCache()
		{
			_RedirectService.ClearCache();
		}

		public IEnumerable<Redirect> GetRedirects()
		{
			return _RedirectService.GetRedirects();
		}

		public void RedirectsModuleBeforeUpdateHandler(object sender, ObjectEventArgs e)
		{
			switch (e.Object)
			{				
				case PermanentRedirectsInfo permanentRedirectsInfo:
					// Clean the URLS
					permanentRedirectsInfo.MatchUrl = permanentRedirectsInfo.MatchUrl.CleanRedirectsURLs();
					permanentRedirectsInfo.RedirectUrl = permanentRedirectsInfo.RedirectUrl.CleanRedirectsURLs();
					ValidatePermanentRedirectsInfo(permanentRedirectsInfo);
					break;
				case TemporaryRedirectsInfo temporaryRedirectsInfo:
					// Clean the URLS
					temporaryRedirectsInfo.MatchUrl = temporaryRedirectsInfo.MatchUrl.CleanRedirectsURLs();
					temporaryRedirectsInfo.RedirectUrl = temporaryRedirectsInfo.RedirectUrl.CleanRedirectsURLs();
					ValidateTemporaryRedirectsInfo(temporaryRedirectsInfo);
					break;
				case RegexRedirectsInfo regexRedirectsInfo:
					// Do not clean regex urls. These are very specific and should match what is entered.
					ValidateRegexRedirectsInfo(regexRedirectsInfo);
					break;
			}
		}

		/// <summary>
		/// Checks for duplicates based on a specified column name. Optional siteIdentifier for site specific duplicates.
		/// </summary>
		/// <param name="object"></param>
		/// <param name="columnName"></param>
		/// <param name="siteIdentifier"></param>
		private void CheckDuplicateObject(BaseInfo @object, string columnName, string siteIdentifier = "")
		{
			if (!@object.CheckUniqueValues(new string[] { columnName, siteIdentifier }))
			{
				throw new Exception($"There is already an entry with the same {columnName}.");
			}
		}

		private void ValidatePermanentRedirectsInfo(PermanentRedirectsInfo permanentRedirectsInfo)
		{
			var redirect = permanentRedirectsInfo.ToRedirect();
			if (redirect.IsValid)
			{
				CheckDuplicateObject(permanentRedirectsInfo, nameof(permanentRedirectsInfo.MatchUrl), nameof(permanentRedirectsInfo.SiteID));
			}
		}

		private void ValidateTemporaryRedirectsInfo(TemporaryRedirectsInfo temporaryRedirectsInfo)
		{
			var redirect = temporaryRedirectsInfo.ToRedirect();
			if (redirect.IsValid)
			{
				CheckDuplicateObject(temporaryRedirectsInfo, nameof(temporaryRedirectsInfo.MatchUrl), nameof(temporaryRedirectsInfo.SiteID));
			}
		}

		private void ValidateRegexRedirectsInfo(RegexRedirectsInfo regexRedirectsInfo)
		{
			var redirect = regexRedirectsInfo.ToRedirect();
			if (redirect.IsValid)
			{
				CheckDuplicateObject(regexRedirectsInfo, nameof(regexRedirectsInfo.MatchUrl), nameof(regexRedirectsInfo.SiteID));
			}
		}
	}
}
