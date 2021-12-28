using System.Web;
using System.Web.Routing;

namespace Launchpad.Infrastructure.Kentico.Web.Constraints
{
	public class ImageOptimizationRouteConstraint : IRouteConstraint
	{
		public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
		{
			if (!string.IsNullOrWhiteSpace(httpContext.Request.Url.Query))
			{				
				string format = HttpUtility.ParseQueryString(httpContext.Request.Url.Query).Get("format");
                if (!string.IsNullOrWhiteSpace(format) && format.Equals("webp", System.StringComparison.InvariantCultureIgnoreCase))
                {
					return true;
				}
			}
			return false;
		}
	}
}
