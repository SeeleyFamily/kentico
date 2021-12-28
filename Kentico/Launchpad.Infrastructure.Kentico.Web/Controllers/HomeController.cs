using System;
using System.Web.Mvc;
using Kentico.Content.Web.Mvc;
using Kentico.Web.Mvc;
using Launchpad.Core.Abstractions.Models;
using Launchpad.Core.Abstractions.Providers;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Models;
using Launchpad.Infrastructure.Kentico.Web.Attributes;

namespace Launchpad.Infrastructure.Kentico.Web.Controllers
{

	public class HomeController : Controller
	{
		#region Fields
		private readonly IDocumentService documentService;
		private readonly ICurrentNodeProvider nodeProvider;
		#endregion


		public HomeController
		(
			IDocumentService documentService,
			ICurrentNodeProvider nodeProvider
		)
		{
			this.documentService = documentService;
			this.nodeProvider = nodeProvider;
		}



		[CheckRedirectsAttribute]
		public ActionResult Index( )
		{
			
			//adding /home to redirect to root
			if (HttpContext.Request.Url.AbsolutePath == "/home" && !HttpContext.Kentico().Preview().Enabled)
			{
				return RedirectPermanent(Url.Content("~/"));
			}

			// If this isn't the root of the site, go 404 (prevents /home node from duplicating home page, bad for SEO)
			//unless Preview mode
			if ( HttpContext.Request.Url.AbsolutePath != "/" && !HttpContext.Kentico().Preview().Enabled)
			{
				return HttpNotFound();
			}


			// Retrieve the home page node
			PageNode node = documentService.GetHomePage();

			if( node == null )
			{
				throw new Exception( "No home page found." );
			}


			// Set the current node to the home page node
			nodeProvider.SetCurrentNode( node );

			// Retrieve an appropriate view model
			IViewModel viewModel = DependencyResolver.Current.GetService<IViewModel>();


			return View( viewModel.ViewPath, viewModel );
		}


	}

}
