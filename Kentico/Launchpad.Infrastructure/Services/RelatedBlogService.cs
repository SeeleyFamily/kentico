using CMS.DocumentEngine.Types.Common;
using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Extensions;
using Launchpad.Core.Models;
using Launchpad.Core.Models.Summary;
using Launchpad.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Launchpad.Infrastructure.Services
{
	public class RelatedBlogService : RelatedSummaryService<BlogSummaryItem>, IRelatedBlogService, IPerScopeService
	{
		#region Fields
		private readonly IBlogAuthorService blogAuthorService;
		#endregion

		// this is a good example of how to use the RelatedService to allow for a custom summary item return type...
		// instead of the base ISummaryItem, we want to return BlogSummaryItem and set the additional properties.
		// This code was pulled from CustomRelatedService.cs in CX-Enlivant authored by Ramiro Santos
		// Note the differences, this implementation does not replace the implementation of IRelatedService, but extends it

		public RelatedBlogService(
			IDocumentService documentService, 
			IDocumentQueryConfiguration queryConfiguration, 
			IRelatedService<PageNode> pageNodeRelatedService,
			IBlogAuthorService blogAuthorService
			) : 
			base(
				documentService, 
				queryConfiguration, 
				pageNodeRelatedService
				)
		{
			this.blogAuthorService = blogAuthorService;
		}

		public override IEnumerable<BlogSummaryItem> ToSummaryItems(IEnumerable<PageNode> pageNodes)
		{						
			// take note of how related summary items are populated below
			// the related services uses preview services to popualte the nodes for performance
			// this means that page type specific fields will not be available
			var relatedSummaryItems = pageNodes.Select(x =>
			{
				var previewSummaryItem = x.ToPreviewSummary<BlogSummaryItem>();				 				
				// the below won't work since we don't populate fields for page type specific fields				
				previewSummaryItem.Date = x.Fields.GetDateTimeValue(nameof(BlogDetail.PublishDate));
				// this will work since we load it into document custom data
				previewSummaryItem.Date = x.CustomData.GetDateTimeValue(nameof(Preview.PreviewDate));
				// the below likely does work right now
				previewSummaryItem.Authors = GetBlogAuthorSummaryItems(x.Fields.GetGuidArray(nameof(BlogDetail.Authors)));			
				return previewSummaryItem;
			});
			return relatedSummaryItems;
		}

		private IEnumerable<BlogAuthorSummaryItem> GetBlogAuthorSummaryItems(Guid[] guids)
		{
			IEnumerable<BlogAuthorSummaryItem> authorSummaryItems = Enumerable.Empty<BlogAuthorSummaryItem>();
			if (!guids.IsNullOrEmpty())
			{
				var blogAuthorSummaryItems = blogAuthorService.GetSummaryItems();
				authorSummaryItems = blogAuthorSummaryItems.Where(x => guids.Contains(x.Guid)).OrderBy(x => Array.IndexOf(guids, x.Guid));
			}
			return authorSummaryItems;

		}
	}
}
