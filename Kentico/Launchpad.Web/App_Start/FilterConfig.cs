using System;
using System.Configuration;
using System.Web.Mvc;
using Launchpad.Infrastructure.Kentico.Web.Filters;
using Launchpad.Infrastructure.Kentico.ImageOptimization.Filters;


namespace Launchpad.Web
{

	public class FilterConfig
	{

		public static void RegisterGlobalFilters( GlobalFilterCollection filters )
		{
			// OnActionExecuting
			if( Boolean.Parse( ConfigurationManager.AppSettings[ "security:enabled" ] ) is bool isSecured && isSecured )
			{
				filters.Add( new NodePermissionFilter() );
				//filters.Add( new AuthorizeAttribute() );
			}


			// OnResultExecuting
			filters.Add( new PageBuilderInitFilter() );

			filters.Add(new OptimizeFilter());
		}

	}

}
