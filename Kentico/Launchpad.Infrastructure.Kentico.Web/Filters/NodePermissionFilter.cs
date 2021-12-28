using System.Web.Mvc;
using Launchpad.Core.Abstractions.Models;
using Launchpad.Core.Abstractions.Providers;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Models;


namespace Launchpad.Infrastructure.Kentico.Web.Filters
{

	/// <summary>
	/// Checks the current request's <see cref="PageNode" /> to determine if a user has permission to access it.
	/// </summary>
	public class NodePermissionFilter : ActionFilterAttribute
	{

		public override void OnActionExecuting( ActionExecutingContext filterContext )
		{
			// Retrieve the current node, if any
			ICurrentNodeProvider currentNodeProvider = DependencyResolver.Current.GetService<ICurrentNodeProvider>();
			PageNode currentNode = currentNodeProvider.GetCurrentNode();

			if( currentNode == null )
			{
				return;
			}


			// Is the user authorized?
			ICurrentUserProvider currentUserProvider = DependencyResolver.Current.GetService<ICurrentUserProvider>();
			IDocumentService documentService = DependencyResolver.Current.GetService<IDocumentService>();

			IUser user = currentUserProvider.GetCurrentUser();


			if( documentService.IsNodeAuthorizedForUser( currentNode, user ) )
			{
				// User is authorized
				return;
			}


			// Not allowed
			if( user is AnonymousUser )
			{
				// User isn't logged in, give them the chance to do so
				filterContext.Result = new HttpUnauthorizedResult();
			}
			else
			{
				// User is logged in and just doesn't have permission; treat this as a 404
				filterContext.Result = new HttpNotFoundResult();
			}
		}

	}

}
