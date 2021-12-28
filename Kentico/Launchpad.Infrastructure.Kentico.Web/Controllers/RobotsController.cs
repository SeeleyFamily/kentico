using System.Web.Mvc;
using Launchpad.Core.Abstractions.Services;


namespace Launchpad.Infrastructure.Kentico.Web.Controllers
{

	public class RobotsController : Controller
	{
		#region Fields
		private readonly IDocumentService documentService;
		#endregion


		public RobotsController
		(
			IDocumentService documentService
		)
		{
			this.documentService = documentService;
		}


		public ActionResult Index( )
		{
			// Retrieve the robots file
			string file = documentService.GetRobotsFile();

			return new ContentResult
			{
				Content = file,
				ContentType = "text/plain; charset=utf-8"
			};
		}
	}

}