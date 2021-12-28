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

	public class BlogService<TSummaryItem, TDocumentSpecification> :
		SearchableSummaryDocumentService<TSummaryItem, TDocumentSpecification>,
		IBlogService<TSummaryItem, TDocumentSpecification>,
		IPerScopeService
		where TSummaryItem : BlogSummaryItem, new()
		where TDocumentSpecification : BlogSpecification, new()
	{

		#region Properties
		#endregion

		#region Fields
		private readonly IDocumentService<BlogDetail> blogDetailDocumentService;
		private readonly IBlogAuthorService blogAuthorService;
		#endregion


		public BlogService(
			ICategoryService categoryService,
			IDocumentService<BlogDetail> blogDetailDocumentService,
			IBlogAuthorService blogAuthorService
		) : base(categoryService)
		{
			this.blogDetailDocumentService = blogDetailDocumentService;
			this.blogAuthorService = blogAuthorService;
		}


		public override IEnumerable<PageNode> GetPageNodes(TDocumentSpecification specification)
		{
			List<PageNode> pageNodes = new List<PageNode>();
			pageNodes.AddRange(
				blogDetailDocumentService.Get(
					new string[]{
						nameof(BlogDetail.PublishDate),
						nameof(BlogDetail.Authors)
					}
				)
				.Select(x => x.ToPageNode())
			);
			return pageNodes;
		}

		public override TSummaryItem ToSummaryItem(PageNode pageNode)
		{
			var summaryItem = base.ToSummaryItem(pageNode);

			var featured = pageNode.FeatureOrder > 0;
			summaryItem.Featured = featured;

			var date = pageNode.Fields.GetDateTimeValue(nameof(BlogDetail.PublishDate));
			summaryItem.Date = date;

			var authors = pageNode.Fields.GetStringValue(nameof(BlogDetail.Authors));
			IEnumerable<BlogAuthorSummaryItem> authorSummaryItems = Enumerable.Empty<BlogAuthorSummaryItem>();

			if (!string.IsNullOrWhiteSpace(authors))
			{
				Guid[] guids = authors.ToGuidArray(';');    // TODO: Better, less magic-stringed fix, this is a temporary bandaid				
				authorSummaryItems = GetBlogAuthorSummaryItems(guids);
			}

			summaryItem.Authors = authorSummaryItems;

			return summaryItem;
		}

		public override IEnumerable<PageNode> ApplySpecifications(IEnumerable<PageNode> pageNodes, TDocumentSpecification specification)
		{
			pageNodes = base.ApplySpecifications(pageNodes, specification);
			pageNodes = ApplyAuthorsSpecification(pageNodes, specification);
			pageNodes = ApplyTopicsSpecification(pageNodes, specification);

			return pageNodes;
		}

		private IEnumerable<PageNode> ApplyAuthorsSpecification(IEnumerable<PageNode> pageNodes, TDocumentSpecification specification)
		{
			if (!specification.Authors.IsNullOrEmpty())
			{
				pageNodes = pageNodes.Where(x =>
				{
					var authors = x.Fields.GetStringValue(nameof(BlogDetail.Authors));
					var authorGuids = authors.ToGuidArray();
					if (!authorGuids.IsNullOrEmpty())
					{
						if (specification.Authors.Any(y => authorGuids.Contains(y)))
						{
							return true;
						}
					}
					return false;
				});
			}

			return pageNodes;
		}

		private IEnumerable<PageNode> ApplyTopicsSpecification(IEnumerable<PageNode> pageNodes, TDocumentSpecification specification)
		{
			pageNodes = ApplyIncludedTopicsSpecification(pageNodes, specification);
			pageNodes = ApplyExcludedTopicsSpecification(pageNodes, specification);
			return pageNodes;
		}

		private IEnumerable<PageNode> ApplyIncludedTopicsSpecification(IEnumerable<PageNode> pageNodes, TDocumentSpecification specification)
		{
			if (!specification.Topics.IsNullOrEmpty())
			{
				pageNodes = pageNodes.Where(x =>
				{
					var categories = x.Categories;
					if (categories == null)
					{
						categories = new List<Category>();
					}
					return x.Categories.Any(y => specification.Topics.Any(z => z.Equals(y.CodeName, StringComparison.InvariantCultureIgnoreCase)));
				}
				);
			}

			return pageNodes;
		}

		private IEnumerable<PageNode> ApplyExcludedTopicsSpecification(IEnumerable<PageNode> pageNodes, TDocumentSpecification specification)
		{
			if (!specification.ExcludedTopics.IsNullOrEmpty())
			{
				pageNodes = pageNodes.Where(x =>
				{
					var categories = x.Categories;
					if (categories == null)
					{
						categories = new List<Category>();
					}
					return !x.Categories.Any(y => specification.ExcludedTopics.Any(z => z.Equals(y.CodeName, StringComparison.InvariantCultureIgnoreCase)));
				}
				);
			}
			return pageNodes;
		}

		public override IEnumerable<PageNode> ApplySortSpecifications(IEnumerable<PageNode> pageNodes, TDocumentSpecification specification)
		{
			if (!string.IsNullOrWhiteSpace(specification.Sort))
			{
				if (SortType.Newest.GetAttribute<CodeDisplayNameTypeAttribute>().CodeName.Equals(specification.Sort, StringComparison.InvariantCultureIgnoreCase))
				{
					pageNodes = pageNodes.OrderByDescending(x =>
					{
						var date = x.Fields.GetDateTimeValue(nameof(BlogDetail.PublishDate));
						return date;
					}
					);
				}
			}

			pageNodes = base.ApplySortSpecifications(pageNodes, specification);

			pageNodes = pageNodes.ApplyFeaturedSortSpecification(specification);

			return pageNodes;
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
