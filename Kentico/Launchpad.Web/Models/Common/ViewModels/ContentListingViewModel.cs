/*
 * Built with Common Launchpad 2.0.2
 */

using CMS.DocumentEngine.Types.Common;
using Launchpad.Core.Abstractions.Providers;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Models.Summary;
using Launchpad.Core.Specifications;
using Launchpad.Infrastructure.Kentico.Web.Attributes;

namespace Launchpad.Web.Models.Common.ViewModels
{
	[RegisterForPageType(ContentListing.CLASS_NAME)]
	public class ContentListingViewModel : ContentListingViewModel<ContentSummaryItem, ContentSpecification, IContentService>
	{
		public ContentListingViewModel
		(
			ILayoutProvider layoutProvider,
			IContentService contentService,
			ICategoryService categoryService,
			ICategoryFilterProvider categoryFilterProvider,
			IDocumentService documentService
		)
			: base(layoutProvider, contentService, categoryService, categoryFilterProvider, documentService)
		{
			SearchableService = contentService;
		}
	}

}