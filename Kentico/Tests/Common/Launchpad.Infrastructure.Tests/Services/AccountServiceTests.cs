using System.Linq;
using CMS.Membership;
using CMS.SiteProvider;
using CMS.Tests;
using Launchpad.Core.Abstractions.Models;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Infrastructure.Abstractions.Services;
using Launchpad.Infrastructure.Utilities;
using NUnit.Framework;


namespace Launchpad.Infrastructure.Tests.Services
{

	public class AccountServiceTests : IntegrationTests
	{
		#region Fields
		private readonly IKenticoUserService kenticoUserService;
		private readonly IAccountService service;
		#endregion


		public AccountServiceTests()
		{
			// Default service setup
			service = ServiceCreatorUtility.CreateAccountService();

			kenticoUserService = (IKenticoUserService)service;
		}



		[Test]
		public void EnsuresKenticoUser()
		{
			// Arrange
			IUser user = ServiceCreatorUtility.CreateUser();
			SiteInfo siteInfo = ServiceCreatorUtility.GetSiteInfo();


			// Act
			UserInfo userInfo = kenticoUserService.EnsureUser(user, siteInfo);


			// Assert
			Assert.IsNotNull(userInfo);
			Assert.Greater(userInfo.UserID, 0);
		}


		[Test]
		public void GetsPublicAnonymousUser()
		{
			// Arrange
			UserInfo userInfo = UserInfo.Provider.Get("public");
			SiteInfo siteInfo = ServiceCreatorUtility.GetSiteInfo();


			// Act
			IUser user = kenticoUserService.GetPublicAnonymousUser(siteInfo.SiteID);


			// Assert
			Assert.IsNotNull(user);
			Assert.AreEqual(userInfo.UserID, user.UserId);
			Assert.IsNotEmpty(user.AccessControlList);
		}



		[Test]
		public void SetsUserAcl()
		{
			// Arrange
			IUser user = ServiceCreatorUtility.CreateUser();
			SiteInfo siteInfo = ServiceCreatorUtility.GetSiteInfo();
			UserInfo userInfo = kenticoUserService.EnsureUser(user, siteInfo);


			// Act
			service.SetAcl(user, userInfo.UserID, siteInfo.SiteID);


			// Assert
			Assert.IsNotNull(user.AccessControlList);

			if (!user.AccessControlList.Any())
			{
				Assert.Warn("No ACL Items returned for Authorized User role; site may not be setup for permissions.");
			}
		}

	}

}
