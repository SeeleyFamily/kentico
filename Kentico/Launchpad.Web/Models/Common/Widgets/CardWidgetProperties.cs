/*
 * Built with Common Launchpad 2.0.2
 */

using Kentico.Components.Web.Mvc.FormComponents;
using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Models;
using Launchpad.Web.Models.Common.FormComponents;
using Launchpad.Web.Models.Custom.Widgets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Launchpad.Web.Models.Common.Widgets

{
	public class CardWidgetProperties : WidgetProperties, IWidgetProperties
	{
		private readonly IDocumentService documentService;
		private readonly IMediaService mediaService;

		[EditingComponent(MediaFilesSelector.IDENTIFIER, Order = 1)]
		[EditingComponentProperty(nameof(MediaFilesSelectorProperties.MaxFilesLimit), 1)]
		public IList<MediaFilesSelectorItem> Images { get; set; }

		[EditingComponent(TextAreaComponent.IDENTIFIER, Order = 2, Label = "Image Alt Text")]
		[EditingComponentProperty(nameof(TextAreaProperties.ExplanationText), "Image Alt Text")]
		public string ImgAltText { get; set; } = "";

		[EditingComponent(DropDownComponent.IDENTIFIER, Order = 3, Label = "Image Position")]
		[EditingComponentProperty(nameof(DropDownProperties.DataSource), "bg-pos-center;Center \r\n bg-pos-top;Top \r\n bg-pos-bottom;Bottom \r\n bg-pos-left;Left \r\n bg-pos-right;Right")]
		[EditingComponentProperty(nameof(DropDownProperties.DefaultValue), "bg-pos-center")]
		public string ImgPosition { get; set; } = "";

		[EditingComponent(TextInputComponent.IDENTIFIER, Order = 4, Label = "Title")]
		public string Title { get; set; } = "Title";

		[EditingComponent(TextAreaComponent.IDENTIFIER, Order = 5, Label = "Description Text")]
		public string Description { get; set; } = "Description Text";

		[EditingComponent(TextInputComponent.IDENTIFIER, Order = 10, Label = "CTA Text")]
		public string CtaText { get; set; } = "Read More";

		[EditingComponent(PageSelector.IDENTIFIER, Order = 15, Label = "CTA Link (Page Selector)")]
		public IList<PageSelectorItem> CtaPage { get; set; }

		[EditingComponent(TextInputComponent.IDENTIFIER, Order = 16, Label = "CTA Link (Manual)")]
		public string CtaPageOverride { get; set; } = "";

		public CardWidgetProperties()
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

		public string ImageUrl()
		{
			MediaFile imgData = Media();
			if (imgData != null)
			{
				return imgData.Url;
			}

			return string.Empty;
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

		public override string WidgetClass()
		{
			var widgetClass = base.WidgetClass();
			widgetClass = widgetClass + " " + ImgPosition;
			return widgetClass;
		}

		// Example of filtering a widget variant
		[EditingComponent(WidgetVariantDropdownComponent.IDENTIFIER, Order = 9999, Label = "Widget Variant")]
		[EditingComponentProperty(nameof(WidgetVariantDropdownComponentProperties.WidgetName), "card")]
		public override string WidgetVariant { get; set; }
	}

}