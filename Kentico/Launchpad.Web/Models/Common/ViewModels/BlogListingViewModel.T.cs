/*
 * Built with Common Launchpad 2.0.2
 */

using Launchpad.Core.Abstractions.Providers;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Extensions;
using Launchpad.Core.Models.Summary;
using Launchpad.Core.Specifications;
using Launchpad.Infrastructure.Kentico.Web.Models.ViewModels;
using System.Linq;

namespace Launchpad.Web.Models.Common.ViewModels
{

	public abstract class BlogListingViewModel<TSummaryItem, TSpecification, TService> : ListingViewModel<TSummaryItem, TSpecification, TService>
		where TSummaryItem : BlogSummaryItem
		where TSpecification : BlogSpecification
		where TService : ISearchableSummaryDocumentService<TSummaryItem, TSpecification>
	{


		#region KenticoProperties
		public string Headline { get; protected set; }
		public string Description { get; protected set; }
		public string HeroBackgroundImage { get; protected set; }
		public string HeroBackgroundImageMobile { get; protected set; }
		public string Content { get; protected set; }
		public string FeaturedContent { get; protected set; }
		public bool HideFilters { get; protected set; }
		public bool HideSearchFilter { get; protected set; }
		public bool HideTopicFilter { get; protected set; }
		public string TopicFilter { get; protected set; }
		public string Collections { get; protected set; }
		public string CollectionsPath { get; protected set; }
		#endregion


		#region Properties				
		public override string ViewPath { get; protected set; } = $"BlogListing/Index";
		public override string ApiRoute { get; protected set; } = "Blog";
		public HeroViewModel HeroViewModel { get; protected set; }
		public WysiwygViewModel WysiwygViewModel { get; protected set; }
		public PageBuilderViewModel PageBuilderViewModel { get; protected set; }
		#endregion


		#region fields
		private readonly ICategoryFilterProvider categoryFilterProvider;
		#endregion


		public BlogListingViewModel
		(
			ILayoutProvider layoutProvider,
			TService searchableService,
			ICategoryFilterProvider categoryFilterProvider
		)
			: base(layoutProvider)
		{
			this.SearchableService = searchableService;
			this.categoryFilterProvider = categoryFilterProvider;
		}


		protected override void Populate()
		{
			base.Populate();
			PopulateHero();
			PopulateWysiywg();
			PopulatePageBuilder();
		}


		protected virtual void PopulateHero()
		{
			HeroViewModel = new HeroViewModel()
			{
				Headline = Headline,
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


		protected virtual void PopulatePageBuilder()
		{
			PageBuilderViewModel = new PageBuilderViewModel(Node.PageBuilderWidgets);
		}


		protected override void PopulateFilters()
		{
			// Uni selector control default separator is ';'
			var categoryGuids = TopicFilter.ToGuidArray(';');
			if (!categoryGuids.IsNullOrEmpty())
			{
				if (categoryGuids.Length == 1)
				{
					var topicsFilter = categoryFilterProvider.GetParentCategoryFilter(categoryGuids.FirstOrDefault(), nameof(BlogSpecification.Topics));
					Filters.Add(topicsFilter);
				}
				else
				{
					var topicsFilter = categoryFilterProvider.GetCategoryFilter(categoryGuids, nameof(BlogSpecification.Topics));
					Filters.Add(topicsFilter);
				}
			}
		}


		protected override void PopulatePushStateParameters()
		{
			base.PopulatePushStateParameters();
			PushStateParameters.Add(nameof(Specification.PageIndex));
		}
	}
}