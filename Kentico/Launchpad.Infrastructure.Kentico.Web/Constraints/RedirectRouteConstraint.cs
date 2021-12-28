using Launchpad.Core.Enums;
using Launchpad.Core.Models;
using Launchpad.Infrastructure.Abstractions.Services;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Launchpad.Infrastructure.Kentico.Web.Constraints
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
			Redirect redirect = null;
			string currentPath = values[parameterName].ToString();
			bool doRedirect = false;
			bool isPathAndQueryRedirect = false;

			// Do Domain Level Redirect Check First
			//var absoluteUri = httpContext.Request.Url.AbsoluteUri;
			//var absoluteUriRedirect = MatchRedirect(absoluteUri,RedirectMode.AbsoluteUri);

			//if(redirect == null)
			//{
			//	redirect = absoluteUriRedirect;
			//}


			// Then do Query String Redirect Check 
			if (!doRedirect && !string.IsNullOrWhiteSpace(httpContext.Request.Url.Query))
			{
				var pathAndQuery = httpContext.Request.Url.PathAndQuery.TrimStart('/');
				redirect = RedirectService.MatchRedirect(pathAndQuery, RedirectMode.PathAndQuery);
				if (redirect != null)
				{
					isPathAndQueryRedirect = true;
					currentPath = pathAndQuery;
					doRedirect = true;
				}
			}

			// Check Absolute Path
			// Absolute Path will have encoded url parameters
			if (!doRedirect)
			{
				var absolutePath = httpContext.Request.Url.AbsolutePath.TrimStart('/');
				redirect = RedirectService.MatchRedirect(absolutePath);
				if (redirect != null)
				{
					doRedirect = true;
				}
			}

			// Lastly do the default check
			// Default check will have unencoded url parameters
			if (!doRedirect)
			{
				redirect = RedirectService.MatchRedirect(currentPath);
				if (redirect != null)
				{
					doRedirect = true;
				}
			}

			if (doRedirect && redirect != null)
			{
				if (redirect.IsRegexMatch)
				{
					if (redirect.IsRegexReplace)
					{
						values[parameterName] = redirect.RegexRule.Replace(currentPath, redirect.RedirectURL);
					}
					else
					{
						values[parameterName] = redirect.RedirectURL;
					}
				}
				else
				{
					if (redirect.IsTemporaryRedirect)
					{
						values["redirectType"] = RedirectType.Temporary;
					}
					values[parameterName] = redirect.RedirectURL;
				}

				// Attach QS parameters if we're doing a Path and Query Redirect
				if (!isPathAndQueryRedirect)
				{
					string queryString = httpContext.Request.Url.Query;
					if (!string.IsNullOrWhiteSpace(queryString))
					{
						values[parameterName] = values[parameterName] + queryString;
					}
				}

				return true;
			}
			return false;
		}
	}
}