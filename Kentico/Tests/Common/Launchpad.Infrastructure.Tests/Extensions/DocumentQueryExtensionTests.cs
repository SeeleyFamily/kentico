using System.Linq;
using CMS.DocumentEngine;
using CMS.DocumentEngine.Types.Common;
using CMS.Membership;
using CMS.Tests;
using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Infrastructure.Extensions;
using Launchpad.Infrastructure.Utilities;
using NUnit.Framework;


namespace Launchpad.Infrastructure.Tests.Services
{

	public class DocumentQueryExtensionTests : IntegrationTests
	{

		[Test]
		public void AppliesGlobalContentFilters()
		{
			// Arrange
			IDocumentQueryConfiguration queryConfig = ServiceCreatorUtility.CreateDocumentQueryConfiguration();


			// Act
			DocumentQuery<NotificationBanner> query = DocumentHelper.GetDocuments<NotificationBanner>()
																	.FromGlobalContent(queryConfig);

			var result = query.TypedResult;


			// Assert
			Assert.IsNotEmpty(result);
			Assert.IsTrue(result.First().NodeAliasPath.ToLower().Contains("global"));
		}


		[Test]
		public void AppliesUserPermissions()
		{
			// Arrange
			UserInfo userInfo = UserInfo.Provider.Get("public");


			// Act
			DocumentQuery<Home> query = DocumentHelper.GetDocuments<Home>()
													  .WhereUserIsAllowed(userInfo);

			var results = query.Result;
			var text = query.QueryText;


			// Assert (TODO: This is a weak test, currently only verifying that the query executes successfully.)
			Assert.Pass();
		}

	}

}
