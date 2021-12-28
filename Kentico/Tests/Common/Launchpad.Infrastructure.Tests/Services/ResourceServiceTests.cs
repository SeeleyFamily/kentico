using System.Linq;
using CMS.Localization;
using CMS.Tests;
using Launchpad.Core.Services;
using Launchpad.Infrastructure.Abstractions.Services;
using Launchpad.Infrastructure.Utilities;
using NUnit.Framework;


namespace Launchpad.Infrastructure.Tests.Services
{

	public class ResourceServiceTests : IntegrationTests
	{
		#region Fields
		private readonly ICacheService cacheService;
		private readonly IResourceService service;
		#endregion


		public ResourceServiceTests( )
		{
			// Default service setup
			cacheService = ServiceCreatorUtility.CreateCacheService();
			service = ServiceCreatorUtility.CreateResourceService( );
		}



		[Test]
		public void CachesAllResourceStrings( )
		{
			// Arrange
			ResourceStringInfo info = GetRandomResourceString();


			// Act
			service.LoadCache();

			string cachedString = cacheService.GetFromCache<string>( cs =>
			{
				Assert.Fail( "Attempting to get cached item resulted in load method being executed." );
				return null;
			},
				$"|ResourceString|{info.StringKey}|{info.CultureCode}"
			);


			// Assert
			Assert.AreEqual( info.TranslationText, cachedString );
		}



		[Test]
		public void CachesSingleResourceStrings( )
		{
			// Arrange
			ResourceStringInfo info = GetRandomResourceString();


			// Act
			string initial = service.GetString( info.StringKey, info.CultureCode );

			string cachedString = cacheService.GetFromCache<string>( cs =>
			{
				Assert.Fail( "Attempting to get cached item resulted in load method being executed." );
				return null;
			},
				$"|ResourceString|{info.StringKey}|{info.CultureCode}"
			);


			// Assert
			Assert.IsNotEmpty( initial );
			Assert.IsNotEmpty( cachedString );
			Assert.AreEqual( info.TranslationText, initial );
			Assert.AreEqual( info.TranslationText, cachedString );
		}



		[Test]
		public void RetrievesDefaultCulture( )
		{
			// Arrange
			ResourceStringInfo info = GetRandomResourceString();


			// Act
			string initial = service.GetString( info.StringKey );


			// Assert
			Assert.IsNotEmpty( initial );
			Assert.AreEqual( info.TranslationText, initial );
		}



		private ResourceStringInfo GetRandomResourceString( )
		{
			return ResourceStringInfo.Provider.Get()
											 .Source( s => s.InnerJoin( "CMS_ResourceTranslation Translation", "Translation.TranslationStringID", "CMS_ResourceString.StringID" ) )
											 .Source( s => s.InnerJoin( "CMS_Culture Culture", "Culture.CultureID", "Translation.TranslationCultureID" ) )
											 .OrderBy( "NewID()" )
											 .FirstOrDefault();
		}

	}

}
