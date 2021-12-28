using CMS.DataEngine;
using CMS.Helpers;
using CMS.Localization;
using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Core.Services;
using Launchpad.Infrastructure.Abstractions.Services;
using System;
using System.Linq;


namespace Launchpad.Infrastructure.Services
{

	public class ResourceService : IResourceService, IPerScopeService
	{
		#region Fields
		private readonly Lazy<ICacheService> cacheService;
		private const string defaultCulture = "en-US";
		#endregion


		public ResourceService
		(
			Lazy<ICacheService> cacheService
		)
		{
			this.cacheService = cacheService;
		}



		public virtual string GetString(string key, string culture = null, bool useCache = true)
		{
			if (culture == null)
			{
				culture = defaultCulture;
			}


			if (!useCache)
			{
				return GetResourceStringInfo(key, culture).TranslationText;
			}

			return cacheService.Value.GetFromCache(cs =>
		   {
			   ResourceStringInfo info = GetResourceStringInfo(key, culture);

			   if (info != null)
			   {
					// Update the cache settings with this info's dependency
					cs.CacheDependency = CacheHelper.GetCacheDependency($"cms.resourcestring|byid|{info.StringID}");
			   }

			   return info.TranslationText;
		   }, $"ResourceString|{key}|{culture}");
		}


		public virtual void LoadCache()
		{
			InfoDataSet<ResourceStringInfo> resourceStrings = CreateQueryBase()
																	.OrderBy("StringKey")
																	.TypedResult;

			if (!resourceStrings.Any())
			{
				return;
			}


			// Add each item to cache with its cache dependency
			string[] cacheDependencies = new string[1];

			foreach (ResourceStringInfo info in resourceStrings)
			{
				cacheDependencies[0] = $"cms.resourcestring|byid|{info.StringID}";
				cacheService.Value.SetCacheItem(info.TranslationText, $"ResourceString|{info.StringKey}|{info.CultureCode}", cacheDependencies);
			}
		}



		protected ObjectQuery<ResourceStringInfo> CreateQueryBase()
		{
			return ResourceStringInfo.Provider.Get()
											 .Columns("StringKey", "CultureCode", "StringID", "TranslationText")
											 .Source(s => s.InnerJoin("CMS_ResourceTranslation Translation", "Translation.TranslationStringID", "CMS_ResourceString.StringID"))
											 .Source(s => s.InnerJoin("CMS_Culture Culture", "Culture.CultureID", "Translation.TranslationCultureID"));
		}


		protected ResourceStringInfo GetResourceStringInfo(string key, string culture)
		{
			return CreateQueryBase()
						.TopN(1)
						.WhereEquals("StringKey", key)
						.WhereEquals("CultureCode", culture)
						.FirstOrDefault();
		}

	}

}
