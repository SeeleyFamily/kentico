using System.Web.Mvc;
using System.Web.Routing;
using Launchpad.Infrastructure.Kentico.Web.Constraints;

namespace Launchpad.Infrastructure.Kentico.Web.Extensions
{

	public static class RouteCollectionExtensions
	{

		/// <summary>
		/// Adds ignore directives to the <see cref="RouteCollection"/> for common static asset paths and files.
		/// </summary>
		public static void IgnoreStaticAssetRoutes(this RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			routes.IgnoreRoute("src/{*path}");
			routes.IgnoreRoute("dist/{*path}");
			routes.IgnoreRoute("assets/{*path}");
			routes.IgnoreRoute("icons/{*path}");
			routes.IgnoreRoute("content/{*path}");
			routes.IgnoreRoute("scripts/{*path}");
			routes.IgnoreRoute("favicon.ico");
			//routes.IgnoreRoute("robots.txt");
		}

		/// <summary>
		/// Maps the default MVC route to the <see cref="RouteCollection"/>. Typically called last.
		/// </summary>
		public static Route MapDefaultRoute(this RouteCollection routes)
		{
			return routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			);
		}


		/// <summary>
		/// Maps the home page route to the <see cref="RouteCollection"/>. Call close to the front of registrations after ignore routes, MVC attribute routing and Kentic routing, etc.
		/// </summary>
		public static void MapHomeRoute(this RouteCollection routes)
		{
			routes.MapRoute
			(
				name: "Home",
				url: "",
				defaults: new { controller = "Home", action = "Index" }
			);

			routes.MapRoute
			(
				name: "AliasHome",
				url: "home",
				defaults: new { controller = "Home", action = "Index" }
			);
		}

		/// <summary>
		/// Maps the admin route to the <see cref="RouteCollection"/>. The default action will send the user to the CMS Edit Url for this relativePath
		/// </summary>
		public static void MapAdminRoute(this RouteCollection routes)
		{
			routes.MapRoute
			(
				name: "GetDirectPath",
				url: "Admin/GetDirectPath/{*guidString}",
				defaults: new { controller = "Admin", action = "GetDirectPath" }
			);

			routes.MapRoute
			(
				name: "Admin",
				url: "Admin/{*relativePath}",
				defaults: new { controller = "Admin", action = "Edit" }
			);
		}


		/// <summary>
		/// Forces all request through <see cref="KenticoCultureRouteConstraint"/> to ensure that the correct locale is set
		/// </summary>
		public static void MapLocalizationRoutes(this RouteCollection routes)
		{
			routes.MapRoute
			(
				name: "Localization without Culture",
				url: "{*culture}",
				defaults: new { controller = "", action = "" },
				constraints: new { culture = new KenticoCultureRouteConstraint() }
			);

			routes.MapRoute
			(
				name: "Localization with Culture",
				url: "{culture}/{*nodeAliasPath}",
				defaults: new { controller = "", action = "" },
				constraints: new { culture = new KenticoCultureRouteConstraint() }
			);
		}

		/// <summary>
		/// Maps the Kentico dynamic page controller to the <see cref="RouteCollection"/> and matches against nodes in the Kentico tree. Call after static routes are registered, prior to the default route.
		/// </summary>
		public static Route MapKenticoNodeRoutes(this RouteCollection routes)
		{
			return routes.MapRoute
			(
				name: "KenticoNodeRouting",
				url: "{*nodeAliasPath}",
				defaults: new { controller = "KenticoPage", action = "GetByNodeAlias" },
				constraints: new { nodeAliasPath = new KenticoNodeRouteConstraint() }
			);
		}


		/// <summary>
		/// Maps the redirect controller to the <see cref="RouteCollection"/> and checks if there is a redirect for the given route
		/// </summary>
		public static Route MapRedirectRoutes(this RouteCollection routes)
		{
			return routes.MapRoute
			(
				name: "RedirectRouting",
				url: "{*nodeAliasPath}",
				defaults: new { controller = "Redirect", action = "Redirect", redirectType = UrlParameter.Optional },
				constraints: new { nodeAliasPath = new RedirectRouteConstraint() }
			);
		}

		/// <summary>
		/// Maps the home page route to the <see cref="RouteCollection"/>. Call close to the front of registrations after ignore routes, MVC attribute routing and Kentic routing, etc.
		/// </summary>
		public static void MapErrorRoutes(this RouteCollection routes)
		{
			routes.MapRoute
			(
				name: "ServerError",
				url: "server-error",
				defaults: new { controller = "Error", action = "ServerError" }
			);

			routes.MapRoute
			(
				name: "TestServerError",
				url: "test-server-error",
				defaults: new { controller = "Error", action = "TestServerError" }
			);

			routes.MapRoute
			(
				name: "NotFound",
				url: "Page-Not-Found",
				defaults: new { controller = "Error", action = "NotFound" }
			);
		}

		public static void MapImageOptimizationRoute(this RouteCollection routes)
		{
			routes.MapRoute
			(
				name: "ImageOptimization",
				url: "Optimize/GetMedia/{guid}/{*fileName}",
				defaults: new {controller = "ImageOptimization", action = "OptimizeImage" },
				constraints: new { guid = new ImageOptimizationRouteConstraint() }
			);
		}

		public static void MapDefaultVideoPlayerRoute(this RouteCollection routes)
		{
			routes.MapRoute(
				name: "DefaultVideoPlayer",
				url: "GetMedia/{guid}/{*fileName}",
				defaults: new { controller = "DefaultVideoPlayer", action = "PlayVideo" },
				constraints: new { guid = new DefaultVideoPlayerRouteConstraint() }
			);
		}


		public static Route MapRobots( this RouteCollection routes )
		{
			return routes.MapRoute
			(
				name: "Robots",
				url: "robots.txt",
				defaults: new { controller = "Robots", action = "Index" }
			);
		}

	}

}
