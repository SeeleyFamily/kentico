/*
 * Built with Common Launchpad 2.0.2
 */

using Kentico.Components.Web.Mvc.FormComponents;
using Kentico.Forms.Web.Mvc;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Models;
using Launchpad.Web.Models.Custom.Sections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Launchpad.Web.Models.Common.Sections
{
	public class BackgroundImageSectionProperties : SectionProperties
	{
		private readonly IMediaService mediaService;

		[EditingComponent(MediaFilesSelector.IDENTIFIER)]
		[EditingComponentProperty(nameof(MediaFilesSelectorProperties.MaxFilesLimit), 1)]
		public IList<MediaFilesSelectorItem> BackgroundImage { get; set; }

		[EditingComponent(MediaFilesSelector.IDENTIFIER)]
		[EditingComponentProperty(nameof(MediaFilesSelectorProperties.MaxFilesLimit), 1)]
		public IList<MediaFilesSelectorItem> BackgroundImageMobile { get; set; }

		public BackgroundImageSectionProperties()
		{
			this.mediaService = DependencyResolver.Current.GetService<IMediaService>();
		}

		public MediaFile GetBackgroundImage()
		{
			Guid guid = BackgroundImage?.FirstOrDefault()?.FileGuid ?? Guid.Empty;
			if (guid != Guid.Empty)
			{
				return mediaService.GetMediaFile(guid);
			}
			return null;
		}

		public MediaFile GetBackgroundImageMobile()
		{
			Guid guid = BackgroundImageMobile?.FirstOrDefault()?.FileGuid ?? Guid.Empty;
			if (guid != Guid.Empty)
			{
				return mediaService.GetMediaFile(guid);
			}
			return null;
		}
	}
}