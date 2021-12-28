using CMS.DocumentEngine.Types.Common;
using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Attributes;
using Launchpad.Core.Enums;
using Launchpad.Core.Extensions;
using Launchpad.Core.Models;
using Launchpad.Core.Models.Summary;
using Launchpad.Core.Specifications;
using Launchpad.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Launchpad.Infrastructure.Services
{
	public class ContentService<TSummaryItem, TDocumentSpecification> :
		SearchableSummaryDocumentService<TSummaryItem, TDocumentSpecification>,
		IContentService<TSummaryItem, TDocumentSpecification>,
		IPerScopeService
		where TSummaryItem : ContentSummaryItem, new()
		where TDocumentSpecification : ContentSpecification, new()
	{
		#region Fields
		private readonly IDocumentService<ContentDetail> contentDetailContentService;
		private readonly IDocumentService<AssetResource> assetResourceContentService;
		private readonly IDocumentService<ExternalResource> externalResourceContentService;
		#endregion


		public ContentService(
			ICategoryService categoryService,
			IDocumentService<ContentDetail> contentDetailContentService,
			IDocumentService<AssetResource> assetResourceContentService,
			IDocumentService<ExternalResource> externalResourceContentService
		) : base(categoryService)
		{
			this.contentDetailContentService = contentDetailContentService;
			this.assetResourceContentService = assetResourceContentService;
			this.externalResourceContentService = externalResourceContentService;
		}

		public override IEnumerable<PageNode> GetPageNodes(TDocumentSpecification specification)
		{
			List<PageNode> pageNodes = new List<PageNode>();

			pageNodes.AddRange(contentDetailContentService.Get().Select(x => x.ToPageNode()));
			pageNodes.AddRange(assetResourceContentService.Get().Select(x => x.ToPageNode()));
			pageNodes.AddRange(externalResourceContentService.Get().Select(x => x.ToPageNode()));

			return pageNodes;
		}

		public override TSummaryItem ToSummaryItem(PageNode pageNode)
		{
			var summaryItem = base.ToSummaryItem(pageNode);

			var featured = pageNode.FeatureOrder > 0;
			summaryItem.Featured = featured;

			var date = pageNode.Fields.GetDateTimeValue(nameof(ContentDetail.PublishDate));
			summaryItem.Date = date;
		
			return summaryItem;
		}

		public override IEnumerable<PageNode> ApplySortSpecifications(IEnumerable<PageNode> pageNodes, TDocumentSpecification specification)
		{
			if (!string.IsNullOrWhiteSpace(specification.Sort))
			{
				if (SortType.Newest.GetAttribute<CodeDisplayNameTypeAttribute>().CodeName.Equals(specification.Sort, StringComparison.InvariantCultureIgnoreCase))
				{
					pageNodes = pageNodes.OrderByDescending(x =>
					{
						var date = x.Fields.GetDateTimeValue(nameof(ContentDetail.PublishDate));
						return date;
					}
					);
				}
			}

			pageNodes = base.ApplySortSpecifications(pageNodes, specification);

			pageNodes = pageNodes.ApplyFeaturedSortSpecification(specification);

			return pageNodes;
		}
	}
}