using System.Web;
using System.Web.Http;


namespace Launchpad.Api
{

	public class WebApiApplication : HttpApplication
    {

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

    }

}
