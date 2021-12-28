using Kentico.Forms.Web.Mvc;
using Launchpad.Web.Controllers.FormSections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

[assembly: RegisterFormSection("Launchpad.Web.Controllers.FormSections.TwoColumns", typeof(TwoColumnFormSectionController), "Two columns", Description = "Organizes fields into two equal-width columns side-by-side.", IconClass = "icon-l-cols-2")]
namespace Launchpad.Web.Controllers.FormSections
{
    public class TwoColumnFormSectionController : Controller
    {
        // Action used to retrieve the section markup
        public ActionResult Index()
        {
            return PartialView("FormSections/_TwoColumnFormSection");
        }
    }
}