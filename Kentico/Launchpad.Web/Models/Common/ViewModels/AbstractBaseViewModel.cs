/*
 * Built with Common Launchpad 2.0.2
 */

using CMS.DocumentEngine.Types.Common;
using Custom.Core.Models;
using Custom.Infrastructure.Abstractions.Services;
using Launchpad.Core.Abstractions.Models;
using Launchpad.Core.Abstractions.Providers;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Attributes;
using Launchpad.Core.Constants;
using Launchpad.Core.Enums;
using Launchpad.Core.Extensions;
using Launchpad.Core.Models;
using Launchpad.Core.Utilities;
using Launchpad.Infrastructure.Extensions;
using Launchpad.Infrastructure.Kentico.Web.Attributes;
using Launchpad.Infrastructure.Kentico.Web.Models.ViewModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;


namespace Launchpad.Web.Models.Common.ViewModels
{

	public abstract partial class AbstractBaseViewModel : IViewModel
	{
		#region Properties

		/// <summary>
		/// Banners for the current page.
		/// </summary>
		public BannerViewModel Banners { get; set; }

		/// <summary>
		/// Breadcrumbs for the current page.
		/// </summary>
		public Breadcrumbs Breadcrumbs { get; set; }

		public string HeroTexture { get; set; }

		/// <summary>
		/// The current request's <see cref="HttpContextBase"/> object.
		/// </summary>
		public HttpContextBase HttpContext { get; }

		/// <summary>
		/// Container for the page's menus.
		/// </summary>
		public MenuViewModel Menus { get; set; }

		/// <summary>
		/// The page's metadata information such as title and description.
		/// </summary>
		public PageMetadata Metadata { get => Node.Metadata; }

		/// <summary>
		/// The <see cref="PageNode"/> of the current web request.
		/// </summary>
		public PageNode Node { get; protected set; }

		public bool UsePageBuilder { get; protected set; } = false;

		public List<PageBuilderTabComponentViewModel> PageBuilderTabsViewModels { get; protected set; }

		/// <summary>
		/// Use this property to force a viewmodel not to render and redirect to 404.
		/// </summary>
		public bool ShouldNotRender { get; protected set; } = false;

		public bool NoIndexNoFollow { get; protected set; } = false;

		public string RelCanonical { get; protected set; } = "";

		/// <summary>
		/// Container for global site wide settings.
		/// </summary>
		public SettingsViewModel Settings { get; set; }

		public SiteSettingsModel SiteSettings { get; set; }

		/// <summary>
		/// The view to display the view model in. Should be overridden and/or set in subclasses.
		/// </summary>
		public virtual string ViewPath { get; protected set; }

		public List<string> ViewPathFolders { get; protected set; }

		public static string AppEnvironment { get; } = ConfigurationManager.AppSettings["AppEnvironment"];

		public static bool IsLiveSite { get; } = ConfigurationManager.AppSettings["LiveSite"] == "true" ? true : false;


		protected static string AssemblyManifestVersion { get; } = Assembly.GetExecutingAssembly().ManifestModule.ModuleVersionId.ToString();

		/// <summary>
		/// Provides layout services for all pages.
		/// </summary>
		protected virtual ILayoutProvider LayoutProvider { get; set; }
		#endregion


		#region Fields
		private readonly IBannerService bannerService;
		private readonly ICurrentNodeProvider currentNodeProvider;
		private readonly ICurrentSiteProvider currentSiteProvider;
		private readonly IMenuService menuService;
		private readonly ISettingsService settingsService;
		private readonly ISiteSettingsService siteSettingsService;

		private bool isInitialized;
		private static Dictionary<Type, bool> pageBuilderMappings = new Dictionary<Type, bool>();
		private static Dictionary<Type, IEnumerable<PropertyInfo>> propertyMappings = new Dictionary<Type, IEnumerable<PropertyInfo>>();
		private Type type;
		#endregion



		/// <summary>
		/// An abstract base view model class with functionality common to every page on a site.
		/// </summary>
		/// <remarks>
		/// Do not edit this class in custom projects. In most cases, only <see cref="BaseViewModel"/>
		/// should inherit this class, and then other view models should inherit <see cref="BaseViewModel"/>.
		/// </remarks>
		public AbstractBaseViewModel
		(
			ILayoutProvider layoutProvider
		)
		{
			this.bannerService = layoutProvider.GetBannerService();
			this.currentNodeProvider = layoutProvider.GetCurrentNodeProvider();
			this.currentSiteProvider = layoutProvider.GetCurrentSiteProvider();
			this.HttpContext = layoutProvider.GetHttpContext();
			this.LayoutProvider = layoutProvider;
			this.menuService = layoutProvider.GetMenuService();
			this.settingsService = layoutProvider.GetSettingsService();
			this.siteSettingsService = (ISiteSettingsService)layoutProvider.GetSiteSettingsService();
			type = GetType();

			ViewPathFolders = new List<string>()
			{
				$"{NamespacePath.Common.GetAttribute<CodeDisplayNameTypeAttribute>().DisplayName}",
				$"{NamespacePath.Custom.GetAttribute<CodeDisplayNameTypeAttribute>().DisplayName}",
			};
		}



		public string GetAssemblyGuid()
		{
			return AssemblyManifestVersion;
		}


		public string GetCSSPath()
		{
			return $"/dist/{GetViewFolderName()}.min.css?v={AssemblyManifestVersion}".ToLower();
		}


		public string GetJavaScriptPath()
		{
			return $"/dist/{GetViewFolderName()}.min.js?v={AssemblyManifestVersion}".ToLower();
		}


		/// <summary>
		/// Entry point for populating a view model's properties. Should only be called once, typically by Dependency Injection factories. Do not call this method manually.
		/// </summary>
		public virtual void InitPopulate()
		{
			if (isInitialized)
			{
				// Prevent calls from repopulating the view model
				throw new Exception("View model is already initialized.");
			}


			Node = currentNodeProvider.GetCurrentNode();

			MapKenticoFields();
			Populate();
			PopulateDefaults();
			PopulateHead();
			ConfigurePageBuilder();


			isInitialized = true;
		}


		protected virtual void ConfigurePageBuilder()
		{
			// If the EnablePageBuilder attribute is present, enable PageBuilder
			if (!pageBuilderMappings.ContainsKey(type))
			{
				pageBuilderMappings.Add(type, (GetType().GetCustomAttribute<EnablePageBuilder>() != null));
			}


			UsePageBuilder = pageBuilderMappings[type];

			if (UsePageBuilder)
			{
				PopulateTabWidgetViewModels();
			}
		}


		protected virtual void PopulateTabWidgetViewModels()
		{
			var tabWidgets = Node.PageBuilderWidgets?
							.GetWidgetVariants(WidgetIdentifier.TabWidget)
							.Where(x => !string.IsNullOrWhiteSpace(x.PropertiesDictionary["tabNames"]?.ToString()))
							.Select(x => new
							{
								Variant = x,
								WidgetProperties = (x.Properties as JObject).ToObject<Widgets.TabWidgetProperties>()
							})
							.Select(x => new
							{
								x.Variant.Identifier,
								x.WidgetProperties.TabNames,
								x.WidgetProperties.Index
							})
							.ToList();

			PageBuilderTabsViewModels = tabWidgets?
				.Select(x => new PageBuilderTabComponentViewModel(Node.PageBuilderWidgets, x.TabNames)
				{
					Index = x.Index
				})
				.ToList() ?? new List<PageBuilderTabComponentViewModel>();
		}


		/// <remarks>
		/// TODO: This method and its dictionary may be better off in some other class, such as a static utility class
		/// </remarks>
		protected virtual IEnumerable<PropertyInfo> GetMappableProperties()
		{
			// If the type has been mapped already, use the cached results
			if (propertyMappings.ContainsKey(type))
			{
				return propertyMappings[type];
			}


			// Map types & add to the cache dictionary
			IEnumerable<PropertyInfo> properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
													   .Where(k => k.CanWrite && Node.Fields.Any(f => f.Key.ToLower() == k.Name.ToLower()))
													   .ToArray();


			propertyMappings.Add(type, properties);
			return properties;
		}


		/// <summary>
		/// Maps the view model's own properties to matching values in the current <see cref="PageNode.Fields"/> dictionary. Fields and properties are matched on a 1-to-1 
		/// name basis, with casing ignored.
		/// </summary>
		protected virtual void MapKenticoFields()
		{
			if (Node == null || Node.Fields == null || !Node.Fields.Any())
			{
				return;
			}


			foreach (PropertyInfo property in GetMappableProperties())
			{
				// Get the corresponding value from the Node field
				object value = Node.Fields[property.Name];

				if (value == null)
				{
					continue;
				}


				if (property.PropertyType != value.GetType())
				{
					// Can a conversion be made?
					try
					{
						value = Convert.ChangeType(value, property.PropertyType);
					}
					catch (Exception)
					{
						// TODO: Since these types are incompatible, remove the type for future consideration
						continue;
					}
				}


				// 1-to-1 match, set the value automatically
				property.SetValue(this, value);
			}
		}


		/// <summary>
		/// Override <see cref="Populate"/> in subclasses to perform specific population and mapping operations. Called during <see cref="InitPopulate"/>.
		/// </summary>
		protected virtual void Populate()
		{
			PopulateBanners();
			PopulateBreadcrumbs();
			PopulateMenus();
			PopulateSettings();
			PopulateSiteSettings();
		}


		/// <summary>
		/// Populates the page's banners if they have not already been closed. (Uses the "Banners" guid collection cookie.)
		/// </summary>
		protected virtual void PopulateBanners()
		{
			// Init & retrieve banners
			Banners = new BannerViewModel();
			Banner cookieBanner = bannerService.GetCookieBanner();
			IEnumerable<Banner> banners = (Node != null) ?
											bannerService.GetNotificationBanners(Node.NodeAliasPath) :
											bannerService.GetGlobalNotificationBanners();


			// Get previously closed banners
			IEnumerable<Guid> closedBanners = Enumerable.Empty<Guid>();

			// TODO: Move cookie name magic string to constant
			if (HttpContext.Request.Cookies[BannerConstants.BannerCookies]?.Value is string settings)
			{
				settings = HttpUtility.UrlDecode(settings);
				// Convert delimited cookie to banner GUIDs
				closedBanners = settings.Split(',').Select(g =>
				{
					if (Guid.TryParse(g, out Guid guid))
					{
						return guid;
					}

					return Guid.Empty;
				})
					.Where(g => g != Guid.Empty);
			}


			// Display cookie banner?
			if (cookieBanner != null && !closedBanners.Contains(cookieBanner.NodeGuid))
			{
				Banners.CookieBanner = cookieBanner;
			}


			// Display a notification banner?
			if (banners != null && banners.FirstOrDefault(b => !closedBanners.Contains(b.NodeGuid)) is Banner banner)
			{
				Banners.NotificationBanner = banner;
			}
		}


		/// <summary>
		/// Populates the page's breadcrumb <see cref="PageNode"/> collection.
		/// </summary>
		protected virtual void PopulateBreadcrumbs()
		{
			Breadcrumbs = menuService.GetBreadcrumbs(Node.NodeID);
		}


		/// <summary>
		/// Called at the end of population, can be overridden in subclasses to set default values for properties that were not set prior, such as <see cref="PageMetadata"/> values.
		/// </summary>
		protected virtual void PopulateDefaults()
		{
			if (String.IsNullOrWhiteSpace(Metadata.Title))
			{
				Metadata.Title = Node?.DocumentName;
			}

			Metadata.Title = Metadata.Title.Replace("{DocumentName}", Node?.DocumentName);
			if (Settings != null && !string.IsNullOrWhiteSpace(Settings.SitePrefix))
			{
				Metadata.Title = $"{Metadata.Title} | { Settings.SitePrefix }";
			}


			if (string.IsNullOrWhiteSpace(Metadata.OgImage))
			{
				Metadata.OgImage = Node.Preview?.PreviewImage;
			}

			if (!string.IsNullOrWhiteSpace(Metadata.OgImage))
			{
				if (Metadata.OgImage.StartsWith("~"))
				{
					Metadata.OgImage = $"{HttpContext.Request.Url.Scheme}://{HttpContext.Request.Url.Authority}{Metadata.OgImage.TrimStart('~')}";
				}
			}

			if (string.IsNullOrWhiteSpace(Metadata.TwitterCard))
			{
				Metadata.TwitterCard = "summary_large_image";
			}

			Metadata.OgTitle = CoalesceUtility.CoalesceWithoutWhitespace(Metadata.OgTitle, Metadata.Title);
			Metadata.OgDescription = CoalesceUtility.CoalesceWithoutWhitespace(Metadata.OgDescription, Metadata.Description);
		}


		protected virtual void PopulateHead()
		{
			if (!IsLiveSite)
			{
				NoIndexNoFollow = true;
			}
			else if (Constants.NonIndexablePaths.Any(x => HttpContext.Request.Url.AbsolutePath.StartsWith(x)))
			{
				NoIndexNoFollow = true;
			}
			else
			{
				var presentationUrl = currentSiteProvider.GetCurrentSite().PresentationUrl.ToLower();
				var currentRequestHost = HttpContext.Request.Url.Host.ToLower();
				if (!presentationUrl.Contains(currentRequestHost))
				{
					NoIndexNoFollow = true;
				}
				else
				{
					NoIndexNoFollow = Node.Fields.GetBoolValue(nameof(BasePage.NoIndexNoFollow));
				}
			}

			RelCanonical = Metadata.CanonicalUrl;
			if (string.IsNullOrWhiteSpace(RelCanonical))
			{
				RelCanonical = HttpContext.Request.Url.AbsoluteUri;
				if (!string.IsNullOrWhiteSpace(HttpContext.Request.Url.Query))
				{
					RelCanonical = RelCanonical.Replace(HttpContext.Request.Url.Query, String.Empty);
				}
			}
		}


		/// <summary>
		/// Populates the layout menus on the page.
		/// </summary>
		protected virtual void PopulateMenus()
		{
			Menus = new MenuViewModel
			{
				FooterMenu = menuService.GetFooterMenu(),
				NavigationMenu = menuService.GetNavigationMenu(),
				SubFooterMenu = menuService.GetSubFooterMenu(),
				UtilityMenu = menuService.GetUtilityMenu()
			};
		}


		protected virtual void PopulateSettings()
		{
			Settings = new SettingsViewModel
			{
				GtmContainerId = settingsService.GetSetting(SettingConstants.GoogleTagManagerContainerId),
				GoogleSiteVerificationCode = settingsService.GetSetting(SettingConstants.GoogleSiteVerificationCode),
				GoogleAnalyticsId = settingsService.GetSetting(SettingConstants.GoogleAnalyticsId),
				SitePrefix = settingsService.GetSetting(SettingConstants.CMSSitePrefix),
				FbVerificationSettingKey = settingsService.GetSetting(SettingConstants.FbVerificationSettingKey)
			};
		}

		protected virtual void PopulateSiteSettings()
		{
			SiteSettings = siteSettingsService.GetFromGlobalContent().FirstOrDefault();
		}

		private string GetViewFolderName()
		{
			if (ViewPathFolders.Any(x => ViewPath.Contains($"{x}/")))
			{
				return ViewPath.Split('/')[1];
			}
			return ViewPath.Split('/')[0];
		}
	}

}
