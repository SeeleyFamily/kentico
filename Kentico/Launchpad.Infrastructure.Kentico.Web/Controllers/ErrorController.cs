using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Launchpad.Core.Abstractions.Models;
using Launchpad.Core.Abstractions.Providers;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Models;


namespace Launchpad.Infrastructure.Kentico.Web.Controllers
{

	public class ErrorController : Controller
	{
		#region Fields
		private readonly IDocumentService DocumentService;
		private readonly ICurrentNodeProvider NodeProvider;
		private IViewModel ViewModel;
		#endregion


		public ErrorController
		(
			IDocumentService documentService,
			ICurrentNodeProvider nodeProvider
		)
		{
			this.DocumentService = documentService;
			this.NodeProvider = nodeProvider;
		}



		[OutputCache(Duration = 86400, VaryByParam = "none", Location = OutputCacheLocation.Server)]
		public ViewResult NotFound()
		{			
			// Retrieve the error page node
			PageNode node = this.DocumentService.Get404Page();
			// This is a compatability fallback
			// some of our first launchpad sites used an error page type for the 404.
			// implications are that the view model will be error page instead of page not found
			// ultimately this will require a custom view template referancing the correct model in the view signature
			if (node == null)
			{
				node = this.DocumentService.GetErrorPage(404);
			}
			if (node == null)
			{
				throw new HttpException((int)HttpStatusCode.InternalServerError, "The 404 Page Node was not found.");
			}


			return ReturnView(node, HttpStatusCode.NotFound);
		}


		[OutputCache(Duration = 86400, VaryByParam = "none", Location = OutputCacheLocation.Server)]
		public ViewResult ServerError()
		{
			// Retrieve the error page node
			PageNode node = this.DocumentService.GetErrorPage(500);

			if (node == null)
			{
				throw new HttpException((int)HttpStatusCode.InternalServerError, "The Error Page Node was not found.");
			}


			// Prevent IIS from using its own error page
			HttpContext.Response.TrySkipIisCustomErrors = true;


			return ReturnView(node, HttpStatusCode.InternalServerError); // The resulting page is 200, don't do 500/internal server error here because IIS is actually going to redirect away from the erroring page to this handler
		}

		public ViewResult TestServerError()
		{
			throw new System.Exception("Test Server Error");		
		}


		private ViewResult ReturnView(PageNode node, HttpStatusCode statusCode)
		{
			// Set the current node
			this.NodeProvider.SetCurrentNode(node);

			// Retrieve an appropriate view model
			this.ViewModel = DependencyResolver.Current.GetService<IViewModel>();


			Response.StatusCode = (int)statusCode;
			return View(this.ViewModel.ViewPath, this.ViewModel);
		}

	}

}
