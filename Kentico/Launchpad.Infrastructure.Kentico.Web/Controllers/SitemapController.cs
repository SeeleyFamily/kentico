using System.Web.Mvc;
using System.Web.UI;
using System.Xml.Linq;
using CMS.DocumentEngine;
using Launchpad.Core.Abstractions.Services;


namespace Launchpad.Infrastructure.Kentico.Web.Controllers
{

	public class SitemapController : Controller
	{
		#region Fields
		private readonly ISitemapService sitemapService;
		#endregion


		public SitemapController
		(
			ISitemapService sitemapService
		)
		{
			this.sitemapService = sitemapService;
		}


		[Route( "sitemap.xml" )]
		[OutputCache( Duration = 86400, VaryByParam = "none", Location = OutputCacheLocation.Server )]
		public virtual ActionResult Xml( )
		{
			XDocument xml = sitemapService.GetSitemap();
			AddStaticRoutes( xml );

			return Content( xml.ToString(), "text/xml" );
		}



		/// <summary>
		/// Can be overridden in subclasses to add static routes without <see cref="TreeNode"/> representations.
		/// </summary>
		protected virtual void AddStaticRoutes( XDocument xml )
		{
		
		}
	
	}

}
