using System.Linq;
using CMS.SiteProvider;
using CMS.Tests;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Models;
using Launchpad.Infrastructure.Abstractions.Services;
using Launchpad.Infrastructure.Utilities;
using NUnit.Framework;


namespace Launchpad.Infrastructure.Tests.Services
{

	public class SiteServiceTests : IntegrationTests
	{
		#region Fields
		private readonly ICacheService cacheService;
		private readonly ISiteService service;
		#endregion


		public SiteServiceTests()
		{
			// Default service setup
			cacheService = ServiceCreatorUtility.CreateCacheService();
			service = ServiceCreatorUtility.CreateSiteService();
		}



		[Test]
		public void CachesSite()
		{
			// Arrange
			SiteInfo siteInfo = GetRandomSiteInfo();


			// Act
			Site site = service.GetSite(siteInfo.SiteID);
			Site cachedSite = cacheService.GetFromCache<Site>((cs) =>
			   {
				   Assert.Fail("Attempting to get cached item resulted in load method being executed.");
				   return null;
			   },
				$"site|{siteInfo.SiteID}"
			);


			// Assert
			Assert.IsNotNull(site);
			Assert.IsNotNull(cachedSite);

			Assert.AreEqual(site, cachedSite);

			Assert.AreEqual(site.CodeName, cachedSite.CodeName);
			Assert.AreEqual(site.SiteGuid, cachedSite.SiteGuid);
		}


		[Test]
		public void GetsSiteByCodeName()
		{
			// Arrange
			SiteInfo siteInfo = GetRandomSiteInfo();


			// Act
			Site site = service.GetSite(siteInfo.SiteName);


			// Assert
			Assert.IsNotNull(site);

			Assert.AreEqual(site.CodeName, siteInfo.SiteName);
			Assert.AreEqual(site.SiteGuid, siteInfo.SiteGUID);
		}


		[Test]
		public void GetsSiteByID()
		{
			// Arrange
			SiteInfo siteInfo = GetRandomSiteInfo();


			// Act
			Site site = service.GetSite(siteInfo.SiteID);


			// Assert
			Assert.IsNotNull(site);

			Assert.AreEqual(site.CodeName, siteInfo.SiteName);
			Assert.AreEqual(site.SiteGuid, siteInfo.SiteGUID);
		}



		private SiteInfo GetRandomSiteInfo()
		{
			return SiteInfo.Provider.Get()
								   .TopN(1)
								   .OrderBy("NewID()")
								   .FirstOrDefault();
		}

	}

}
