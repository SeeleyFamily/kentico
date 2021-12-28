using System;
using System.ComponentModel;
using System.Configuration;
using CMS.DataEngine;
using CMS.Helpers;
using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Infrastructure.Abstractions.Services;


namespace Launchpad.Infrastructure.Services
{

	public class SettingsService : ISettingsService, IPerScopeService
	{
		#region Fields
		private readonly ICacheService cacheService;
		private readonly ICacheConfiguration cacheConfiguration;
		#endregion


		public SettingsService
		(
			ICacheService cacheService,
			ICacheConfiguration cacheConfiguration
		)
		{
			this.cacheService = cacheService;
			this.cacheConfiguration = cacheConfiguration;
		}



		public string GetSetting( string settingCodeName )
		{
			return GetSetting<string>( settingCodeName );
		}


		public T GetSetting<T>( string settingCodeName )
		{
			T loadFunction( CacheSettings cs )
			{
				// Search site-specific first
				string value = SettingsKeyInfoProvider.GetSettingsKeyInfo( settingCodeName, cacheConfiguration.SiteID )
													  ?.KeyValue;

				// Try to retrieve the value from global settings instead
				if( value == null )
				{
					value = SettingsKeyInfoProvider.GetSettingsKeyInfo( new SettingsKeyName( settingCodeName ) )
												   ?.KeyValue;
				}

				// Fall back to web.config
				if( value == null )
				{
					value = ConfigurationManager.AppSettings[settingCodeName];
				}
				else
				{
					// Add a cache dependency
					cs.CacheDependency = CacheHelper.GetCacheDependency( new string[] { $"cms.settingskey|byname|{settingCodeName.ToLower()}" } );
				}


				// Couldn't find the setting, return the default value for <T>
				if( String.IsNullOrWhiteSpace( value ) )
				{
					return default;
				}


				// Try to convert the value to <T>
				TypeConverter converter = TypeDescriptor.GetConverter( typeof( T ) );

				if( converter == null )
				{
					return default;
				}


				try
				{
					return ( T ) converter.ConvertFromString( value );
				}
				catch( Exception )
				{
					return default;
				}
			}


			// Get or create the setting from cache
			T setting = cacheService.GetFromCache( loadFunction, $"settings|{settingCodeName.ToLower()}" );

			if( setting != null )
			{
				return setting;
			}


			// Couldn't get or convert string to setting type
			return default;
		}
	}

}
