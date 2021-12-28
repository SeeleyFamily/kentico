using CMS.IO;
using CMS.MediaLibrary;
using CMS.SiteProvider;
using Launchpad.Infrastructure.Kentico.ImageOptimization.Extensions;
using System;
using System.Web.Mvc;

namespace Launchpad.Infrastructure.Kentico.Web.Controllers
{
	public class DefaultVideoPlayerController : Controller
	{
		public DefaultVideoPlayerController(
			)
		{
		}

		public ActionResult PlayVideo(string guid, string fileName)
		{
			var mediaFileInfo = MediaFileInfo.Provider.Get(new Guid(guid), SiteContext.CurrentSiteID);
			if (mediaFileInfo != null)
			{
				FileStream stream = mediaFileInfo.GetFileStream();
				return base.File(stream, mediaFileInfo.FileMimeType);
			}

			// return 404 if media file asset DNE
			return HttpNotFound();
		}
	}
}
