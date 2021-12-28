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
	public class QuoteBannerWidgetProperties : WidgetProperties, IWidgetProperties
	{
		private readonly IMediaService mediaService;

		[EditingComponent(DropDownComponent.IDENTIFIER, Order = 0, Label = "Background Style")]
		[EditingComponentProperty(nameof(DropDownProperties.DataSource), "primary;Primary\r\nsecondary;Secondary\r\ntertiary;Tertiary\r\nquaternary;Quaternary")]
		[EditingComponentProperty(nameof(DropDownProperties.DefaultValue), "primary")]
		public string BackgroundStyle { get; set; }

		[EditingComponent(MediaFilesSelector.IDENTIFIER)]
		[EditingComponentProperty(nameof(MediaFilesSelectorProperties.MaxFilesLimit), 1)]
		public IList<MediaFilesSelectorItem> Images { get; set; }

		[EditingComponent(TextAreaComponent.IDENTIFIER, Order = 0, Label = "Quote Text")]
		public string QuoteText { get; set; } = "Quote Text";

		[EditingComponent(TextInputComponent.IDENTIFIER, Order = 1, Label = "Quote's author")]
		public string QuoteAuthor { get; set; } = "Quote's author";

		[EditingComponent(TextInputComponent.IDENTIFIER, Order = 1, Label = "Author's Title")]
		public string QuoteTitle { get; set; } = "Author's Title";


		public QuoteBannerWidgetProperties()
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
					Url = mediaService.GetMediaUrl(guid)
				};
			}
			return null;
		}
	}
}