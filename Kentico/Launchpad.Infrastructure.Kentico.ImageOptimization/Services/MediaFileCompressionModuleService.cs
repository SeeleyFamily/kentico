using CMS.Base;
using CMS.Core;
using CMS.DataEngine;
using CMS.Helpers;
using CMS.IO;
using CMS.MediaLibrary;
using CMS.SiteProvider;
using Launchpad.Core.Attributes;
using Launchpad.Core.Enums;
using Launchpad.Core.Extensions;
using Launchpad.Infrastructure.Kentico.ImageOptimization.Abstractions;
using Launchpad.Infrastructure.Kentico.ImageOptimization.Extensions;
using Launchpad.Infrastructure.Kentico.ImageOptimization.Services;
using Launchpad.Infrastructure.Kentico.ImageOptimization.WebFarmTasks;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Launchpad.Infrastructure.ImageMagick.Services
{
	public class MediaFileCompressionModuleService
	{
		#region Fields
		private readonly IImageCompressionService imageCompressionService;
		private readonly AzureCacheCleanerService cacheCleanerService;
		#endregion

		public MediaFileCompressionModuleService()
		{
			var tinyPngKey = ConfigurationManager.AppSettings.GetStringValue("TinyPngApiKey");
			if (string.IsNullOrWhiteSpace(tinyPngKey))
			{
				// Free			
				imageCompressionService = new ImageMagickImageCompressionService();
			}
			else
			{
				// Paid but compresses better
				imageCompressionService = new TinyPngImageCompressionService();
			}

			// Cleaner
			cacheCleanerService = new AzureCacheCleanerService();
		}

		internal void OnUpdateAfter(object sender, ObjectEventArgs e)
		{
			var mediaFileInfo = (MediaFileInfo)e.Object;
			DeleteImage(mediaFileInfo);
			CompressImage(mediaFileInfo);
		}

		internal void OnDeleteBefore(object sender, ObjectEventArgs e)
		{
			var mediaFileInfo = (MediaFileInfo)e.Object;
			DeleteImage(mediaFileInfo);
		}

		internal void OnInsertAfter(object sender, ObjectEventArgs e)
		{
			var mediaFileInfo = (MediaFileInfo)e.Object;
			CompressImage(mediaFileInfo);
		}

		private void DeleteImage(MediaFileInfo mediaFileInfo)
		{
			DeleteConvertedThumbnails(mediaFileInfo);
			cacheCleanerService.ClearCacheFile(mediaFileInfo);
			CreateCacheClearWebFarmTask(mediaFileInfo);
		}

		private void CreateCacheClearWebFarmTask(MediaFileInfo mediaFileInfo)
		{
			// TODO: Logic that requires synchronization

			// Creates the custom web farm synchronization task, saving it to the database for processing by other web farm servers
			WebFarmHelper.CreateTask(new ClearAzureCacheWebFarmTask()
			{
				CreatorName = SystemContext.ServerName,
				Data = mediaFileInfo.FileGUID
			});

			// Logs a record into the event log of the server that created the web farm task
			string message = $"Server {SystemContext.ServerName} finished processing an operation and created a task for other web farms.";
			Service.Resolve<IEventLogService>().LogInformation("CustomTask", "Create", message);
		}

		private void CompressImage(MediaFileInfo mediaFileInfo)
		{
			imageCompressionService.CompressImage(mediaFileInfo);
		}

		private void DeleteConvertedThumbnails(MediaFileInfo mediaFileInfo)
		{
			// Delete Existing Converted Thumbnail
			List<string> optimizedExtensions = new List<string>()
			{
				OptimizedImageType.WebP.GetAttribute<CodeDisplayNameTypeAttribute>().CodeName.ToLower(),
			};
			foreach (var extension in optimizedExtensions)
			{
				var optimizedImagePath = mediaFileInfo.GetOptimizedImagePhysicalFilePath(extension);
				FileInfo file = FileInfo.New(optimizedImagePath);

				if (file.Exists)
				{
					file.Delete();
				}
			}
		}

		public void CompressMediaFiles()
		{
			List<string> ErrorMessages = new List<string>();

			var allMediaFiles = MediaFileInfo.Provider.Get().OnSite(SiteContext.CurrentSiteID, true)
				.Where(x => x.FileMimeType.ToLower().Contains("image") && !x.FileCustomData.ToString().Contains("compressed_by_"))
				.ToList();

			var totalCount = allMediaFiles.Count;
			var currentCount = 0;
			var updatedCount = 0;
			foreach (var mediaFile in allMediaFiles)
			{
				try
				{
					currentCount++;
					mediaFile.Update();
					updatedCount++;
				}
				catch (Exception e)
				{
					ErrorMessages.Add($"Issue Compressing MediaFile: {mediaFile.FileID}, Error: {e.Message}");
				}
			}

			if (ErrorMessages != null && ErrorMessages.Any())
			{
				ErrorMessages.Prepend($"Proccessed {currentCount}:{totalCount} - Updated {updatedCount}");
				throw new Exception(ErrorMessages.Join("\n"));
			}
		}
	}
}
