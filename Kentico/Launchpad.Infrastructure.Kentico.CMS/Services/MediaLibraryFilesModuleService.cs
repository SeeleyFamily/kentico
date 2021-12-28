using CMS.Core;
using CMS.DataEngine;
using CMS.DocumentEngine;
using CMS.Helpers;
using CMS.MediaLibrary;
using CMS.SiteProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Launchpad.Infrastructure.Kentico.CMS.Services
{
	public class MediaLibraryFilesModuleService
	{
		private List<string> ErrorMessages { get; set; } = new List<string>();

		public MediaLibraryFilesModuleService()
		{

		}

		public void BulkImportMediaLibraryFiles()
		{
			//string mediaLibraryFileBulkImportLibrary = SettingsKeyInfoProvider.GetValue("MediaLibraryFileBulkImportLibrary");
			//if (string.IsNullOrWhiteSpace(mediaLibraryFileBulkImportLibrary))
			//{
			//	mediaLibraryFileBulkImportLibrary = "MigratedMediaLibrary";
			//}

			//// Gets the media library
			//MediaLibraryInfo library = MediaLibraryInfoProvider.GetMediaLibraryInfo(mediaLibraryFileBulkImportLibrary, SiteContext.CurrentSiteName);
			//if (library != null)
			//{
			//	var mediaRootFolderPath = MediaLibraryHelper.GetMediaRootFolderPath(SiteContext.CurrentSiteName);
			//	var migrationPath = mediaRootFolderPath + library.LibraryFolder;

			//	var mediaLibraryFiles = MediaLibraryFilesInfoProvider.GetMediaLibraryFiles();
			//	foreach (var mediaLibraryFile in mediaLibraryFiles.ToList().OrderBy(x => x.FileFullPath.Length).ThenBy(x => x.FilePath.Length).ThenBy(x => x.FileName.Length))
			//	{
			//		var filePath = mediaLibraryFile.FilePath.Trim('/') + '\\' + mediaLibraryFile.FileName.Trim('/');
			//		if (!string.IsNullOrWhiteSpace(mediaLibraryFile.FileFullPath))
			//		{
			//			filePath = mediaLibraryFile.FileFullPath.Trim('/');
			//		}

			//		var physicalFilePath = MediaLibraryHelper.EnsurePhysicalPath(migrationPath + "\\" + filePath);
			//		FileInfo file = FileInfo.New(physicalFilePath);
			//		if (file.Exists)
			//		{
			//			try
			//			{
			//				var mediaFileInfo = new MediaFileInfo()
			//				{
			//					FileName = mediaLibraryFile.FileName,
			//					FileTitle = mediaLibraryFile.FileTitle,
			//					FileDescription = mediaLibraryFile.FileDescription,
			//					FilePath = !string.IsNullOrWhiteSpace(mediaLibraryFile.FileFullPath)? mediaLibraryFile.FileFullPath.Trim('/') : mediaLibraryFile.FilePath.Trim('/') + '/' + mediaLibraryFile.FileName.Trim('/'), // Sets the path within the media library's folder structure
			//					FileExtension = file.Extension,
			//					FileMimeType = MimeTypeHelper.GetMimetype(file.Extension),
			//					FileSiteID = SiteContext.CurrentSiteID,
			//					FileLibraryID = library.LibraryID,
			//					FileSize = file.Length,
			//				};
			//				// Optionally
			//				if (mediaLibraryFile.FileCreatedWhen != new DateTime() && mediaLibraryFile.FileCreatedWhen != null)
			//				{
			//					mediaFileInfo.FileCreatedWhen = mediaLibraryFile.FileCreatedWhen;
			//					mediaFileInfo.FileModifiedWhen = mediaLibraryFile.FileCreatedWhen;
			//				}

			//				// Custom Data is Read Only

			//				//if (!string.IsNullOrWhiteSpace(mediaLibraryFile.FileCustomData))
			//				//{
			//				//	mediaFileInfo.FileCustomData = mediaLibraryFile.FileCustomData;
			//				//}

			//				MediaFileInfoProvider.SetMediaFileInfo(mediaFileInfo);

			//				// If the import was successful, delete the migration entry
			//				mediaLibraryFile.Delete();
			//			}
			//			catch (Exception e)
			//			{
			//				ErrorMessages.Add($"MediaLibraryFilesID: {mediaLibraryFile.MediaLibraryFilesID}, Error: {e.Message}");

			//				// Error code for Media File Already Exists
			//				if (e.HResult == -2146233088)
			//				{
			//					mediaLibraryFile.Delete();
			//				}
			//			};
			//		}
			//		else
			//		{
			//			// File Does Not Exists
			//			ErrorMessages.Add($"MediaLibraryFilesID: {mediaLibraryFile.MediaLibraryFilesID}, Error: Physical File Does Not Exist");
			//		}
			//	}
			//}

			//if (ErrorMessages != null && ErrorMessages.Any())
			//{
			//	EventLogProvider.LogInformation("BulkImportMediaLibraryFilesScheduledTask", "ErrorMessages", ErrorMessages.Join("\n"));
			//	throw new Exception($"There were {ErrorMessages.Count()} errors. See the event log for more details.");
			//}
		}

		public void ReplaceWithPermanentUrlReferences()
		{
			string mediaLibraryFilePrefix = SettingsKeyInfoProvider.GetValue("MediaLibraryFilePrefix");
			string mediaLibraryFileStringReplaceSearchValue = SettingsKeyInfoProvider.GetValue("MediaLibraryFileStringReplaceSearchValue");
			string mediaLibraryFileStringReplaceNewValue = SettingsKeyInfoProvider.GetValue("MediaLibraryFileStringReplaceNewValue");
			string mediaLibraryFileReferenceDomains = SettingsKeyInfoProvider.GetValue("MediaLibraryFileReferenceDomains");
			string mediaLibraryFileNodeClassNames = SettingsKeyInfoProvider.GetValue("MediaLibraryFileNodeClassNames");
			if (string.IsNullOrWhiteSpace(mediaLibraryFileNodeClassNames))
			{
				throw new Exception("Please enter a class name in the Content Migration Settings for MediaLibraryFileNodeClassNames.");
			}
			var classNames = mediaLibraryFileNodeClassNames.Split(',').Join("','");
			var allMediaFiles = MediaFileInfo.Provider.Get().OnSite(SiteContext.CurrentSiteID, true).ToList();
			var allContentTreeNodes = DocumentHelper.GetDocuments().OnCurrentSite().WithCoupledColumns().Where($"ClassName in ('{classNames}')").ToList();
			var allSiteDomains = mediaLibraryFileReferenceDomains.Split(',').ToList();

			var totalCount = allContentTreeNodes.Count();
			var currentCount = 0;
			var updatedCount = 0;

			foreach (var node in allContentTreeNodes)
			{
				currentCount++;
				var defaultFields = TreeNode.New().Properties.Union(new string[] { "Fields" }).ToList();
				var contentFields = node.Properties.Except(defaultFields);
				foreach (string field in contentFields)
				{
					var fieldValue = node[field];
					if (fieldValue != null)
					{
						List<string> fieldMediaFileReferences = new List<string>();

						var fieldStringValue = fieldValue.ToString().Trim();

						// Get all src matches
						var srcMatches = Regex.Matches(fieldStringValue, "src=[\"'](.+?)[\"'].*?", RegexOptions.IgnoreCase);
						foreach (Match srcMatch in srcMatches)
						{
							var value = srcMatch.Value.ToString();
							if (value.Contains("."))
							{
								fieldMediaFileReferences.Add(value.Replace("src=\"", "").Trim('"'));
							}
						}
						var hrefMatches = Regex.Matches(fieldStringValue, "href=[\"'](.+?)[\"'].*?", RegexOptions.IgnoreCase);
						foreach (Match hrefMatch in hrefMatches)
						{
							var value = hrefMatch.Value.ToString();
							if (value.Contains("."))
							{
								fieldMediaFileReferences.Add(value.Replace("href=\"", "").Trim('"'));
							}
						}

						// If the field value is a relative url referencing a file with an extension
						if (fieldStringValue.Trim().StartsWith("/") && fieldStringValue.Contains("."))
						{
							fieldMediaFileReferences.Add(fieldStringValue);
						}

						// Starts With Kentico's ~/ Reference
						if (fieldStringValue.StartsWith("~/") && fieldStringValue.Contains("."))
						{
							// Verify that it isn't the permanent url
							// If it isn't, convert to permanent url
							if (!fieldStringValue.StartsWith("~/getmedia/") && !fieldStringValue.StartsWith("~/getattachment/"))
							{
								fieldMediaFileReferences.Add(fieldStringValue);
							}
						}

						// Check if the Field Value is a Url
						try
						{
							Uri uri = new Uri(fieldStringValue);
							if (allSiteDomains.Contains(uri.Host.Trim('/')))
							{
								fieldMediaFileReferences.Add(fieldStringValue);
							}
						}
						catch (Exception)
						{
							// Reference is not a valid Url
						}

						if (fieldMediaFileReferences.Any())
						{
							bool updateNode = false;
							foreach (var filePathReference in fieldMediaFileReferences)
							{
								// DO REGEX MATCH EXTRACTION
								var d = Regex.Match(filePathReference, @"\/media\/(.+)\.ashx");
								if (d.Success)
								{
									var guid = d.Value.Replace("/media/", "").Replace(".ashx", "");
								}


								// Media File Path Reference is not empty
								// it must have a . to be a referenced filePath
								if (!string.IsNullOrWhiteSpace(filePathReference)
									&& filePathReference.Contains("."))
								{

									var searchReferencePath = filePathReference.Replace("~/", "").Trim('/').ToLower();

									// Check if the filePathReference is a Url
									try
									{
										Uri uri = new Uri(filePathReference);
										if (!allSiteDomains.Contains(uri.Host.Trim('/')))
										{
											continue;
										}
										else
										{
											if (!uri.AbsolutePath.Contains("."))
											{
												continue;
											}
											else
											{
												searchReferencePath = uri.AbsolutePath.Trim('/').ToLower();
											}
										}
									}
									catch (Exception)
									{
										// Reference is not a valid Url
									}

									// check if its already a permanent url
									if (searchReferencePath.StartsWith("getmedia/") || searchReferencePath.StartsWith("getattachment/"))
									{
										continue;
									}


									if (!string.IsNullOrWhiteSpace(searchReferencePath))
									{
										var searchMediaFilePaths = new List<string>
										{
											searchReferencePath
										};

										if (!string.IsNullOrWhiteSpace(mediaLibraryFilePrefix))
										{
											var prefixedFilePath = mediaLibraryFilePrefix.Trim('/').ToLower() + "/" + searchReferencePath;
											searchMediaFilePaths.Add(prefixedFilePath);

											if (!string.IsNullOrWhiteSpace(mediaLibraryFileStringReplaceSearchValue) && prefixedFilePath.Contains(mediaLibraryFileStringReplaceSearchValue))
											{
												searchMediaFilePaths.Add(prefixedFilePath.Replace(mediaLibraryFileStringReplaceSearchValue, mediaLibraryFileStringReplaceNewValue));
											}
										}
										if (!string.IsNullOrWhiteSpace(mediaLibraryFileStringReplaceSearchValue) && searchReferencePath.Contains(mediaLibraryFileStringReplaceSearchValue))
										{
											searchMediaFilePaths.Add(searchReferencePath.Replace(mediaLibraryFileStringReplaceSearchValue.ToLower(), mediaLibraryFileStringReplaceNewValue.ToLower()));
										}

										bool searchPathFound = false;
										foreach (var mediaFilePath in searchMediaFilePaths)
										{
											var mediaFiles = allMediaFiles.Where(x => x.FilePath.Trim('/').ToLower() == mediaFilePath.Trim('/').ToLower());
											if (mediaFiles.Any())
											{
												var permanentUrl = MediaLibraryHelper.GetPermanentUrl(mediaFiles.First());
												if (!string.IsNullOrWhiteSpace(permanentUrl))
												{
													// DO THE REPLACEMENT
													try
													{
														var newfieldStringValue = fieldStringValue.Replace(filePathReference, permanentUrl);
														node[field] = newfieldStringValue;
														updateNode = true;
														searchPathFound = true;
													}
													catch (Exception e)
													{
														ErrorMessages.Add($"Issue Updating Node: {node.NodeID}, Error: {e.Message}");
													}
												}
												break;
											}
										}
										if (!searchPathFound)
										{
											ErrorMessages.Add($"Issue Updating Node: {node.NodeID}, Could Not Find Replacement for: {searchReferencePath}");
										}
									}
								}
							}
							if (updateNode)
							{
								try
								{
									node.Update();
									updatedCount++;
								}
								catch (Exception e)
								{
									ErrorMessages.Add($"Issue Updating Node: {node.NodeID}, Error: {e.Message}");

								}
							}
						}
					}
				}
			}

			if (ErrorMessages != null && ErrorMessages.Any())
			{
				ErrorMessages.Prepend($"Proccessed {currentCount}:{totalCount} - Updated {updatedCount}");
				Service.Resolve<IEventLogService>().LogInformation("PermanentUrlReferencesScheduledTask", "ErrorMessages", ErrorMessages.Join("\n"));
				throw new Exception($"There were {ErrorMessages.Count()} errors. See the event log for more details.");
			}
		}
	}
}
