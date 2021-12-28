using System.Web.Mvc;
using Launchpad.Core.Abstractions.Models;
using Launchpad.Core.Enums;

namespace Launchpad.Infrastructure.Kentico.Web.Controllers
{

	public class RedirectController : Controller
	{		

		public RedirectController()
        {

        }        

        public ActionResult Redirect(string nodeAliasPath, RedirectType redirectType =  RedirectType.Permanent)
        {
            return redirectType == RedirectType.Permanent ? RedirectPermanent(nodeAliasPath) : base.Redirect(nodeAliasPath);
        }        
    }

}