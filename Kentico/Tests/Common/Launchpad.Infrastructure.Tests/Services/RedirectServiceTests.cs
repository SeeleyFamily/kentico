using System;
using System.Collections.Generic;
using CMS.Helpers;
using CMS.Tests;
using Launchpad.Core.Models;
using Launchpad.Infrastructure.Abstractions.Services;
using Launchpad.Infrastructure.Tests.Utilities;
using Launchpad.Infrastructure.Utilities;
using NUnit.Framework;


namespace Launchpad.Infrastructure.Tests.Services
{

	public class RedirectServiceTests : IntegrationTests
	{
		#region Fields
		private readonly IRedirectService service;
		#endregion

		public RedirectServiceTests()
		{
			// Default service setup
			service = ServiceCreatorUtility.CreateRedirectService();
		}

		[Test]
		public void GetRedirects()
		{
			// Arrange		
			var redirects = service.GetRedirects();

			// Act			

			// Assert
			Assert.IsNotNull(redirects);
			Assert.IsNotEmpty(redirects);
		}

		[Test]
		public void GetRedirectsCache()
		{
			// Arrange
			var redirects = service.GetRedirects();

			CacheSettings cacheSettings = new CacheSettings(5, String.Join("|", new string[] { "redirects", "redirects.permanentredirects", $"site:{(int)SiteID.Launchpad}" }));
			// Act			            
			var cachedRedirects = CacheHelper.Cache<List<Redirect>>(() =>
			{
				Assert.Fail("Attempting to get cached item resulted in load method being executed.");
				return null;
			},
				cacheSettings
			);


			// Assert
			Assert.IsNotNull(redirects);
			Assert.IsNotEmpty(redirects);
		}

		[Test]

		public void ClearRedirectsCache()
		{
			// Arrange
			var redirects = service.GetRedirects();
			service.ClearCache();

			CacheSettings cacheSettings = new CacheSettings(5, String.Join("|", new string[] { "redirects", "redirects.permanentredirects", $"site:{(int)SiteID.Launchpad}" }));

			bool loadMethodCalled = false;
			// Act			            
			var cachedRedirects = CacheHelper.Cache<object>(() =>
			{
				loadMethodCalled = true;
				return null;
			},
				cacheSettings
			);

			// Assert
			Assert.IsTrue(loadMethodCalled);
		}

		[Test]

		public void MultisiteRedirects()
		{
			// Arrange			
			var siteOneRedirects = service.GetRedirects();
			var multiSiteService = ServiceCreatorUtility.CreateRedirectService((int)SiteID.MultisiteExample);			
			var siteTwoRedirects = multiSiteService.GetRedirects();

			// Act			

			// Assert
			Assert.IsNotNull(siteOneRedirects);
			Assert.IsNotNull(siteTwoRedirects);
			Assert.AreNotEqual(siteOneRedirects, siteTwoRedirects);
		}
	}

}
