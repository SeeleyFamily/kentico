using System.Collections.Generic;
using System.Linq;
using CMS.DataEngine;
using CMS.DocumentEngine;
using CMS.DocumentEngine.Types.Common;
using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Models;
using Launchpad.Infrastructure.Abstractions.Services;
using Launchpad.Infrastructure.Extensions;


namespace Launchpad.Infrastructure.Services
{

	public class BannerService : IBannerService, IPerScopeService
	{
		#region Fields
		private readonly ICacheService cacheService;
		private readonly IDocumentQueryConfiguration queryConfiguration;
		#endregion


		public BannerService
		(
			ICacheService cacheService,
			IDocumentQueryConfiguration queryConfiguration
		)
		{
			this.cacheService = cacheService;
			this.queryConfiguration = queryConfiguration;
		}



		public Banner GetCookieBanner( )
		{
			Banner banner = cacheService.GetFromCache( cs =>
			{
				CookieBanner node = DocumentHelper.GetDocuments<CookieBanner>()
												  .WithAllPageTypeColumns()
												  .WithPageNodeColumns()
												  .TopN( 1 )
												  .FromGlobalContent( queryConfiguration )
												  .FirstOrDefault();

				if( node == null )
				{
					return null;
				}


				// Add cache dependencies
				cs.AddNodeDependency( node );
				cs.AddTypeDependency(queryConfiguration.SiteName.ToLower(), CookieBanner.CLASS_NAME.ToLower());

				return ToModel( node );
			},

			cacheKey: $"banner|cookie",
			cacheDependencies: new string[]{
				$"nodes|{queryConfiguration.SiteName}|{CookieBanner.CLASS_NAME}|all".ToLower()
			}, 
			alwaysCache: true);


            return banner;
		}


		public IEnumerable<Banner> GetGlobalNotificationBanners( )
		{
			IEnumerable<Banner> banners = cacheService.GetFromCache( cs =>
			{
				IEnumerable<NotificationBanner> nodes = DocumentHelper.GetDocuments<NotificationBanner>()
																	  .WithAllPageTypeColumns()
																	  .WithPageNodeColumns()
                                                                      .OrderBy("NodeOrder")
																	  .FromGlobalContent( queryConfiguration );

				if( nodes == null || !nodes.Any() )
				{
					return Enumerable.Empty<Banner>().ToArray();
				}


				// Add cache dependencies
				cs.AddNodeDependencies( nodes );

				return nodes.Select( n => ToModel( n ) ).ToArray();
			},

			cacheKey: $"banner|global",
			cacheDependencies: new string[]{
				$"nodes|{queryConfiguration.SiteName}|{NotificationBanner.CLASS_NAME}|all".ToLower()
			},
			alwaysCache: true);


            return banners;
		}


		/// <summary>
		/// Returns notification banners for a given page. Includes global notifications.
		/// </summary>
		public IEnumerable<Banner> GetNotificationBanners( string path )
		{
			IEnumerable<Banner> banners = cacheService.GetFromCache( cs =>
			{
				IEnumerable<NotificationBanner> nodes = DocumentHelper.GetDocuments<NotificationBanner>()
																	  .Path( path, PathTypeEnum.Children )
																	  .WithAllPageTypeColumns()
																	  //.WithPageNodeColumns()
																	  .AddColumns( "NodeAliasPath", "NodeGUID", "NodeID", "NodeLevel", "NodeOrder" )
																	  .OrderBy( "NodeOrder" )
																	  .ApplyConfiguration( queryConfiguration );

				if( nodes == null || !nodes.Any() )
				{
					return Enumerable.Empty<Banner>();
				}


				// Add cache dependencies
				cs.AddNodeDependencies( nodes );

				return nodes.Select( n => ToModel( n ) );
			},

			cacheKey: $"banners|node|{path.ToLower()}",
			cacheDependencies: new string[]{
				$"nodes|{queryConfiguration.SiteName}|{NotificationBanner.CLASS_NAME}|all".ToLower()
			},
			alwaysCache: true);



            // Join to global banners and return
            return banners.Union( GetGlobalNotificationBanners() ).ToArray();
		}



		protected Banner ToModel( CookieBanner banner )
		{
			return new Banner
			{
				Title = banner.Title,
				Image = banner.Image,
				Content = banner.Content,
				CtaUrl = banner.CtaUrl,
				CtaLabel = banner.CtaText,
				IsCtaUsedToClose = banner.IsCtaUsedToClose,
				BannerType = banner.BannerType,
				NodeAliasPath = banner.NodeAliasPath,
				NodeGuid = banner.NodeGUID,
				NodeID = banner.NodeID,
				NodeLevel = banner.NodeLevel,
				NodeOrder = banner.NodeOrder
			};
		}


		protected Banner ToModel( NotificationBanner banner )
		{
			return new Banner
			{
				Title = banner.Title,
				Image = banner.Image,
				Content = banner.Content,
				CtaUrl = banner.CtaUrl,
				CtaLabel = banner.CtaText,
				IsCtaUsedToClose = banner.IsCtaUsedToClose,
				BannerType = banner.BannerType,
				NodeAliasPath = banner.NodeAliasPath,
				NodeGuid = banner.NodeGUID,
				NodeID = banner.NodeID,
				NodeLevel = banner.NodeLevel,
				NodeOrder = banner.NodeOrder
			};
		}

	}

}
