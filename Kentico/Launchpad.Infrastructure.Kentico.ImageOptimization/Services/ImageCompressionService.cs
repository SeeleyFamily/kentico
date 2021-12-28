using CMS.Core;
using CMS.IO;
using CMS.MediaLibrary;
using Launchpad.Infrastructure.Kentico.ImageOptimization.Abstractions;
using Launchpad.Infrastructure.Kentico.ImageOptimization.Extensions;
using System;
using System.Linq;

namespace Launchpad.Infrastructure.Kentico.ImageOptimization.Services
{
	public abstract class ImageCompressionService : IImageCompressionService
	{
		#region Properties
		public string CompressionServicePrefix = "";
		#endregion;

		#region Fields
		private readonly string fileCustomDataPrefix = "compressed_by_";
		private readonly string fileCustomDataColumn = "CompressedBy";
		#endregion

		protected virtual string GetCompressionLabel()
		{
			return fileCustomDataPrefix + CompressionServicePrefix;
		}

		public virtual void CompressImage(MediaFileInfo mediaFileInfo)
		{
			if (!mediaFileInfo.FileMimeType.ToLower().Contains("image"))
			{
				return;
			}

			var compressionLabel = GetCompressionLabel();

			var currentFileCustomDataValue = string.Empty;
			bool currentFileIsCompressed = false;
			if (mediaFileInfo.FileCustomData.TryGetValue(fileCustomDataColumn, out var currentStringValue))
			{
				if (currentStringValue != null)
				{
					currentFileCustomDataValue = currentStringValue.ToString();
					if (currentFileCustomDataValue.Contains(compressionLabel))
					{
						currentFileIsCompressed = true;
					}
				}
			}

			if (!currentFileIsCompressed)
			{
				mediaFileInfo.FileCustomData.SetValue(fileCustomDataColumn, compressionLabel);
			}

			// attempt the compression
			try
			{
				// Its a new upload
				// we should attempt the compression on new uploads
				if (mediaFileInfo.FileBinary != null && mediaFileInfo.FileBinary.Any())
				{
					using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
					{
						try
						{
							stream.Write(mediaFileInfo.FileBinary, 0, mediaFileInfo.FileBinary.Length);
							CompressImage(stream);
							stream.Capacity = (int)stream.Length;

							mediaFileInfo.FileBinary = stream.GetBuffer();
							mediaFileInfo.FileSize = stream.Capacity;
							mediaFileInfo.SubmitChanges(true);
							//mediaFileInfo.Update();
						}
						catch (Exception e)
						{
							Service.Resolve<IEventLogService>().LogInformation("ImageCompressionService", "CompressImage", "Compression of new uploaded image failed for MediaFileInfo: " + mediaFileInfo.FileID + " - " + e.Message);
						}
						finally
						{
							stream.Close();
						}
					}
				}
				else
				{
					// Edit / Update an existing image...

					if (currentFileIsCompressed)
					{
						// already compressed
						// do nothing
						return;
					}

					using (FileStream stream = mediaFileInfo.GetFileStream(FileMode.OpenOrCreate))
					{
						try
						{
							CompressImage(stream);
							mediaFileInfo.FileSize = stream.Length;
							mediaFileInfo.SubmitChanges(true);
							//mediaFileInfo.Update();
						}
						catch (Exception e)
						{
							Service.Resolve<IEventLogService>().LogInformation("ImageCompressionService", "CompressImage", "Compression of updated image failed for MediaFileInfo: " + mediaFileInfo.FileID + " - " + e.Message);
						}
						finally
						{
							// Must close the stream;
							stream.Close();
						}
					}
				}
			}
			catch (Exception e)
			{
				Service.Resolve<IEventLogService>().LogInformation("ImageCompressionService", "CompressImage", "Overall compression issue for MediaFileInfo: " + mediaFileInfo.FileID + " - " + e.Message);
			}
		}

		protected abstract void CompressImage(System.IO.Stream stream);
	}
}
