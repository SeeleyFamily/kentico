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
	public class FeaturedWidgetProperties : WidgetProperties, IWidgetProperties
	{
		private readonly IDocumentService documentService;
		private readonly IMediaService mediaService;

		[EditingComponent(TextInputComponent.IDENTIFIER, Order = 0, Label = "Video URL (Optional)")]
		[EditingComponentProperty(nameof(TextInputProperties.ExplanationText), "Embed URL of the video")]
		public string VideoUrl { get; set; } = "";

		[EditingComponent(MediaFilesSelector.IDENTIFIER, Order = 1)]
		[EditingComponentProperty(nameof(MediaFilesSelectorProperties.MaxFilesLimit), 1)]
		public IList<MediaFilesSelectorItem> Images { get; set; }

		[EditingComponent(TextAreaComponent.IDENTIFIER, Order = 2, Label = "Image Alt Text")]
		[EditingComponentProperty(nameof(TextAreaProperties.ExplanationText), "Image Alt Text")]
		public string ImgAltText { get; set; } = "";

		[EditingComponent(TextInputComponent.IDENTIFIER, Order = 3, Label = "Title")]
		public string Title { get; set; } = "Title";

		[EditingComponent(TextInputComponent.IDENTIFIER, Order = 4, Label = "Subtitle")]
		public string Subtitle { get; set; } = "Subtitle";

		[EditingComponent(TextAreaComponent.IDENTIFIER, Order = 5, Label = "Description Text")]
		public string Description { get; set; } = "Description Text";

		[EditingComponent(TextInputComponent.IDENTIFIER, Order = 6, Label = "CTA Text")]
		public string CtaText { get; set; } = "Read More";

		[EditingComponent(PageSelector.IDENTIFIER, Order = 6, Label = "CTA Link (Page Selector)")]
		public IList<PageSelectorItem> CtaPage { get; set; }

		[EditingComponent(TextInputComponent.IDENTIFIER, Order = 7, Label = "CTA Link (Manual)")]
		public string CtaPageOverride { get; set; } = "";

		[EditingComponent(DropDownComponent.IDENTIFIER, Order = 8, Label = "Position")]
		[EditingComponentProperty(nameof(DropDownProperties.DataSource), "left-align;Left\r\ncenter-align;Center\r\nright-align;Right")]
		[EditingComponentProperty(nameof(DropDownProperties.DefaultValue), "left-align")]
		public string DisplayAlignment { get; set; }

		[EditingComponent(TextInputComponent.IDENTIFIER, Order = 9, Label = "CSS Class")]
		public string CssClass { get; set; } = "";

		[EditingComponent(CheckBoxComponent.IDENTIFIER, Order = 10, Label = "Show Drop-shadow")]
		[EditingComponentProperty(nameof(CheckBoxProperties.DefaultValue), false)]
		public bool ShowDropShadow { get; set; }

		public FeaturedWidgetProperties()
		{
			this.documentService = DependencyResolver.Current.GetService<IDocumentService>();
			this.mediaService = DependencyResolver.Current.GetService<IMediaService>();
		}

		public string CtaLink()
		{
			if (!String.IsNullOrEmpty(CtaPageOverride)) return CtaPageOverride;
			PageSelectorItem page = CtaPage?.FirstOrDefault();
			if (page != null)
			{
				PageNode node = documentService.Get(page.NodeGuid);
				if (node != null)
				{
					return node.DocumentUrlPath.ToLower();
				}
			}
			return "";
		}

		public MediaFile Media()
		{
			Guid guid = Images?.FirstOrDefault()?.FileGuid ?? Guid.Empty;
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