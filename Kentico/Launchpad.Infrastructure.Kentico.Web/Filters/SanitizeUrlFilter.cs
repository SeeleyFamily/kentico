using System.Web;
using System.Web.Mvc;

namespace Launchpad.Infrastructure.Kentico.Web.Filters
{
    public class SanitizeUrlFilter : ActionFilterAttribute
    {
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            //Ignore for now
            // Only perform this filter for ViewResult action results
            if (!(filterContext.Result is ViewResult))
            {
                return;
            }

            HttpResponseBase response = filterContext.HttpContext.Response;
            response.Filter = new SanitizeUrlFilterStream(filterContext);
        }
    }
}
