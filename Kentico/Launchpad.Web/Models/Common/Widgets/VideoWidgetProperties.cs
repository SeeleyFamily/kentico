/*
 * Built with Common Launchpad 2.0.2
 */

using Kentico.Components.Web.Mvc.FormComponents;
using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Models;
using Launchpad.Web.Models.Custom.Widgets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Launchpad.Web.Models.Common.Widgets
{
	public class VideoWidgetProperties : WidgetProperties, IWidgetProperties
	{
		private readonly IMediaService mediaService;

		[EditingComponent(DropDownComponent.IDENTIFIER, Order = 0, Label = "Video Source")]
		[EditingComponentProperty(nameof(DropDownProperties.DataSource), "external;External\r\nmedialibrary;Media Library")]
		[EditingComponentProperty(nameof(DropDownProperties.DefaultValue), "external")]
		public string VideoSource { get; set; }

		[EditingComponent(TextInputComponent.IDENTIFIER, Order = 2, Label = "Video URL (External videos only)")]
		[EditingComponentProperty(nameof(TextInputProperties.ExplanationText), "Embed URL of the video")]
		public string VideoUrl { get; set; } = "";

		[EditingComponent(MediaFilesSelector.IDENTIFIER, Order = 4, Label = "Video (Media library videos only)")]
		[EditingComponentProperty(nameof(MediaFilesSelectorProperties.MaxFilesLimit), 1)]
		public IList<MediaFilesSelectorItem> Video { get; set; }

		[EditingComponent(DropDownComponent.IDENTIFIER, Order = 6, Label = "Video Play Type")]
		[EditingComponentProperty(nameof(DropDownProperties.DataSource), "inline;Inline\r\nmodal;Modal")]
		[EditingComponentProperty(nameof(DropDownProperties.DefaultValue), "inline")]
		public string VideoPlayType { get; set; } = "";

		[EditingComponent(RichTextComponent.IDENTIFIER, Order = 7, Label = "Video Transcript (Optional)")]
		public string VideoTranscript { get; set; } = "";

		[EditingComponent(MediaFilesSelector.IDENTIFIER, Order = 8, Label = "Preview Image (Optional)")]
		[EditingComponentProperty(nameof(MediaFilesSelectorProperties.MaxFilesLimit), 1)]
		public IList<MediaFilesSelectorItem> PreviewImage { get; set; }

		[EditingComponent(TextAreaComponent.IDENTIFIER, Order = 12, Label = "Video Caption (Optional)")]
		public string VideoCaption { get; set; } = "";

		[EditingComponent(CheckBoxComponent.IDENTIFIER, Order = 14, Label = "Autoplay")]
		public bool Autoplay { get; set; }

		[EditingComponent(CheckBoxComponent.IDENTIFIER, Order = 16, Label = "Loop Video")]
		public bool LoopVideo { get; set; }

		[EditingComponent(CheckBoxComponent.IDENTIFIER, Order = 18, Label = "Show Controls")]
		public bool ShowControls { get; set; }

		[EditingComponent(CheckBoxComponent.IDENTIFIER, Order = 20, Label = "Mute Video")]
		public bool MuteVideo { get; set; }

		public VideoWidgetProperties()
		{
			this.mediaService = DependencyResolver.Current.GetService<IMediaService>();
		}

		public MediaFile GetPreviewImage()
		{
			Guid guid = PreviewImage?.FirstOrDefault()?.FileGuid ?? Guid.Empty;
			if (guid != Guid.Empty)
			{
				return new MediaFile
				{
					Url = mediaService.GetMediaUrl(guid),
				};
			}
			return null;
		}

		public string GetVideoUrl()
		{
			if (IsExternalVideo())
			{
				return VideoUrl;
			}
			else
			{
				Guid guid = Video?.FirstOrDefault()?.FileGuid ?? default;
				if (guid == default)
				{
					return string.Empty;
				}

				return mediaService.GetMediaUrl(guid);
			}
		}

		public bool IsExternalVideo()
		{
			return VideoSource == "external";
		}
	}

}