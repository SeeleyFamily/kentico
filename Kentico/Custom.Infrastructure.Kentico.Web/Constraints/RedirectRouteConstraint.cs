using Launchpad.Core.Enums;
using Launchpad.Core.Models;
using Launchpad.Infrastructure.Abstractions.Services;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Custom.Infrastructure.Kentico.Web.Constraints
{

	public class RedirectRouteConstraint : IRouteConstraint
	{
		private IRedirectService RedirectService { get; set; }

		public RedirectRouteConstraint()
		{
			RedirectService = DependencyResolver.Current.GetService<IRedirectService>();
		}

		public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
		{
			//To add new match rules.
			return true;
		}
	}
}