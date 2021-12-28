using System;
using System.Collections.Generic;
using System.Linq;
using CMS.DocumentEngine;
using CMS.DocumentEngine.Types.Common;
using CMS.Tests;
using Launchpad.Core.Abstractions.Models;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Models;
using Launchpad.Infrastructure.Extensions;
using Launchpad.Infrastructure.Utilities;
using NUnit.Framework;


namespace Launchpad.Infrastructure.Tests.Services
{

	public class DocumentServiceTTests : IntegrationTests
    {
        #region Fields
        private readonly IDocumentService<Home> homepageService;
        #endregion


        public DocumentServiceTTests()
        {
            // Default service setup
            homepageService = ServiceCreatorUtility.CreateDocumentService<Home>(isCached: true);
        }



		[Test]
		public void AllowsAccessToRootToPublic( )
		{
			// Arrange
			IUser user = new User
			{
				AccessControlList = new List<AccessControlItem>
				{
					new AccessControlItem{ AclId = 1, IsAllowed = true }
				}
			};


			PageNode rootNode = DocumentHelper.GetDocuments( "CMS.Root" )
											  .TopN( 1 )
											  .Columns( "NodeACLID", "NodeSiteID" )
											  .FirstOrDefault()
											  .ToPageNode();


			// Act
			bool isAllowed = homepageService.IsNodeAuthorizedForUser( rootNode, user );


			// Assert
			Assert.IsTrue( isAllowed );
		}


		[Test]
        public void GetRootChildHomepageByPath()
        {
            // Arrange
            TreeNode root = DocumentHelper.GetDocuments().Path("/").TopN(1).FirstOrDefault();
            IEnumerable<Home> homepages = DocumentHelper.GetDocuments<Home>()
				.OnSite(root.NodeSiteID)
				.ToArray();


            // Act
            Guid rootGuid = root.NodeGUID;
            IEnumerable<Home> nodes = homepageService.GetByParent("/");


            // Assert
            Assert.IsNotNull(nodes);
            Assert.AreEqual(nodes.Count(), homepages.Count());
            Assert.AreEqual(nodes.FirstOrDefault().NodeID, homepages.FirstOrDefault().NodeID);
        }


        [Test]
        public void GetRootChildHomepageByGuid()
        {
            // Arrange
            TreeNode root = DocumentHelper.GetDocuments().Path("/").TopN(1).FirstOrDefault();
            IEnumerable<Home> homepages = DocumentHelper.GetDocuments<Home>()
				.OnSite(root.NodeSiteID)
				.ToArray();
			

            // Act
            Guid rootGuid = root.NodeGUID;
            IEnumerable<Home> nodes = homepageService.GetByParent(rootGuid);


            // Assert
            Assert.IsNotNull(nodes);
            Assert.AreEqual(nodes.Count(), homepages.Count());
            Assert.AreEqual(nodes.FirstOrDefault().NodeID, homepages.FirstOrDefault().NodeID);
        }


        [Test]
        public void GetRootChildHomepageById()
        {
            // Arrange
            TreeNode root = DocumentHelper.GetDocuments().Path("/").TopN(1).FirstOrDefault();
            IEnumerable<Home> homepages = DocumentHelper.GetDocuments<Home>()
				.OnSite(root.NodeSiteID)
				.WhereEquals("NodeParentID", root.NodeID)
				.ToArray();

            // Act
            IEnumerable<Home> nodes = homepageService.GetByParent(1);

            // Assert
            Assert.IsNotNull(nodes);
            Assert.AreEqual(nodes.Count(), homepages.Count());
            Assert.AreEqual(nodes.FirstOrDefault().NodeID, homepages.FirstOrDefault().NodeID);
        }

    }

}
