using System.Collections.Generic;
using CMS.Tests;
using Launchpad.Infrastructure.Abstractions.Services;
using Launchpad.Infrastructure.Utilities;
using NUnit.Framework;


namespace Launchpad.Infrastructure.Tests.Services
{

	public class CacheServiceTests : IntegrationTests
	{
		#region Fields
		private readonly ICacheService service;
		#endregion


		public CacheServiceTests( )
		{
			// Default service setup
			service = ServiceCreatorUtility.CreateCacheService( isCached: true );
		}



		[Test]
		public void CachesObjectsWithLoaders( )
		{
			// Arrange
			string cacheKey = "tests|cacheobject";
			List<string> expected = new List<string> { "This", "Is", "Cached" };


			// Act
			List<string> cachedItem = service.GetFromCache( ( cs ) => expected, cacheKey );
			List<string> actual = service.GetFromCache<List<string>>( ( cs ) =>
				{
					Assert.Fail( "Attempting to get cached item resulted in load method being executed." );
					return null;
				},
				cacheKey
			);



			// Assert
			Assert.IsNotNull( cachedItem );
			Assert.IsNotNull( actual );

			Assert.AreEqual( expected, cachedItem );
			Assert.AreEqual( expected, actual );
			Assert.AreEqual( cachedItem, actual );

			Assert.IsNotEmpty( cachedItem );
			Assert.IsNotEmpty( actual );


			Assert.AreEqual( expected.Count, cachedItem.Count );
			Assert.AreEqual( expected.Count, actual.Count );
		}



		[Test]
		public void CachesObjectsWithSetters( )
		{
			// Arrange
			string cacheKey = "tests|setobject";
			List<string> firstVersion = new List<string> { "First", "Item" };
			List<string> expected = new List<string> { "This", "Is", "Cached" };


			// Act
			service.SetCacheItem( firstVersion, cacheKey );
			service.SetCacheItem( expected, cacheKey );

			List<string> actual = service.GetFromCache<List<string>>( ( cs ) =>
			{
				Assert.Fail( "Attempting to get cached item resulted in load method being executed." );
				return null;
			},
				cacheKey
			);



			// Assert
			Assert.IsNotNull( actual );
			Assert.IsNotEmpty( actual );
			Assert.AreEqual( expected, actual );
			Assert.AreEqual( expected.Count, actual.Count );
		}

	}

}
