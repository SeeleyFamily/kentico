using Launchpad.Core.Abstractions.Models.Summary;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Abstractions.Specifications;
using Launchpad.Core.Extensions;
using Launchpad.Core.Models;
using Launchpad.Infrastructure.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Launchpad.Infrastructure.Services
{
	/// <summary>
	/// Information Regarding Performance Limitations:
	/// The SearchSummaryDocumentService is simplified for usage, but does has performance limitations.
	/// It will perform poorly for the following:
	/// Too many results - > 1000 results
	/// Multiple Services - > Calling another searchable service within the Abstract ToSummaryItem Method
	/// </summary>	
	public abstract class SearchableSummaryDocumentService<TSummaryItem, TDocumentSpecification> : ISearchableSummaryDocumentService<TSummaryItem, TDocumentSpecification>
		where TSummaryItem : ISummaryItem, new()
		where TDocumentSpecification : IDocumentSpecification, new()
	{


		#region Properties
		private IEnumerable<TSummaryItem> SummaryItems { get; set; }
		private IEnumerable<PageNode> PageNodes { get; set; }
		#endregion;


		#region Fields
		private readonly ICategoryService categoryService;
		#endregion


		public SearchableSummaryDocumentService(
			ICategoryService categoryService
		)
		{
			this.categoryService = categoryService;
		}


		public virtual IEnumerable<PageNode> GetPageNodes()
		{
			if (PageNodes == null)
			{
				TDocumentSpecification specification = new TDocumentSpecification()
				{
					PageSize = int.MaxValue
				};
				// Get PageNodes
				var pageNodes = GetPageNodes(specification);
				return pageNodes;
			}
			return PageNodes;
		}


		public virtual IEnumerable<TSummaryItem> GetSummaryItems()
		{
			if (SummaryItems == null)
			{
				SummaryItems = GetPageNodes().Select(x => ToSummaryItem(x)).ToList();
			}
			return SummaryItems;
		}


		public virtual PagedResult<TSummaryItem> Find(TDocumentSpecification specification)
		{
			// Get PageNodes
			var pageNodes = GetPageNodes(specification);
			// Ensure PageNodes have Categories
			pageNodes = EnsureCategories(pageNodes);
			// Apply Specification
			pageNodes = ApplySpecifications(pageNodes, specification);
			// Apply Sort Specification
			pageNodes = ApplySortSpecifications(pageNodes, specification);

			// The following block is commented out for performance
			// It ended up converting the fullset on every call which is not necessary

			//// Covert To Summary Item
			//var summaryItems = pageNodes.Select(x => ToSummaryItem(x));
			//// To Paged Results
			//var pagedResults = summaryItems.ToPagedResult(specification);

			// Ensures only the returned results are converted
			var pagedResults = pageNodes.ToPagedResult(specification);
			var finalResults = pagedResults.ConvertTo(ToSummaryItem);
			return finalResults;
		}


		private IEnumerable<PageNode> EnsureCategories(IEnumerable<PageNode> pageNodes)
		{
			foreach (var pageNode in pageNodes)
			{
				var categories = categoryService.GetDocumentCategories(pageNode);
				pageNode.Categories = categories;
				if (pageNode.CategoryCodeNamePaths == null)
				{
					pageNode.CategoryCodeNamePaths = categories.Select(y => y.CodeNamePath);
				}
			}
			return pageNodes;
		}


		public abstract IEnumerable<PageNode> GetPageNodes(TDocumentSpecification specification);


		public virtual IEnumerable<PageNode> ApplySpecifications(IEnumerable<PageNode> pageNodes, TDocumentSpecification specification)
		{
			return pageNodes.ApplyDocumentSpecification(specification);
		}


		public virtual IEnumerable<PageNode> ApplySortSpecifications(IEnumerable<PageNode> pageNodes, TDocumentSpecification specification)
		{
			return pageNodes.ApplyDocumentSortSpecification(specification);
		}


		public virtual TSummaryItem ToSummaryItem(PageNode pageNode)
		{
			// Allow the preview implementation to handle majority of the assignments here
			// This can be overrided on the subclass per service level..
			TSummaryItem previewSummaryItem = pageNode.ToPreviewSummary<TSummaryItem>();
			return previewSummaryItem;
		}
	}

}
