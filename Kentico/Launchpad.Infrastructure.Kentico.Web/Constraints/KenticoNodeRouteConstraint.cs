using CMS.DocumentEngine.Types.Common;
using Kentico.Content.Web.Mvc;
using Kentico.Web.Mvc;
using Launchpad.Core.Abstractions.Providers;
using Launchpad.Core.Models;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace Launchpad.Infrastructure.Kentico.Web.Constraints
{

	public class KenticoNodeRouteConstraint : IRouteConstraint
	{

		public virtual bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
		{
			// Retrieve the current node, if any
			ICurrentNodeProvider currentNodeProvider = DependencyResolver.Current.GetService<ICurrentNodeProvider>();
			PageNode currentNode = currentNodeProvider.GetCurrentNode();

			// If there's no node based off the current route/url, this isn't a Kentico node match
			// (Never handle home page nodes from this path, i.e., /home; let the HomeController handle "/" and treat those paths as 404)
			// Also if the node.IsContentOnly flag is true, then it should 404.
			// 12/10/20 - Ramiro: Adding a hardcoded check for a "/home" to account for when we have custom home page types. 
			return currentNode != null && 
				(
					currentNode.NodeClassName != Home.CLASS_NAME 
					|| currentNode.DocumentUrlPath != "/home"
					|| httpContext.Kentico().Preview().Enabled					
				)
				&& !currentNode.IsContentOnly 
				&& currentNode.NodeClassName != Placeholder.CLASS_NAME;
		}

	}

}