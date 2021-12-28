using System.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using Kentico.Web.Mvc;
using Launchpad.Core.Abstractions.Models;


namespace Launchpad.Infrastructure.Kentico.Web.Filters
{

	/// <summary>
	/// Checks the current request's <see cref="ActionResult"/> to see if Kentico's PageBuilder needs to be initialized.
	/// </summary>
	public class PageBuilderInitFilter : ActionFilterAttribute
	{

		public override void OnResultExecuting( ResultExecutingContext filterContext )
		{
			// If the result is a ViewResult, and its view model is IViewModel, check for PageBuilder functionality requirements
			if( ! ( filterContext.Result is ViewResult viewResult ) )
			{
				return;
			}

			if( ! ( viewResult.Model is IViewModel viewModel ) )
			{
				return;
			}

			// Initialize PageBuilder? Setting to default to TRUE so all pages can have the "Page" tab
			if(viewModel.Node?.DocumentID is int documentID && (true ||  viewModel.UsePageBuilder ))
			{
				// not sure how to handle this warning
				filterContext.HttpContext.Kentico().PageBuilder().Initialize( documentID );
			}

		}

	}

}
