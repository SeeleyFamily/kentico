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
	public class BannerWidgetProperties : WidgetProperties, IWidgetProperties
	{
		private readonly IMediaService mediaService;
		private readonly IDocumentService documentService;

		[EditingComponent(DropDownComponent.IDENTIFIER, Order = 0, Label = "Background Style")]
		[EditingComponentProperty(nameof(DropDownProperties.DataSource), "primary;Primary\r\nsecondary;Secondary\r\ntertiary;Tertiary")]
		[EditingComponentProperty(nameof(DropDownProperties.DefaultValue), "primary")]
		public string BackgroundStyle { get; set; }

		[EditingComponent(TextInputComponent.IDENTIFIER, Order = 12, Label = "Title")]
		public string Title { get; set; } = "Headline";

		public string Content { get; set; } = "";

		[EditingComponent(MediaFilesSelector.IDENTIFIER, Order = 8, Label = "Image (Optional)")]
		[EditingComponentProperty(nameof(MediaFilesSelectorProperties.MaxFilesLimit), 1)]
		public IList<MediaFilesSelectorItem> Image { get; set; }

		[EditingComponent(TextInputComponent.IDENTIFIER, Order = 12, Label = "Image Alt Text")]
		public string ImageAlt { get; set; }

		[EditingComponent(TextInputComponent.IDENTIFIER, Order = 13, Label = "CTA Text")]
		public string CtaText { get; set; } = "Read More";

		[EditingComponent(PageSelector.IDENTIFIER, Order = 14, Label = "CTA Link (Page Selector)")]
		public IList<PageSelectorItem> CtaPage { get; set; }

		[EditingComponent(TextInputComponent.IDENTIFIER, Order = 15, Label = "CTA Link (Manual)")]
		public string CtaPageOverride { get; set; } = "";

		public BannerWidgetProperties()
		{
			this.documentService = DependencyResolver.Current.GetService<IDocumentService>();
			this.mediaService = DependencyResolver.Current.GetService<IMediaService>();
		}

		public MediaFile GetImage()
		{
			Guid guid = Image?.FirstOrDefault()?.FileGuid ?? Guid.Empty;
			if (guid != Guid.Empty)
			{
				return new MediaFile
				{
					Url = mediaService.GetMediaUrl(guid),
					Description = ImageAlt
				};
			}
			return null;
		}

		public string CtaLink()
		{
			if (!String.IsNullOrEmpty(CtaPageOverride)) return CtaPageOverride;
			PageSelectorItem page = CtaPage?.FirstOrDefault();
			if (page != null)
			{
				PageNode node = documentService.Get(page.NodeGuid);
				return node.DocumentUrlPath.ToLower();
			}
			return "";
		}
	}

}
