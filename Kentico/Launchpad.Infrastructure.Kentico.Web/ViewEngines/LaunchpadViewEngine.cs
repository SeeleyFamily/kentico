using System.Web.Mvc;


namespace Launchpad.Infrastructure.Kentico.Web.ViewEngines
{

	public class LaunchpadViewEngine : RazorViewEngine
	{
		
		public LaunchpadViewEngine( )
		{
			ViewLocationFormats = new[]
			{
				"~/Views/{1}/{0}.cshtml",
				"~/Views/{0}.cshtml",
				"~/Views/Templates/{1}/{0}.cshtml",
				"~/Views/Templates/{0}.cshtml",
				"~/Views/Common/Templates/{1}/{0}.cshtml",
				"~/Views/Common/Templates/{0}.cshtml"
			};

			PartialViewLocationFormats = new[]
			{
				"~/Views/Shared/{1}/{0}.cshtml",
				"~/Views/Shared/{0}.cshtml",
				"~/Views/Common/Shared/{1}/{0}.cshtml",
				"~/Views/Common/Shared/{0}.cshtml"
			};

			MasterLocationFormats = new[]
			{
				"~/Views/Shared/{0}.cshtml",
				"~/Views/Common/Shared/{0}.cshtml"
			};
		}

	}

}
