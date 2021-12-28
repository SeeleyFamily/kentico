using CMS.SiteProvider;
using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Models;
using Launchpad.Infrastructure.Abstractions.Services;
using Launchpad.Infrastructure.Extensions;
using System;


namespace Launchpad.Infrastructure.Services
{

	public class SiteService : ISiteService, IPerScopeService
	{
		#region Fields
		private readonly Lazy<ICacheService> cacheService;
		#endregion


		public SiteService
		(
			Lazy<ICacheService> cacheService
		)
		{
			this.cacheService = cacheService;
		}



		public Site GetSite(int id)
		{
			return cacheService.Value.GetFromCache((cs) =>
		   {
			   SiteInfo siteInfo = SiteInfo.Provider.Get(id);

			   return siteInfo.ToSite();
		   },

			cacheKey: $"site|{id}",
			cacheConfiguration: cacheService.Value.CreateGlobalConfiguration());
		}


		public Site GetSite(string codeName)
		{
			return cacheService.Value.GetFromCache((cs) =>
		   {
			   SiteInfo siteInfo = SiteInfo.Provider.Get(codeName);

			   return siteInfo.ToSite();
		   },

			cacheKey: $"site|{codeName}",
			cacheConfiguration: cacheService.Value.CreateGlobalConfiguration());
		}


	}

}
