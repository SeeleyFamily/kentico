using System.Collections.Generic;
using System.Linq;
using CMS.DocumentEngine;
using CMS.DocumentEngine.Types.Common;
using CMS.Tests;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Models;
using Launchpad.Infrastructure.Utilities;
using NUnit.Framework;


namespace Launchpad.Infrastructure.Tests.Services
{

	public class BannerServiceTests : IntegrationTests
	{
		#region Fields
		private readonly IBannerService service;
		#endregion


		public BannerServiceTests()
		{
			// Default service setup
			service = ServiceCreatorUtility.CreateBannerService();
		}



		[Test]
		public void GetsCookieBanner()
		{
			// Arrange
			CookieBanner node = DocumentHelper.GetDocuments<CookieBanner>()
											  .TopN(1)
											  .Published()
											  .OrderBy("NodeOrder")
											  .FirstOrDefault();


			// Act
			Banner banner = service.GetCookieBanner();



			// Assert
			Assert.IsNotNull(banner);
			Assert.AreEqual(node.Content, banner.Content);
			Assert.AreEqual(node.NodeID, banner.NodeID);
		}


		[Test]
		public void GetsGlobalNotifications()
		{
			// Arrange
			GlobalContent contentNode = DocumentHelper.GetDocuments<GlobalContent>()
													  .TopN(1)
													  .Column("NodeAliasPath")
													  .Published()
													  .FirstOrDefault();

			IEnumerable<NotificationBanner> nodes = DocumentHelper.GetDocuments<NotificationBanner>()
																  .Path(contentNode.NodeAliasPath, PathTypeEnum.Section)
																  .Published()
																  .OrderBy("NodeOrder")
																  .ToArray();


			// Act
			IEnumerable<Banner> banners = service.GetGlobalNotificationBanners();



			// Assert
			Assert.IsNotNull(banners);
			Assert.IsNotEmpty(banners);
			Assert.AreEqual(nodes.Count(), banners.Count());
		}


		[Test]
		public void GetsPageNotifications()
		{
			// Arrange
			GlobalContent contentNode = DocumentHelper.GetDocuments<GlobalContent>()
													  .TopN(1)
													  .Column("NodeAliasPath")
													  .Published()
													  .FirstOrDefault();

			IEnumerable<NotificationBanner> nodes = DocumentHelper.GetDocuments<NotificationBanner>()
																  .WhereNotLike("NodeAliasPath", $"{contentNode.NodeAliasPath}/%")
																  .Published()
																  .OrderBy("NodeOrder")
																  .ToArray();

			if (!nodes.Any())
			{
				Assert.Warn("There are no notification banners on the site.");
				return;
			}

			IEnumerable<Banner> globals = service.GetGlobalNotificationBanners();


			// Act
			IEnumerable<Banner> banners = service.GetNotificationBanners(nodes.First().Parent.NodeAliasPath);



			// Assert
			Assert.IsNotNull(banners);
			Assert.IsNotEmpty(banners);
			Assert.Greater(banners.Count(), globals.Count());
		}

	}

}
