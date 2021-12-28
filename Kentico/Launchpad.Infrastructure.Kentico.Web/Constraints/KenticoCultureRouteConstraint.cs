using CMS.Localization;
using CMS.SiteProvider;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Routing;


namespace Launchpad.Infrastructure.Kentico.Web.Constraints
{
	public class KenticoCultureRouteConstraint : IRouteConstraint
	{
		public virtual bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
		{
			string cultureAlias = values[parameterName]?.ToString();

			// TODO following line may need to be optimized and cached
			// checks if the culture exists
			var kenticoCulture = CultureSiteInfoProvider.GetSiteCultures(SiteContext.CurrentSiteName).Items.Where(x => x.CultureAlias == $"/{cultureAlias}").FirstOrDefault();

			//if culture is not null, this is a correct culture and it must continue with this route, otherwise, we let the next routes handle it.
			if (kenticoCulture != null)
			{
				/*
				 * Creates a CultureInfo object from the culture code and 
				 * sets the current culture for the MVC application
				 * 
				 * We're running this here due to the way we get current node. If we try to go the mvcroute way,
				 * by the time we change the culture of the thread, we would have already brought over the currentnode.
				 * 
				 * We might want to fix this later.
				 * 
				 * Ramiro - 07/29/21
				 *
				 */
				var culture = new System.Globalization.CultureInfo(kenticoCulture.CultureCode);
				Thread.CurrentThread.CurrentUICulture = culture;
				Thread.CurrentThread.CurrentCulture = culture;
				// following line is required for k12 but not k13
				LocalizationContext.CurrentCulture.CultureCode = kenticoCulture.CultureCode;
			}
			else
			{
				// Default to English
				// This may need to change if we ever do a site with default language as some other locale
				var culture = new System.Globalization.CultureInfo("en-US");
				Thread.CurrentThread.CurrentUICulture = culture;
				Thread.CurrentThread.CurrentCulture = culture;
				// following line is required for k12 but not k13
				LocalizationContext.CurrentCulture.CultureCode = "en-US";
			}

			// always return false here
			// the goal of this constraint is to set the culture
			// never to make it to the controller
			return false;
		}
	}
}
