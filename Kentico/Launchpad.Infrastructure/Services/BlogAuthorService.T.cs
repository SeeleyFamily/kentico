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

	public class BlogAuthorService<TSummaryItem, TDocumentSpecification> :
		SearchableSummaryDocumentService<TSummaryItem, TDocumentSpecification>,
		IBlogAuthorService<TSummaryItem, TDocumentSpecification>,
		IPerScopeService
		where TSummaryItem : BlogAuthorSummaryItem, new()
		where TDocumentSpecification : BlogAuthorSpecification, new()
	{


		#region Fields
		private readonly IDocumentService<BlogAuthor> blogAuthorDocumentService;
		#endregion


		public BlogAuthorService(
			ICategoryService categoryService,
			IDocumentService<BlogAuthor> blogAuthorDocumentService
		) : base(categoryService)
		{
			this.blogAuthorDocumentService = blogAuthorDocumentService;
		}


		public override IEnumerable<PageNode> GetPageNodes(TDocumentSpecification specification)
		{
			List<PageNode> pageNodes = new List<PageNode>();

			// default get() gets all page node data
			// below is optimized to get only the properties required
			// note how we are not retrieving image with this query
			// instead we using documentCustomData to get that information
			// both approaches are valid
			pageNodes.AddRange(blogAuthorDocumentService.Get(
				new string[] {
					nameof(BlogAuthor.FirstName),
					nameof(BlogAuthor.LastName),
					nameof(BlogAuthor.Title)
				}
			).Select(x => x.ToPageNode()));

			return pageNodes;
		}


		public override TSummaryItem ToSummaryItem(PageNode pageNode)
		{
			var summaryItem = base.ToSummaryItem(pageNode);

			// there are various ways to pull data from the cms here
			// preview fields are prepopulated via the cms
			var example = string.Empty;
			// sumaryItem.Title is already populated via documentCustomDataModuleService
			example = summaryItem.Title;
			// customData is also accessible via CustomData property
			example = pageNode.CustomData.GetStringValue(nameof(BlogAuthor.Title));			

			// we can access properties referenced above in the GetPageNodes method above			
			var blogAuthorTitle = pageNode.Fields.GetStringValue(nameof(BlogAuthor.Title));
			summaryItem.BlogAuthorTitle = blogAuthorTitle;

			var firstName = pageNode.Fields.GetStringValue(nameof(BlogAuthor.FirstName));
			var lastName = pageNode.Fields.GetStringValue(nameof(BlogAuthor.LastName));
			var blogAuthorFullName = $"{firstName} {lastName}".Trim();			
			summaryItem.BlogAuthorFullName = blogAuthorFullName;

			return summaryItem;
		}


		public override IEnumerable<PageNode> ApplySortSpecifications(IEnumerable<PageNode> pageNodes, TDocumentSpecification specification)
		{
			// Order by Last Name
			if (!string.IsNullOrWhiteSpace(specification.Sort))
			{
				if (SortType.AZ.GetAttribute<CodeDisplayNameTypeAttribute>().CodeName.Equals(specification.Sort, StringComparison.InvariantCultureIgnoreCase))
				{
					pageNodes = pageNodes.OrderBy(x =>
					{
						var lastName = x.Fields.GetStringValue(nameof(BlogAuthor.LastName));
						return lastName;
					}
					);
				}
			}
			return base.ApplySortSpecifications(pageNodes, specification);
		}

	}

}
