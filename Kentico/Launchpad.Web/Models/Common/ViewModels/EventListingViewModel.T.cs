/*
 * Built with Common Launchpad 2.0.2
 */

using Launchpad.Core.Abstractions.Providers;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Models.Summary;
using Launchpad.Core.Specifications;
using Launchpad.Infrastructure.Kentico.Web.Models.ViewModels;

namespace Launchpad.Web.Models.Common.ViewModels
{

	public abstract class EventListingViewModel<TSummaryItem, TSpecification, TService> : ListingViewModel<TSummaryItem, TSpecification, TService>
		where TSummaryItem : EventSummaryItem
		where TSpecification : EventSpecification
		where TService : ISearchableSummaryDocumentService<TSummaryItem, TSpecification>

	{


		#region KenticoProperties
		public string Headline { get; protected set; }
		public string Description { get; protected set; }
		public string HeroBackgroundImage { get; protected set; }
		public string HeroBackgroundImageMobile { get; protected set; }
		public string Content { get; protected set; }
		#endregion


		#region Properties                
		public override string ViewPath { get; protected set; } = "EventListing/Index";
		public override string ApiRoute { get; protected set; } = "Event";
		public HeroViewModel HeroViewModel { get; protected set; }
		public WysiwygViewModel WysiwygViewModel { get; protected set; }
		public PageBuilderViewModel PageBuilderViewModel { get; protected set; }
		#endregion


		public EventListingViewModel
		(
			ILayoutProvider layoutProvider,
			TService searchableService
		)
			: base(layoutProvider)
		{
			SearchableService = searchableService;
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


		protected virtual void PopulatePageBuilder()
		{
			PageBuilderViewModel = new PageBuilderViewModel(Node.PageBuilderWidgets);
		}


	}

}
