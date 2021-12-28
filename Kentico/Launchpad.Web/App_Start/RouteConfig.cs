using Kentico.Web.Mvc;
using Launchpad.Infrastructure.Kentico.Web.Extensions;
using System.Web.Mvc;
using System.Web.Routing;


namespace Launchpad.Web
{

	public class RouteConfig
	{

		public static void RegisterRoutes(RouteCollection routes)
		{
			// Ignore routes
			routes.IgnoreStaticAssetRoutes();

			// Image Optimization Route
			routes.MapImageOptimizationRoute();

			// Default Video Player Route
			routes.MapDefaultVideoPlayerRoute();

			// Maps routes to Kentico HTTP handlers and features enabled in ApplicationConfig.cs
			// Always map the Kentico routes before adding other routes. Issues may occur if Kentico URLs are matched by a general route, for example images might not be displayed on pages
			routes.Kentico().MapRoutes();

			// MVC attribute routing
			routes.MapMvcAttributeRoutes();

			// Localization routing
			routes.MapLocalizationRoutes();

			//< !-- ================ CUSTOM =============== -->
			//< !-- ======================================= -->
			//< !-- Custom project specific static routes   -->
			//< !-- ======================================= -->
			//< !-- ======================================= -->



			// Final default mappings
			routes.MapErrorRoutes();
			routes.MapHomeRoute();
			routes.MapAdminRoute();
			routes.MapRobots();
			routes.MapKenticoNodeRoutes();
			routes.MapRedirectRoutes();
			/*
				We should not have the default route below as it causes issues with the 404 handling;
			*/
			//routes.MapDefaultRoute();			
		}

	}

}
