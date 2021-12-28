using System;
using System.Linq;
using System.Net.Http;
using CMS.DocumentEngine;
using CMS.SiteProvider;
using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Core.Abstractions.Models;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Abstractions.Specifications;
using Launchpad.Core.Configuration;
using Launchpad.Core.Models;
using Launchpad.Core.Services;
using Launchpad.Core.Specifications;
using Launchpad.Infrastructure.Abstractions.Factories;
using Launchpad.Infrastructure.Abstractions.Services;
using Launchpad.Infrastructure.Factories;
using Launchpad.Infrastructure.Services;


namespace Launchpad.Infrastructure.Utilities
{

	/// <summary>
	/// Provides a single factory set of methods for creating dependencies, mainly for testing or CMS modules.
	/// </summary>
	/// <remarks>
	/// DO NOT USE THIS IN THE LAUNCHPAD MVC PROJECTS. Use Dependency Injection instead. This utility is a stopgap intended for use
	/// in automated tests and in CMSApp where DI is difficult currently.</remarks>
	public class ServiceCreatorUtility
	{

		public static IAccountService CreateAccountService()
		{
			var cacheService = CreateLazy(CreateCacheService());


			return new AccountService(cacheService);
		}


		public static IAzureSearchClientFactory CreateAzureSearchClientFactory()
		{
			return new AzureSearchClientFactory();
		}


		public static IAzureSearchService CreateAzureSearchService()
		{
			var factory = CreateLazy(CreateAzureSearchClientFactory());

			return new AzureSearchService(factory);
		}


		public static IBannerService CreateBannerService()
		{
			var cacheService = CreateCacheService();
			var queryConfig = CreateDocumentQueryConfiguration();


			return new BannerService(cacheService, queryConfig);
		}


		public static ICacheService CreateCacheService(bool isCached = true)
		{
			Lazy<ICacheConfiguration> cacheConfig = CreateLazy( ( ICacheConfiguration ) new CacheConfiguration() );
			cacheConfig.Value.IsCached = isCached;
			cacheConfig.Value.CacheMinutes = 5;
			cacheConfig.Value.SiteID = 1;


			return new CacheService(cacheConfig);
		}


		public static ICategoryService CreateCategoryService(int siteID = 1)
		{
			var cacheService = CreateCacheService();
			var siteContextConfiguration = CreateSiteContextConfiguration();
			siteContextConfiguration.SiteId = siteID;
			return new CategoryService(cacheService, siteContextConfiguration);
		}


		public static ICountryService CreateCountryService()
		{
			var cacheService = CreateCacheService();

			return new CountryService(cacheService);
		}


		public static ICustomTableService CreateCustomTableService(bool isCached = true)
		{
			var cacheService = CreateLazy(CreateCacheService(isCached));
			return new CustomTableService(cacheService);
		}


		public static IDocumentService CreateDocumentService(bool isPreview = false, bool isCached = true)
		{
			// Can't cache when preview
			isCached = isPreview ? false : isCached;


			var cacheService = CreateCacheService(isCached);
			var queryConfig = CreateDocumentQueryConfiguration(isPreview);
			var siteService = CreateLazy(CreateSiteService());
			var documentUrlPathInfoService = CreateLazy(CreateDocumentUrlPathInfoService());

			return new DocumentService(cacheService, queryConfig, documentUrlPathInfoService);
		}


		public static IDocumentService<T> CreateDocumentService<T>(bool isPreview = false, bool isCached = true) where T : TreeNode, new()
		{
			// Can't cache when preview
			isCached = isPreview ? false : isCached;

			var cacheService = CreateCacheService(isCached);
			var queryConfig = CreateDocumentQueryConfiguration(isPreview);

			return new DocumentService<T>(queryConfig, cacheService);
		}


		public static IDocumentQueryConfiguration CreateDocumentQueryConfiguration(bool isPreview = false)
		{
			var queryConfig = new DocumentQueryConfiguration
			{
				SiteId = 1
			};

			return queryConfig;
		}


		public static IDocumentSpecification CreateDocumentSpecification()
		{
			var specification = new DocumentSpecification();

			return specification;
		}


		public static IDocumentUrlPathInfoService CreateDocumentUrlPathInfoService( )
		{
			ICacheService cacheService = CreateCacheService();
			var siteContextConfiguration = CreateSiteContextConfiguration();

			return new DocumentUrlPathInfoService( cacheService, siteContextConfiguration );
		}


		public static IEmailService CreateEmailService( )
		{
			Lazy<ILoggerService> loggerService = CreateLazy( CreateLoggerService() );

			return new EmailService( loggerService );
		}


		public static IEmailValidationService CreateEmailValidationService()
		{

			HttpClient client = new HttpClient();
			var loggerService = CreateLazy(CreateLoggerService());
			return new ExampleEmailValidationService(client, loggerService);
		}


		public static IGoogleMapService CreateGoogleMapService()
		{
			HttpClient httpClient = CreateHttpClient();
			ISettingsService settingsService = CreateSettingsService();


			return new GoogleMapService(httpClient, settingsService);
		}


		public static HttpClient CreateHttpClient()
		{
			return new HttpClient();
		}


		public static Lazy<T> CreateLazy<T>(T instance)
		{
			return new Lazy<T>(() => instance);
		}


		public static ILoggerService CreateLoggerService()
		{
			return new LoggerService();
		}


		public static IMenuService CreateMenuService()
		{
			var cacheService = CreateCacheService();
			var queryConfig = CreateDocumentQueryConfiguration();
			var documentService = CreateDocumentService();
			return new MenuService(cacheService, documentService, queryConfig);
		}


		public static IRedirectService CreateRedirectService(int siteID = 1, bool isCached = true)
		{
			var cacheService = CreateCacheService(isCached);
			var loggerService = CreateLoggerService();
			var siteContextConfiguration = CreateSiteContextConfiguration();
			siteContextConfiguration.SiteId = siteID;

			return new RedirectService(cacheService, loggerService, siteContextConfiguration);
		}


		public static IRelatedService CreateRelatedService()
		{
			var queryConfig = CreateDocumentQueryConfiguration();
			//return new RelatedService( queryConfig );
			throw new NotImplementedException();
		}


		public static IResourceService CreateResourceService()
		{
			var cacheService = CreateLazy(CreateCacheService());

			return new ResourceService(cacheService);
		}


		public static ISearchIndexSpecification CreateSearchIndexSpecification()
		{
			var specification = new SearchIndexSpecification();

			return specification;
		}


		public static ISettingsService CreateSettingsService()
		{
			ICacheService cacheService = CreateCacheService();

			var cacheConfig = new CacheConfiguration
			{
				IsCached = true,
				CacheMinutes = 5,
				SiteID = 1
			};


			return new SettingsService(cacheService, cacheConfig);
		}


		public static ISiteContextConfiguration CreateSiteContextConfiguration(bool isPreview = false)
		{
			var siteContextConfiguration = new SiteContextConfiguration
			{
				SiteId = 1
			};

			return siteContextConfiguration;
		}


		public static ISiteService CreateSiteService()
		{
			var cacheService = CreateLazy(CreateCacheService(true));

			return new SiteService(cacheService);
		}


		public static IUser CreateUser(int? id = null)
		{
			if (!id.HasValue)
			{
				id = new Random().Next(1001, 99999);
			}

			string timestamp = DateTime.Now.ToString("yyyyMMddhhmmss");


			IUser user = new User
			{
				Email = $"testuser.{timestamp}@riseinteractive.com",
				FirstName = "Test",
				LastName = "User",
				Roles = Enumerable.Empty<Role>(),
				UserId = id.Value,
				Username = $"TestUser-{timestamp}"
			};

			return user;
		}


		public static SiteInfo GetSiteInfo()
		{
			return SiteInfo.Provider.Get(1);
		}
	}

}