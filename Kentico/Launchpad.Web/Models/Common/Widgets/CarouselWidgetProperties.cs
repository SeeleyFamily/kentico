/*
 * Built with Common Launchpad 2.0.2
 */

using Kentico.Components.Web.Mvc.FormComponents;
using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Models;
using Launchpad.Web.Models.Custom.Widgets;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Launchpad.Web.Models.Common.Widgets
{
	public class CarouselWidgetProperties : WidgetProperties, IWidgetProperties
	{
		private readonly IMediaService mediaService;
		// Assigns a selector component to the Pages property
		[EditingComponent(PageSelector.IDENTIFIER, Order = 1, Label = "Carousel Folder")]
		[EditingComponentProperty(nameof(PageSelectorProperties.ExplanationText), "The folder that contains the images for the carousel")]
		[EditingComponentProperty(nameof(PageSelectorProperties.Required), true)]
		// Returns a list of page selector items (node GUIDs)
		public IList<PageSelectorItem> CarouselContainer { get; set; }

		public CarouselWidgetProperties()
		{
			this.mediaService = DependencyResolver.Current.GetService<IMediaService>();
		}

		public IEnumerable<MediaFile> GetCarouselItems()
		{
			PageSelectorItem folder = CarouselContainer?.FirstOrDefault();
			if (folder != null)
			{
				return mediaService.GetGalleryFolderAssets(folder.NodeGuid);
			}
			return null;
		}
	}
}