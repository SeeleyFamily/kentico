using System.Collections.Generic;
using System.Linq;
using CMS.DocumentEngine;
using CMS.Tests;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Models;
using Launchpad.Infrastructure.Utilities;
using NUnit.Framework;


namespace Launchpad.Infrastructure.Tests.Services
{

	public class MenuServiceTests : IntegrationTests
	{
		#region Fields
		private readonly IMenuService service;
		#endregion


		public MenuServiceTests()
		{
			// Default service setup
			service = ServiceCreatorUtility.CreateMenuService();
		}



		[Test]
		public void BreadcrumbsExcludeNonViewableNodes()
		{
			//if (!DataClassInfoProvider.GetClasses().Any(x => x.ClassName.Equals(CMS.DocumentEngine.Types.Example.Detail.CLASS_NAME, System.StringComparison.InvariantCultureIgnoreCase)))
			//{
			//	Assert.Warn("Example does not exist in this implementation.");
			//	return;
			//}

			//// Arrange: Get an example node, which sits in a non-viewable /Examples/ folder
			//var node = DocumentHelper.GetDocuments<CMS.DocumentEngine.Types.Example.Detail>()
			//									 .TopN(1)
			//									 .Columns("NodeID", "NodeLevel")
			//									 .FirstOrDefault();

			//if (node == null)
			//{
			//	Assert.Warn("Example does not exist in this implementation.");
			//	return;
			//}

			//// Act
			//var breadcrumbs = service.GetBreadcrumbs(node.NodeID);



			//// Assert
			//Assert.IsNotNull(breadcrumbs);
			//Assert.IsNotEmpty(breadcrumbs.Items);
			//Assert.AreEqual(breadcrumbs.Items.Count(), node.NodeLevel);
		}


		[Test]
		public void GetsBreadcrumbs()
		{
			// Arrange: Get the lowest level node
			TreeNode lowestLevel = DocumentHelper.GetDocuments()
												 .TopN(1)
												 .Columns("NodeID")
												 .OrderByDescending("NodeLevel")
												 .FirstOrDefault();


			// Act
			var breadcrumbs = service.GetBreadcrumbs(lowestLevel.NodeID);


			// Assert
			Assert.IsNotNull(breadcrumbs);
			Assert.IsNotEmpty(breadcrumbs.Items);
		}


		[Test]
		public void GetsMenu()
		{
			// Arrange


			// Act
			IEnumerable<MenuItem> menu = service.GetNavigationMenu();



			// Assert
			Assert.IsNotNull(menu);
			Assert.IsNotEmpty(menu);
		}


		[Test]
		public void GetsSideMenu( )
		{
			// Arrange
			PageNode node = new PageNode
			{
				NodeAliasPath = "/example-content/Example-Common-Templates/Example-Blog-Author-Listing-Folder/example-blog-author-listing"
			};


			// Act
			IEnumerable<MenuItem> menu = service.GetSideNavMenu( node, 1 );



			// Assert
			Assert.IsNotNull( menu );
			Assert.IsNotEmpty( menu );
		}

	}

}
