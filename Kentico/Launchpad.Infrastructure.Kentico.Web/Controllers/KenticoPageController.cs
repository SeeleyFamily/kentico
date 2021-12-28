using System.Web.Mvc;
using Launchpad.Core.Abstractions.Models;
using Launchpad.Infrastructure.Kentico.Web.Attributes;

namespace Launchpad.Infrastructure.Kentico.Web.Controllers
{

	public class KenticoPageController : Controller
	{
		#region Fields
		private readonly IViewModel viewModel;
		#endregion


		public KenticoPageController
		(
			IViewModel viewModel
		)
		{
			this.viewModel = viewModel;
		}


		[CheckRedirectsAttribute]
		public ActionResult GetByNodeAlias( string nodeAliasPath )
		{
			if( viewModel == null )
			{
				return HttpNotFound();
			}

			if (viewModel.ShouldNotRender)
			{
				return HttpNotFound();
			}


            return View( viewModel.ViewPath, viewModel );
		}

	}

}