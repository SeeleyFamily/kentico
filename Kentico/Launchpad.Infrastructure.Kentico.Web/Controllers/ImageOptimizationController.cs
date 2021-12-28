using CMS.Helpers;
using CMS.IO;
using CMS.MediaLibrary;
using CMS.SiteProvider;
using Launchpad.Infrastructure.Kentico.ImageOptimization.Abstractions;
using Launchpad.Infrastructure.Kentico.ImageOptimization.Services;
using MimeTypes;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace Launchpad.Infrastructure.Kentico.Web.Controllers
{
	public class ImageOptimizationController : Controller
	{

		#region Fields
		private readonly IImageConversionService webPImageConversionService;
		private readonly string optimizedFormatQueryString = "format";
		#endregion

		public ImageOptimizationController(
			)
		{
			webPImageConversionService = new WebPImageConversionService();
		}

		[OutputCache(Duration = 86400, VaryByParam = "guid", Location = OutputCacheLocation.Server)]
		public ActionResult OptimizeImage(string guid, string fileName)
		{
			string dependencyCacheKey = String.Format("mediafile|{0}", guid);
			// Converts the provided key to lower case and inserts it into the cache
			CacheHelper.EnsureDummyKey(dependencyCacheKey);
			HttpContext.Response.AddCacheItemDependency(dependencyCacheKey);

			var mediaFileInfo = MediaFileInfo.Provider.Get(new Guid(guid), SiteContext.CurrentSiteID);
			// return 404 if media file asset DNE
			if (mediaFileInfo == null)
			{
				return HttpNotFound();
			}

			if (!string.IsNullOrWhiteSpace(HttpContext.Request.Url.Query))
			{
				string format = HttpUtility.ParseQueryString(HttpContext.Request.Url.Query).Get(optimizedFormatQueryString);
				if (format.Equals(webPImageConversionService.GetOptimizedImageType(), System.StringComparison.InvariantCultureIgnoreCase))
				{
					var optimizedFiledPath = webPImageConversionService.GetOptimizedImagePhysicalFilePath(mediaFileInfo);
					// Return the WebP Image from Storage
					FileInfo file = FileInfo.New(optimizedFiledPath);
					if (file.Exists)
					{
						var stream = StorageHelper.GetFileStream(optimizedFiledPath, FileMode.Open);
						return base.File(stream, MimeTypeMap.GetMimeType(file.Extension));
					}
					else
					{
						// If file DNE, generate WebP Version, then return...
						if (webPImageConversionService.CovertImage(mediaFileInfo, out var convertedFile))
						{
							var stream = StorageHelper.GetFileStream(optimizedFiledPath, FileMode.Open);
							return base.File(stream, MimeTypeMap.GetMimeType(convertedFile.Extension));
						}
					}
				}
			}

			// fallback to the original media library asset...
			// this should not hit this case if the constraint is set up correctly...
			return RedirectPermanent(MediaLibraryHelper.GetPermanentUrl(mediaFileInfo));

		}
	}
}
