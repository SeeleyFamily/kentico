using CMS.DocumentEngine.Types.Common;
using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Models.Summary;
using Launchpad.Core.Specifications;


namespace Launchpad.Infrastructure.Services
{

	public class ContentService : ContentService<ContentSummaryItem, ContentSpecification>,
		IContentService,
		IPerScopeService
	{
		public ContentService(

			ICategoryService categoryService,
			IDocumentService<ContentDetail> contentDetailContentService,
			IDocumentService<AssetResource> assetResourceContentService,
			IDocumentService<ExternalResource> externalResourceContentService) : base(categoryService, contentDetailContentService, assetResourceContentService, externalResourceContentService)
		{			
		}

	}
}