using CMS.AzureStorage;
using CMS.Core;
using CMS.MediaLibrary;
using CMS.SiteProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using IOExceptions = System.IO;

namespace Launchpad.Infrastructure.Kentico.ImageOptimization.Services
{

	public class AzureCacheCleanerService
	{
		private IEnumerable<string> DirsToSearch
		{
			get
			{
				return new[]
					{
					PathHelper.CachePath,
					PathHelper.TempPath
				};
			}
		}

		public void ClearCacheFile(MediaFileInfo mediaFileInfo)
		{
			try
			{
				var files = DirsToSearch.SelectMany(x => new IOExceptions.DirectoryInfo(x).EnumerateFiles("*", IOExceptions.SearchOption.AllDirectories)).ToList();
				var deleteFiles = files.Where(x => x.Name.Contains(mediaFileInfo.FileName));
				foreach (var fi in deleteFiles)
				{
					if (fi != null && fi.Exists)
					{
						// Delete the file from file system
						fi.Delete();
					}
				}
			}
			catch (Exception ex)
			{
				Service.Resolve<IEventLogService>().LogException("AzureStorageCacheCleaner", "DeleteFile", ex);
			}

		}

		public void ClearCacheFile(Guid mediaFileGuid)
		{
			var mediaFileInfo = MediaFileInfo.Provider.Get(mediaFileGuid, SiteContext.CurrentSiteID);
			if (mediaFileInfo != null)
			{
				ClearCacheFile(mediaFileInfo);
			}
		}
	}
}
