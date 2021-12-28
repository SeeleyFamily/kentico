/*
 * Built with Common Launchpad 2.0.2
 */

using Kentico.Components.Web.Mvc.FormComponents;
using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Enums;
using Launchpad.Core.Models;
using Launchpad.Web.Models.Custom.Widgets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Launchpad.Web.Models.Common.Widgets
{
	public class HeroWidgetProperties : WidgetProperties, IWidgetProperties
	{
		private readonly IMediaService mediaService;
		private readonly IMenuService menuService;

		[EditingComponent(DropDownComponent.IDENTIFIER, Order = 0, Label = "Hero Style")]
		[EditingComponentProperty(nameof(DropDownProperties.DataSource), "primary;Primary\r\nsecondary;Secondary\r\ntertiary;Tertiary\r\nquaternary;Quaternary\r\nframed;Custom Image Framed (Upload Background Image)")]
		[EditingComponentProperty(nameof(DropDownProperties.DefaultValue), "primary")]
		public string HeroStyle { get; set; }

		[EditingComponent(MediaFilesSelector.IDENTIFIER, Order = 8, Label = "Background Image (Optional)")]
		[EditingComponentProperty(nameof(MediaFilesSelectorProperties.MaxFilesLimit), 1)]
		public IList<MediaFilesSelectorItem> BackgroundImage { get; set; }

		[EditingComponent(MediaFilesSelector.IDENTIFIER, Order = 9, Label = "Background Image Mobile (Optional)")]
		[EditingComponentProperty(nameof(MediaFilesSelectorProperties.MaxFilesLimit), 1)]
		public IList<MediaFilesSelectorItem> BackgroundImageMobile { get; set; }

		[EditingComponent(TextInputComponent.IDENTIFIER, Order = 0, Label = "Headline")]
		public string Headline { get; set; } = "Headline";

		public string Description { get; set; } = "<p>Description Text</p>";

		public HeroWidgetProperties()
		{
			this.mediaService = DependencyResolver.Current.GetService<IMediaService>();
			this.menuService = DependencyResolver.Current.GetService<IMenuService>();
		}

		public BackgroundType GetBackgroundType()
		{
			BackgroundType backgroundType = BackgroundType.Primary;
			switch (HeroStyle?.ToLower())
			{
				case "primary":
					backgroundType = BackgroundType.Primary;
					break;
				case "secondary":
					backgroundType = BackgroundType.Secondary;
					break;
				case "tertiary":
					backgroundType = BackgroundType.Tertiary;
					break;
				case "quaternary":
					backgroundType = BackgroundType.Quaternary;
					break;
				case "framed":
					backgroundType = BackgroundType.Framed;
					break;
				default: // fallback to primary
					backgroundType = BackgroundType.Primary;
					break;
			}
			return backgroundType;
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

		public MediaFile GetBackgroundImageMobile()
		{
			Guid guid = BackgroundImageMobile?.FirstOrDefault()?.FileGuid ?? Guid.Empty;
			if (guid != Guid.Empty)
			{
				return new MediaFile
				{
					Url = mediaService.GetMediaUrl(guid)
				};
			}
			return null;
		}

		public Breadcrumbs GetBreadcrumbs(int nodeID)
		{
			return menuService.GetBreadcrumbs(nodeID);
		}
	}
}