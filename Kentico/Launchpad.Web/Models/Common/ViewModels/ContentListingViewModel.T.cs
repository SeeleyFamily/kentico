/*
 * Built with Common Launchpad 2.0.2
 */

using Launchpad.Core.Abstractions.Providers;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Attributes;
using Launchpad.Core.Enums;
using Launchpad.Core.Extensions;
using Launchpad.Core.Models.Summary;
using Launchpad.Core.Specifications;
using Launchpad.Infrastructure.Kentico.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Launchpad.Web.Models.Common.ViewModels
{
	public abstract class ContentListingViewModel<TSummaryItem, TSpecification, TService> : ListingViewModel<TSummaryItem, TSpecification, TService>
		where TSummaryItem : ContentSummaryItem
		where TSpecification : ContentSpecification, new()
		where TService : ISearchableSummaryDocumentService<TSummaryItem, TSpecification>
	{
		#region KenticoProperties
		public string Headline { get; set; }
		public string Description { get; set; }
		public string HeroBackgroundImage { get; protected set; }
		public string HeroBackgroundImageMobile { get; protected set; }
		public string Content { get; protected set; }
		public string FeaturedContent { get; set; }
		public bool HideFilters { get; set; }
		public bool HideTypeFilter { get; set; }
		public bool HideTopicFilter { get; set; }
		public bool HideSearchFilter { get; set; }
		public string TypeFilter { get; set; }
		public string TopicFilter { get; set; }
		public string Collections { get; set; }
		public string CollectionsPath { get; set; }
		#endregion

		#region Properties	
		public override string ViewPath { get; protected set; } = $"ContentListing/Index";
		public override string ApiRoute { get; protected set; } = "/Api/Content";
		public HeroViewModel HeroViewModel { get; protected set; }
		public WysiwygViewModel WysiwygViewModel { get; protected set; }
		#endregion

		#region Fields				
		private readonly ICategoryService categoryService;
		private readonly ICategoryFilterProvider categoryFilterProvider;
		private readonly IDocumentService documentService;
		#endregion


		public ContentListingViewModel
		(
			ILayoutProvider layoutProvider,
			TService searchableService,
			ICategoryService categoryService,
			ICategoryFilterProvider categoryFilterProvider,
			IDocumentService documentService
		)
			: base(layoutProvider)
		{
			SearchableService = searchableService;
			this.categoryService = categoryService;
			this.categoryFilterProvider = categoryFilterProvider;
			this.documentService = documentService;
		}



		protected override void Populate()
		{
			base.Populate();
			PopulateHero();
			PopulateWysiywg();
		}


		protected override void PopulateFilters()
		{
			if (!HideFilters)
			{
				if (!HideTypeFilter)
				{
					PopulateTypeFilter();
				}
				if (!HideTopicFilter)
				{
					PopulateTopicFilter();
				}
			}
		}


		protected virtual void PopulateHero()
		{
			HeroViewModel = new HeroViewModel()
			{
				Headline = Headline,
				Description = Description,
				Breadcrumbs = Breadcrumbs,
				Image = HeroBackgroundImage,
				ImageMobile = HeroBackgroundImageMobile,
			};
		}


		protected virtual void PopulateWysiywg()
		{
			WysiwygViewModel = new WysiwygViewModel()
			{
				Content = Content
			};
		}


		protected virtual void PopulateTypeFilter()
		{
			if (string.IsNullOrWhiteSpace(TypeFilter))
			{
				return;
			}

			// We are removing the faceted category approach for performance and UX reasons.
			//var availableCategoryValues = ListingSummaryService.GetDistinctValues(nameof(PageNode.CategoryCodeNamePaths), Specification);
			var availableCategoryValues = new List<string>();

			var typeCategory = categoryService.GetCategory(TypeFilter);
			if (typeCategory == null || !typeCategory.CodeNamePath.Contains(DefaultCategory.Types.GetAttribute<CodeDisplayNameTypeAttribute>().CodeName.GetWrappedCodeName()))
			{
				return;
			}
			var typesFilter = categoryFilterProvider.GetParentCategoryFilter(typeCategory.CodeName, nameof(ContentSpecification.Type), 2, availableCategoryValues);
			Filters.Add(typesFilter);
		}


		protected virtual void PopulateTopicFilter()
		{
			// We are removing the faceted category approach for performance and UX reasons.
			//var availableCategoryValues = categoryFilterProvider.GetAvailableCategories(Result);
			var availableCategoryValues = new List<string>();


			if (string.IsNullOrWhiteSpace(TopicFilter))
			{
				var primaryTopicsCategory = categoryService.GetCategory(DefaultCategory.PrimaryTopics.GetAttribute<CodeDisplayNameTypeAttribute>().CodeName);
				if (primaryTopicsCategory == null || !primaryTopicsCategory.CodeNamePath.Contains(DefaultCategory.Topics.GetAttribute<CodeDisplayNameTypeAttribute>().CodeName.GetWrappedCodeName()))
				{
					return;
				}
				var topicsFilter = categoryFilterProvider.GetParentCategoryFilter(primaryTopicsCategory.CodeName, nameof(ContentSpecification.Topic), 2, availableCategoryValues);
				Filters.Add(topicsFilter);
			}
			else
			{
				// Uni Selector Control
				// Multiple selection does ; separator
				var filterTopics = TopicFilter.ToGuidArray(';');
				var topicsFilter = categoryFilterProvider.GetCategoryFilter(filterTopics, nameof(ContentSpecification.Topic), availableCategoryValues);
				Filters.Add(topicsFilter);
			}
		}

		protected override void PopulateSpecification()
		{
			var listingPath = Node.NodeAliasPath;

			// CollectionsPath is a Page Selector instead of Url Selector
			// This provides a NodeGuid instead of the DocumentUrlPath
			// Use NodeGuid to get NodeAliasPath as DocumentUrlPath may exclude page types that do not have a url
			if (!string.IsNullOrWhiteSpace(CollectionsPath))
			{
				if (Guid.TryParse(CollectionsPath, out Guid guid))
				{
					var collectionNode = documentService.Get(guid);
					if (collectionNode != null)
					{
						listingPath = collectionNode.NodeAliasPath;
					}
				}
			}

			var specification = new ContentSpecification(HttpContext.Request.QueryString)
			{
				// Set the Default Specification Values Here - if different from ones specified in the specification class
				Path = listingPath,
				PageSize = 10,
				SearchTerm = null,
				Topic = null,
				Type = null,
				Sort = SortType.Newest.GetAttribute<CodeDisplayNameTypeAttribute>().CodeName
			};

			var featuredGuids = FeaturedContent.ToGuidArray();
			if (!featuredGuids.IsNullOrEmpty())
			{
				specification.FeaturedGuids = featuredGuids;
			}

			// Uni Selector Control
			// Multiple selection does ; separator
			var collections = Collections.ToGuidArray(';');
			if (!collections.IsNullOrEmpty())
			{
				var categories = categoryService.GetCategories(collections);
				if (!categories.IsNullOrEmpty())
				{
					var categoryCodeNames = categories.Select(x => x.CodeName).ToArray();
					specification.Categories = categoryCodeNames;
				}
			}

			Specification = (TSpecification)specification;
		}
	}

}