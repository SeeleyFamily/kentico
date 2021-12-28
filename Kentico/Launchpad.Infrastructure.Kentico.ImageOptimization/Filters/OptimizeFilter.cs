using System.Web;
using System.Web.Mvc;


namespace Launchpad.Infrastructure.Kentico.ImageOptimization.Filters
{
    public class OptimizeFilter : ActionFilterAttribute
    {
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            // Only perform this filter for ViewResult action results
            if (!(filterContext.Result is ViewResult))
            {
                return;
            }


            HttpResponseBase response = filterContext.HttpContext.Response;
            response.Filter = new OptimizeFilterStream(filterContext);
        }
    }
}
