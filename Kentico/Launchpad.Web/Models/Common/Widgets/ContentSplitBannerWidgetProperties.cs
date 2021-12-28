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
	public class ContentSplitBannerWidgetProperties : WidgetProperties, IWidgetProperties
	{
		private readonly IMediaService mediaService;

		[EditingComponent(DropDownComponent.IDENTIFIER, Order = 0, Label = "Background Style")]
		[EditingComponentProperty(nameof(DropDownProperties.DataSource), "primary;Primary\r\nsecondary;Secondary\r\ntertiary;Tertiary")]
		[EditingComponentProperty(nameof(DropDownProperties.DefaultValue), "primary")]
		public string BackgroundStyle { get; set; }

		[EditingComponent(DropDownComponent.IDENTIFIER, Order = 4, Label = "Content Background Style")]
		[EditingComponentProperty(nameof(DropDownProperties.DataSource), ";None\r\nprimary;Primary\r\nsecondary;Secondary\r\ntertiary;Tertiary")]
		[EditingComponentProperty(nameof(DropDownProperties.DefaultValue), "")]
		public string ContentBackgroundStyle { get; set; }


		[EditingComponent(DropDownComponent.IDENTIFIER, Order = 8, Label = "Content Position")]
		[EditingComponentProperty(nameof(DropDownProperties.DataSource), "right;Right\r\nleft;Left")]
		[EditingComponentProperty(nameof(DropDownProperties.DefaultValue), "right")]
		public string ContentPosition { get; set; }

		[EditingComponent(DropDownComponent.IDENTIFIER, Order = 8, Label = "Content Size")]
		[EditingComponentProperty(nameof(DropDownProperties.DataSource), "1;One Third\r\n2;One Half\r\n3;Two Thirds")]
		[EditingComponentProperty(nameof(DropDownProperties.DefaultValue), "2")]
		public string ContentSize { get; set; } = "2";

		[EditingComponent(TextInputComponent.IDENTIFIER, Order = 12, Label = "Title")]
		public string Title { get; set; } = "Headline";

		public string Content { get; set; } = "";

		[EditingComponent(MediaFilesSelector.IDENTIFIER, Order = 8, Label = "Background Image (Optional)")]
		[EditingComponentProperty(nameof(MediaFilesSelectorProperties.MaxFilesLimit), 1)]
		public IList<MediaFilesSelectorItem> BackgroundImage { get; set; }

		public ContentSplitBannerWidgetProperties()
		{
			this.mediaService = DependencyResolver.Current.GetService<IMediaService>();
		}

		public MediaFile GetBackgroundImage()
		{
			Guid guid = BackgroundImage?.FirstOrDefault()?.FileGuid ?? Guid.Empty;
			if (guid != Guid.Empty)
			{
				return new MediaFile
				{
					Url = mediaService.GetMediaUrl(guid)
				};
			}
			return null;
		}
	}

}