using CMS.Helpers;
using CMS.Module.Redirects;
using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Configuration;
using Launchpad.Core.Enums;
using Launchpad.Core.Models;
using Launchpad.Infrastructure.Abstractions.Services;
using Launchpad.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Launchpad.Infrastructure.Services
{

	public class RedirectService : IRedirectService, IPerApplicationService
	{		
		private const string NamePrefix = "redirects";
		private const int CacheMinutes = 43800; // default redirect cache time should be for one month
		private int SiteID { get; set; } = 0;
		private CacheConfiguration CacheConfiguration { get; set; }

		#region Fields
		private readonly ICacheService cacheService;
		private readonly ILoggerService loggerService;
		private readonly ISiteContextConfiguration siteContextConfiguration;		
		#endregion
		

		public RedirectService
		(
			ICacheService cacheService,
			ILoggerService loggerService,
			ISiteContextConfiguration siteContextConfiguration
		)
		{
			this.cacheService = cacheService;
			this.loggerService = loggerService;
			this.siteContextConfiguration = siteContextConfiguration;

			SiteID = this.siteContextConfiguration.SiteId;
			// Allow Redirect Service to define its own cache service & configuration to use
			CacheConfiguration = new CacheConfiguration()
			{
				IsCached = true,
				CacheMinutes = CacheMinutes,
				NamePrefix = NamePrefix,
				SiteID = this.SiteID,
			};
		}



		public void ClearCache()
		{
			loggerService.LogInformation( "Clearing Redirect Cache", "REDIRECT CACHE CLEAR", source: nameof( RedirectService ) );

			cacheService.ClearFromCache(GetRedirectCacheKey(PermanentRedirectsInfo.OBJECT_TYPE.ToLowerInvariant()), CacheConfiguration);
			cacheService.ClearFromCache(GetRedirectCacheKey(TemporaryRedirectsInfo.OBJECT_TYPE.ToLowerInvariant()), CacheConfiguration);
			cacheService.ClearFromCache(GetRedirectCacheKey(RegexRedirectsInfo.OBJECT_TYPE.ToLowerInvariant()), CacheConfiguration);
		}


		public IEnumerable<Redirect> GetRedirects()
		{
			List<Redirect> redirects = new List<Redirect>();
			redirects.AddRange(GetPermanentRedirects());
			redirects.AddRange(GetTemporaryRedirects());
			redirects.AddRange(GetRegexRedirects());
			return redirects;
		}

		public IEnumerable<Redirect> GetRedirects(RedirectMode redirectMode)
		{
			IEnumerable<Redirect> redirects = GetRedirects();

			switch (redirectMode)
			{
				case RedirectMode.AbsolutePath:
					// Do nothing here as AbsoluteUri and Query will never match on exact match...
					// Possible performance update here
					break;
				case RedirectMode.AbsoluteUri:
					redirects = redirects.Where(x =>
					{
						try
						{
							Uri uri = new Uri(x.MatchURL);
						}
						catch (Exception)
						{
							return false;
						}
						return true;
					}
					);
					break;
				case RedirectMode.PathAndQuery:
					redirects = redirects.Where(x => x.MatchURL.Contains("?"));
					break;
				default:
					break;
			}

			return redirects;
		}

		public Redirect MatchRedirect(string matchUrl, RedirectMode redirectMode = RedirectMode.AbsolutePath)
		{
			IEnumerable<Redirect> redirects = GetRedirects(redirectMode);
			redirects = redirects.OrderBy(x => x.IsRegexMatch).ThenByDescending(x=>x.Priority).ThenByDescending(x => x.MatchURL.Length);

			if (redirectMode == RedirectMode.PathAndQuery)
			{
				matchUrl = matchUrl.TrimStart('/');
			}

			foreach (Redirect redirect in redirects)
			{
				if (redirect.IsRegexMatch)
				{
					Match match = redirect.RegexRule.Match(matchUrl);
					if (match.Success)
					{
						return redirect;
					}
				}
				else
				{
					var slashlessMatchUrl = matchUrl.TrimStart('/');
					var slashMatchUrl = "/" + slashlessMatchUrl;
					if (redirect.MatchURL.Equals(slashlessMatchUrl, StringComparison.OrdinalIgnoreCase) || redirect.MatchURL.Equals(slashMatchUrl, StringComparison.OrdinalIgnoreCase))
					{
						return redirect;
					}
				}
			};
			return null;

		}

		private IEnumerable<Redirect> GetPermanentRedirects()
		{
			IEnumerable<Redirect> loadFunction( CacheSettings cacheSettings )
			{
				// This gets called on app start which causes a log exception error
				//loggerService.LogInformation( "Retrieving Permanent Redirects", "PERMANENT REDIRECTS LOADING", source: nameof( RedirectService ) );

				List<Redirect> redirects = new List<Redirect>();
				var permanentRedirects = PermanentRedirectsInfoProvider.GetPermanentRedirects().OnSite(SiteID).ToList();
				permanentRedirects.ForEach(x =>
				{
					try
					{
						var redirect = x.ToRedirect();
						if (redirect.IsValid)
						{
							redirects.Add(redirect);
						}
					}
					catch (Exception) { }
				});
				return redirects;
			}


			return cacheService.GetFromRotatingCache( loadFunction, GetRedirectCacheKey(PermanentRedirectsInfo.OBJECT_TYPE.ToLowerInvariant()), null, CacheConfiguration);
		}


		private IEnumerable<Redirect> GetTemporaryRedirects()
		{
			IEnumerable<Redirect> loadFunction( CacheSettings cacheSettings )
			{
				// This gets called on app start which causes a log exception error
				//loggerService.LogInformation( "Retrieving Temporary Redirects", "TEMPORARY REDIRECTS LOADING", source: nameof( RedirectService ) );

				List<Redirect> redirects = new List<Redirect>();
				var temporaryRedirects = TemporaryRedirectsInfoProvider.GetTemporaryRedirects().OnSite(SiteID).ToList();
				temporaryRedirects.ForEach(x =>
				{
					try
					{
						var redirect = x.ToRedirect();
						if (redirect.IsValid)
						{
							redirects.Add(redirect);
						}
					}
					catch (Exception) { }
				});
				return redirects;
			}


			return cacheService.GetFromRotatingCache(loadFunction, GetRedirectCacheKey(TemporaryRedirectsInfo.OBJECT_TYPE.ToLowerInvariant()), null, CacheConfiguration);
		}


		private IEnumerable<Redirect> GetRegexRedirects()
		{
			IEnumerable<Redirect> loadFunction( CacheSettings cacheSettings )
			{
				// This gets called on app start which causes a log exception error
				//loggerService.LogInformation( "Retrieving Regex Redirects", "REGEX REDIRECTS LOADING", source: nameof( RedirectService ) );

				List<Redirect> redirects = new List<Redirect>();
				var regexRedirects = RegexRedirectsInfoProvider.GetRegexRedirects().OnSite(SiteID).ToList();
				regexRedirects.ForEach(x =>
				{
					try
					{
						var redirect = x.ToRedirect();
						if (redirect.IsValid)
						{
							redirects.Add(redirect);
						}
					}
					catch (Exception) { }
				});
				return redirects;
			}


			return cacheService.GetFromRotatingCache(loadFunction, GetRedirectCacheKey(RegexRedirectsInfo.OBJECT_TYPE.ToLowerInvariant()), null, CacheConfiguration);
		}


		private string GetRedirectCacheKey(string objectTypeClassName)
		{
			return $"{NamePrefix}|{objectTypeClassName}";
		}


	}

}
