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
	public class ImageWidgetProperties : WidgetProperties, IWidgetProperties
	{
		private readonly IMediaService mediaService;

		[EditingComponent(MediaFilesSelector.IDENTIFIER)]
		[EditingComponentProperty(nameof(MediaFilesSelectorProperties.MaxFilesLimit), 1)]
		public IList<MediaFilesSelectorItem> Images { get; set; }

		[EditingComponent(MediaFilesSelector.IDENTIFIER, Order = 2, Label = "Mobile Image")]
		[EditingComponentProperty(nameof(MediaFilesSelectorProperties.MaxFilesLimit), 1)]
		[EditingComponentProperty(nameof(TextAreaProperties.ExplanationText), "Optional: Displays alternative image for mobile views only")]
		public IList<MediaFilesSelectorItem> ImagesMobile { get; set; }

		[EditingComponent(TextAreaComponent.IDENTIFIER, Order = 4, Label = "Image Alt Text")]
		[EditingComponentProperty(nameof(TextAreaProperties.ExplanationText), "Image Alt Text")]
		public string ImgAltText { get; set; } = "";

		[EditingComponent(TextAreaComponent.IDENTIFIER, Order = 8, Label = "Caption")]
		[EditingComponentProperty(nameof(TextAreaProperties.ExplanationText), "Optional: Leave blank for no caption")]
		public string Caption { get; set; } = "";

		[EditingComponent(CheckBoxComponent.IDENTIFIER, Order = 10, Label = "Is Image Modal?")]
		[EditingComponentProperty(nameof(TextAreaProperties.ExplanationText), "Optional: Clickable Image Opens Modal")]
		public bool IsImageModal { get; set; }

		public ImageWidgetProperties()
		{
			this.mediaService = DependencyResolver.Current.GetService<IMediaService>();
		}

		public MediaFile Media()
		{
			Guid guid = Images?.FirstOrDefault()?.FileGuid ?? Guid.Empty;
			if (guid != Guid.Empty)
			{
				return new MediaFile
				{
					Url = mediaService.GetMediaUrl(guid),
					Description = Caption,
				};
			}
			return null;
		}

		public MediaFile MediaMobile()
		{
			Guid guid = ImagesMobile?.FirstOrDefault()?.FileGuid ?? Guid.Empty;
			if (guid != Guid.Empty)
			{
				return new MediaFile
				{
					Url = mediaService.GetMediaUrl(guid),
					Description = Caption,
				};
			}
			return null;
		}

		public bool HasCaption()
		{
			return !String.IsNullOrEmpty(Caption);
		}
	}
}