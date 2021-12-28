using CMS.Core;
using CMS.IO;
using CMS.MediaLibrary;
using ImageMagick;
using Launchpad.Core.Attributes;
using Launchpad.Core.Enums;
using Launchpad.Core.Extensions;
using Launchpad.Infrastructure.Kentico.ImageOptimization.Abstractions;
using Launchpad.Infrastructure.Kentico.ImageOptimization.Extensions;
using System;

namespace Launchpad.Infrastructure.Kentico.ImageOptimization.Services
{
	public class WebPImageConversionService : IImageConversionService
	{
		public string GetOptimizedImageType()
		{
			return OptimizedImageType.WebP.GetAttribute<CodeDisplayNameTypeAttribute>().CodeName.ToLower();
		}

		public byte[] CovertImage(FileStream stream)
		{

			using (var image = new MagickImage(stream))
			{
				image.Format = MagickFormat.WebP;
				return image.ToByteArray();
			}

		}

		public bool CovertImage(MediaFileInfo mediaFileInfo, out FileInfo file)
		{
			file = null;
			try
			{
				FileStream stream = mediaFileInfo.GetFileStream();
				byte[] imageByteArray = CovertImage(stream);
				var optimizedImagePhysicalPath = GetOptimizedImagePhysicalFilePath(mediaFileInfo);

				file = FileInfo.New(optimizedImagePhysicalPath);

				using (FileStream newFileStream = StorageHelper.GetFileStream(optimizedImagePhysicalPath, FileMode.CreateNew))
				{
					newFileStream.Write(imageByteArray, 0, imageByteArray.Length);
					newFileStream.Close();
				}
				return true;

			}
			catch (Exception e)
			{
				Service.Resolve<IEventLogService>().LogInformation("WebPImageConversionService", "CovertImage", "Overall conversion issue for MediaFileInfo: " + mediaFileInfo.FileID + " - " + e.Message);
			}
			return false;

		}

		public string GetOptimizedImagePhysicalFilePath(MediaFileInfo mediaFileInfo)
		{
			return mediaFileInfo.GetOptimizedImagePhysicalFilePath(GetOptimizedImageType());
		}
	}
}
