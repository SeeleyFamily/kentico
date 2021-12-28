using CMS.DocumentEngine;
using CMS.DocumentEngine.Types.Common;
using CMS.Helpers;
using CMS.MediaLibrary;
using CMS.SiteProvider;
using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Models;
using Launchpad.Infrastructure.Abstractions.Services;
using Launchpad.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Launchpad.Infrastructure.Services
{
	public class MediaService : IMediaService, IPerScopeService
	{

		#region Fields
		private readonly ICacheService cacheService;
		private readonly IDocumentQueryConfiguration queryConfiguration;
		private readonly IDocumentService documentService;
		#endregion

		public MediaService(
			ICacheService cacheService,
			IDocumentQueryConfiguration queryConfiguration,
			IDocumentService documentService
			)
		{
			this.cacheService = cacheService;
			this.queryConfiguration = queryConfiguration;
			this.documentService = documentService;
		}

		public MediaFile GetMediaFile(Guid guid)
		{
			MediaFile mediaFile = cacheService.GetFromCache((cs) =>
			{
				MediaFileInfo m = MediaFileInfo.Provider.Get(guid, SiteContext.CurrentSiteID);
				if (m != null)
				{
					return new MediaFile
					{
						FileGuid = guid,
						Url = MediaLibraryHelper.GetPermanentUrl(m).Replace("~/", "/"),
						Description = m.FileDescription
					};
				}
				return null;

			}, $"mediafile|{guid}");

			return mediaFile;
		}

		public string GetMediaUrl(Guid guid)
		{
			MediaFile m = GetMediaFile(guid);
			return m?.Url;
		}
		public IEnumerable<MediaFile> GetGalleryFolderAssets(Guid guid)
		{
			var parentFolder = documentService.Get(guid);
			if (parentFolder == null) return null;

			IEnumerable<MediaFile> mediaFiles = cacheService.GetFromCache((cs) =>
			{
				IEnumerable<TreeNode> nodes = DocumentHelper.GetDocuments()
						.WhereEquals("NodeParentID", parentFolder.NodeID)
						.Type(ImageReference.CLASS_NAME)
						.Type(VideoReference.CLASS_NAME)
						.WithCoupledColumns()
						.ApplyConfiguration(queryConfiguration)
						.OrderBy($"{nameof(TreeNode.NodeOrder)},")
						.ToList();

				// establish cache dependencies on the child pages
				List<string> cacheDependencies = nodes.Select(
					x => $"nodeguid|{queryConfiguration.SiteName}|{x.NodeGUID}".ToLower()
					).ToList();
				// add cache dependency of the folder
				cacheDependencies.Add($"node|{queryConfiguration.SiteName}|{parentFolder.NodeAliasPath}|childnodes".ToLower());
				cs.CacheDependency = CacheHelper.GetCacheDependency(cacheDependencies.ToArray());

				if (nodes != null)
				{
					return nodes.Select(x => ToModel(x)).ToList();
				}
				return null;

			},
			cacheKey: $"gallery|{guid}",
			cacheDependencies: new List<string>()
				   {
					   $"node|{queryConfiguration.SiteName}|{parentFolder.NodeAliasPath}|childnodes".ToLower()
				   }
			);
			return mediaFiles;
		}

		public MediaFile ToModel(TreeNode node)
		{
			MediaFile m = null;
			switch (node)
			{
				case ImageReference customNode:
					m = new MediaFile
					{
						Type = "image",
						FileGuid = node.NodeGUID,
						Description = customNode.ImageCaption,
						Url = customNode.Image.Replace("~/get", "/get"),
						ThumbnailUrl = customNode.Image.Replace("~/get", "/get"),
					};
					break;
				case VideoReference customNode:
					m = new MediaFile
					{
						Type = "video",
						FileGuid = node.NodeGUID,
						Description = customNode.Description,
						Url = customNode.EmbedUrl,
						ThumbnailUrl = customNode.ThumbnailImage.Replace("~/get", "/get")
					};
					break;
				default:
					break;
			}
			return m;
		}
	}
}
