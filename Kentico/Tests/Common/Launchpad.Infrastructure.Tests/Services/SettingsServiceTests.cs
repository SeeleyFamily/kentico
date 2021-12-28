using System.Configuration;
using CMS.DataEngine;
using CMS.Tests;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Constants;
using Launchpad.Infrastructure.Abstractions.Services;
using Launchpad.Infrastructure.Utilities;
using NUnit.Framework;


namespace Launchpad.Infrastructure.Tests.Services
{

	public class SettingsServiceTests : IntegrationTests
	{
		#region Fields
		private readonly ICacheService cacheService;
		private readonly ISettingsService service;
		#endregion


		public SettingsServiceTests( )
		{
			// Default service setup
			cacheService = ServiceCreatorUtility.CreateCacheService();
			service = ServiceCreatorUtility.CreateSettingsService( );
		}



		[Test]
		public void CachesSetting( )
		{
			// Arrange			
			string setting = service.GetSetting( SettingConstants.GoogleMapsApiKey );

			if( setting == null )
			{
				Assert.Warn( $"No setting with key {SettingConstants.GoogleMapsApiKey}." );
				return;
			}


			// Act
			string cachedSetting = cacheService.GetFromCache<string>( ( cs ) =>
			{
				Assert.Fail( "Attempting to get cached item resulted in load method being executed." );
				return null;
			},
				$"settings|{SettingConstants.GoogleMapsApiKey.ToLower()}"
			);



			// Assert
			Assert.IsNotNull( setting );
			Assert.IsNotEmpty( setting );
			Assert.AreEqual( setting, cachedSetting );
		}


		[Test]
		public void GetsSettingByCodeName( )
		{
			// Arrange
			string expected = SettingsKeyInfoProvider.GetSettingsKeyInfo( new SettingsKeyName( SettingConstants.GoogleMapsApiKey ) )
													 ?.KeyValue;

			if( expected == null )
			{
				Assert.Warn( $"No setting with key {SettingConstants.GoogleMapsApiKey}." );
				return;
			}
													


			// Act
			string setting = service.GetSetting( SettingConstants.GoogleMapsApiKey );


			// Assert
			Assert.AreEqual( expected, setting );
		}


		[Test]
		public void GetsSettingFromFallback( )
		{
			// Arrange
			string expected = ConfigurationManager.AppSettings["api:emailvalidationservice:apikey"];

			// Act
			string setting = service.GetSetting( "api:emailvalidationservice:apikey" );


			// Assert
			Assert.AreEqual( expected, setting );
		}


		[Test]
		public void GracefullyHandlesTypeMismatches( )
		{
			// Arrange


			// Act
			int setting = service.GetSetting<int>( "api:emailvalidationservice:apikey" );


			// Assert
			Assert.AreEqual( default(int), setting );
		}

	}

}
