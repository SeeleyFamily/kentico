/*
 * Built with Common Launchpad 2.0.2
 */

using Kentico.Components.Web.Mvc.FormComponents;
using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Web.Models.Custom.Widgets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Launchpad.Web.Models.Common.Widgets
{
	public class AudioPlayerWidgetProperties : WidgetProperties, IWidgetProperties
	{
		private readonly IMediaService mediaService;

		[EditingComponent(MediaFilesSelector.IDENTIFIER, Order = 1, Label = "Media Library Asset")]
		[EditingComponentProperty(nameof(MediaFilesSelectorProperties.MaxFilesLimit), 1)]
		public IList<MediaFilesSelectorItem> AssetFile { get; set; }

		[EditingComponent(TextInputComponent.IDENTIFIER, Order = 3, Label = "External Asset URL")]
		[EditingComponentProperty(nameof(TextInputProperties.ExplanationText), "Will be used if a media file is not selected above")]
		public string AssetUrl { get; set; } = "";

		[EditingComponent(TextInputComponent.IDENTIFIER, Order = 6, Label = "Title")]
		public string AssetTitle { get; set; } = "";

		[EditingComponent(TextInputComponent.IDENTIFIER, Order = 9, Label = "Subtitle")]
		public string AssetSubtitle { get; set; } = "";

		public AudioPlayerWidgetProperties()
		{
			this.mediaService = DependencyResolver.Current.GetService<IMediaService>();
		}

		public string MediaUrl()
		{
			Guid guid = AssetFile?.FirstOrDefault()?.FileGuid ?? Guid.Empty;
			if (guid != Guid.Empty)
			{
				return mediaService.GetMediaUrl(guid);
			}
			if (!String.IsNullOrEmpty(AssetUrl)) return AssetUrl;
			return null;
		}
	}

}