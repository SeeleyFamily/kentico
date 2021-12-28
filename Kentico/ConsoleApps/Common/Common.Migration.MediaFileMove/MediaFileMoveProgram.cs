using CMS.MediaLibrary;
using CMS.SiteProvider;
using Common.Migration.Infrastructure;
using Launchpad.Core.Extensions;
using Launchpad.Core.Utilities;
using System;
using System.Collections.Generic;

namespace Common.Migration.TreeNodeMove
{
	public class MediaFileMoveProgram : BaseProgram
	{
		#region Properties
		IEnumerable<MoveMediaFile> MoveMediaFiles { get; set; }
		#endregion

		static void Main(string[] args)
		{
			var consoleApp = new MediaFileMoveProgram();
			consoleApp.Main();
		}

		public MediaFileMoveProgram() : base()
		{
		}

		public override void RunConsoleApp()
		{
			try
			{
				// Console App Business Logic Here;
				Populate();
				Move();
			}
			catch (Exception e)
			{
				Messages.Add($"Error: {e.Message}");
			}
		}

		private void Populate()
		{
			MoveMediaFiles = new List<MoveMediaFile>()
			{
				//new MoveMediaFile(){ FileId = 0, TargetLibraryId= 0, TargetFilePath = "" },
			};
		}

		private void Move()
		{
			if (!MoveMediaFiles.IsNullOrEmpty())
			{
				foreach (var moveMediaFile in MoveMediaFiles)
				{
					try
					{

						IMediaFileInfoProvider mediaFileInfoProvider = new MediaFileInfoProvider();
						var mediaFileInfo = mediaFileInfoProvider.Get(moveMediaFile.FileId);
						var siteInfo = (SiteInfo)mediaFileInfo.Site;
						var targetPath = CoalesceUtility.CoalesceWithoutWhitespace(moveMediaFile.TargetFilePath, mediaFileInfo.FilePath);
						if (moveMediaFile.TargetLibraryId > 0 && moveMediaFile.TargetLibraryId != mediaFileInfo.FileLibraryID)
						{
							// moves the physical file
							MediaFileInfoProvider.MoveMediaFile(siteInfo.SiteName, mediaFileInfo.FileLibraryID, moveMediaFile.TargetLibraryId, mediaFileInfo.FilePath, targetPath, true);
							// updates the db reference
							mediaFileInfo.FileLibraryID = moveMediaFile.TargetLibraryId;
							mediaFileInfo.FilePath = targetPath;
							mediaFileInfo.Update();
						}
						else if (!targetPath.Equals(mediaFileInfo.FilePath))
						{
							// moves the physical file
							MediaFileInfoProvider.MoveMediaFile(siteInfo.SiteName, mediaFileInfo.FileLibraryID, mediaFileInfo.FilePath, targetPath, true);
							// updates the db reference
							mediaFileInfo.FilePath = targetPath;
							mediaFileInfo.Update();
						}
					}
					catch (Exception e)
					{
						Messages.Add($"Error: {moveMediaFile.FileId} : Error Moving : {e.Message}");
					}
				}
			}
		}
	}
}
