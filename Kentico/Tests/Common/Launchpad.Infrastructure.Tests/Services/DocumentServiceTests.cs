using System.Linq;
using CMS.DocumentEngine;
using CMS.Helpers;
using CMS.Tests;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Abstractions.Specifications;
using Launchpad.Core.Models;
using Launchpad.Infrastructure.Utilities;
using NUnit.Framework;


namespace Launchpad.Infrastructure.Tests.Services
{

	public class DocumentServiceTests : IntegrationTests
	{
		#region Fields
		private readonly IDocumentService service;
		#endregion


		public DocumentServiceTests()
		{
			// Default service setup
			service = ServiceCreatorUtility.CreateDocumentService(isCached: true);
		}



		[Test]
		public void CachesNodesByIdToo()
		{
			// Arrange
			TreeNode expected = DocumentHelper.GetDocuments()
											  .Column( "NodeAliasPath" )
											  .TopN( 1 )
											  .WhereNotEquals( "NodeAliasPath", "/" )
											  .Published()
											  .FirstOrDefault();


			// Act
			PageNode node = service.Get( expected.NodeAliasPath );

			if (node == null)
			{
				Assert.Warn("Node wasn't retrieved");
				return;
			}


			var cachedItem = CacheHelper.GetItem($"|node|{node.NodeAliasPath}|site:{node.NodeSiteID}");
			var cachedIdItem = CacheHelper.GetItem($"|node|{node.NodeID}|site:{node.NodeSiteID}");
			var cachedData = CacheHelper.Cache<TreeNode>(() =>
			{
				Assert.Fail("Attempting to get cached item resulted in load method being executed.");
				return null;
			},
				new CacheSettings(5, new string[] { "", "node", $"{node.NodeID}", $"site:{node.NodeSiteID}" })
			);



			// Assert
			Assert.IsNotNull(cachedItem);
			Assert.IsNotNull(cachedIdItem);
			Assert.IsNotNull(cachedData);

			Assert.AreEqual(cachedItem, cachedIdItem);
			Assert.AreEqual(cachedIdItem, cachedData);
		}


		[Test]
		public void CreatesSitemap()
		{
			// TODO - move this to sitemap service test

			//// Arrange
			//XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";


			//// Act
			//XDocument xml = service.GetSitemap();

			//// Should have collection of URL nodes
			//IEnumerable<XElement> urls = xml.Descendants(ns + "url");

			//Uri.TryCreate(urls.FirstOrDefault().Descendants(ns + "loc").FirstOrDefault()?.Value, UriKind.Absolute, out Uri uri);
			//DateTime.TryParse(urls.FirstOrDefault().Descendants(ns + "lastmod").FirstOrDefault()?.Value, out DateTime lastModified);


			//// Assert
			//Assert.IsNotNull(xml);
			//Assert.IsNotEmpty(urls);

			//Assert.IsNotNull(uri);
			//Assert.IsTrue(uri.IsAbsoluteUri);

			//Assert.IsNotNull(lastModified);
			//Assert.IsTrue(lastModified != DateTime.MinValue);
		}


		[Test]
		public void FindsPageByPath()
		{
			// Arrange
			TreeNode node = DocumentHelper.GetDocuments()
										  .TopN( 1 )
										  .Columns( "DocumentName", "NodeID", "NodeParentID" )
										  .WhereNotEquals( "NodeAliasPath", "/" )
										  .NestingLevel( 1 )
										  .Published()
										  .OrderBy( "NewID()" )
										  .FirstOrDefault();


			IDocumentSpecification specification = ServiceCreatorUtility.CreateDocumentSpecification();
			specification.Path = node.Parent.NodeAliasPath;
			specification.PageSize = 1000000;


			// Act
			PagedResult<PageNode> result = service.Find( specification );
			PageNode match = result?.Items.FirstOrDefault( pn => pn.NodeID == node.NodeID );


			// Assert
			Assert.IsNotNull( result );
			Assert.IsNotEmpty( result?.Items );
			Assert.AreEqual( node.DocumentName, match?.DocumentName );
		}
	}

}
