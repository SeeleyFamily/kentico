using System.Web;
using System.Web.Routing;

namespace Launchpad.Infrastructure.Kentico.Web.Constraints
{
	public class DefaultVideoPlayerRouteConstraint : IRouteConstraint
	{
		public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
		{
			if (!string.IsNullOrWhiteSpace(httpContext.Request.Url.AbsolutePath))
			{
				if (httpContext.Request.Url.AbsolutePath.EndsWith(".mp4")){
					return true;
				}
			}
			return false;
		}
	}
}
