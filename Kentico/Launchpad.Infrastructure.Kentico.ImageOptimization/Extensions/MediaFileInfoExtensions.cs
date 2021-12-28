using CMS.IO;
using CMS.MediaLibrary;
using CMS.SiteProvider;

namespace Launchpad.Infrastructure.Kentico.ImageOptimization.Extensions
{
	public static class MediaFileInfoExtensions
	{
		public static string GetPhysicalFilePath(this MediaFileInfo mediaFileInfo, string fileExtension = "")
		{
			MediaLibraryInfo library = MediaLibraryInfo.Provider.Get(mediaFileInfo.FileLibraryID);
			if (library != null)
			{
				var mediaRootFolderPath = MediaLibraryHelper.GetMediaRootFolderPath(SiteContext.CurrentSiteName);
				var migrationPath = mediaRootFolderPath + library.LibraryFolder;

				var filePath = mediaFileInfo.FilePath.Trim('/');
				if (!string.IsNullOrEmpty(fileExtension))
				{
					filePath = filePath.Replace(mediaFileInfo.FileExtension, "." + fileExtension);
				}

				var physicalFilePath = MediaLibraryHelper.EnsurePhysicalPath(migrationPath + (!string.IsNullOrWhiteSpace(fileExtension) ? "\\" + MediaLibraryHelper.GetMediaFileHiddenFolder(SiteContext.CurrentSiteName) + "\\" + fileExtension : "") + "\\" + filePath);
				return physicalFilePath;
			}

			return "";
		}

		public static string GetOptimizedImagePhysicalFilePath(this MediaFileInfo mediaFileInfo, string optimizedImageFileTypePrefix)
		{
			return mediaFileInfo.GetPhysicalFilePath(optimizedImageFileTypePrefix);
		}

		public static FileStream GetFileStream(this MediaFileInfo mediaFileInfo, FileMode fileMode = FileMode.OpenOrCreate)
		{
			return StorageHelper.GetFileStream(mediaFileInfo.GetPhysicalFilePath(), fileMode);
		}
	}
}
