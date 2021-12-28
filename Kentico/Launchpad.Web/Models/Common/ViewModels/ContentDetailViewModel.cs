/*
 * Built with Common Launchpad 2.0.2
 */

using CMS.DocumentEngine.Types.Common;
using Launchpad.Core.Abstractions.Models.Summary;
using Launchpad.Core.Abstractions.Providers;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Enums;
using Launchpad.Core.Extensions;
using Launchpad.Core.Models.Summary;
using Launchpad.Core.Specifications;
using Launchpad.Infrastructure.Kentico.Web.Attributes;
using Launchpad.Infrastructure.Kentico.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Launchpad.Web.Models.Common.ViewModels
{

	[EnablePageBuilder, RegisterForPageType(ContentDetail.CLASS_NAME)]
	public class ContentDetailViewModel : BaseViewModel
	{


		#region KenticoProperties
		public string Headline { get; protected set; }
		public DateTime PublishDate { get; protected set; }
		public bool HidePublishDate { get; protected set; }
		public string HeroBackgroundImage { get; protected set; }
		public string HeroBackgroundImageMobile { get; protected set; }
		public string Authors { get; protected set; }
		public string Content { get; protected set; }
		public string RelatedContent { get; protected set; }
		#endregion


		#region Properties				
		public override string ViewPath { get; protected set; } = $"ContentDetail/Index";
		public HeroViewModel HeroViewModel { get; protected set; }
		public WysiwygViewModel WysiwygViewModel { get; protected set; }
		public PageBuilderViewModel PageBuilderViewModel { get; protected set; }
		public IEnumerable<ISummaryItem> AuthorsSummaryItems { get; protected set; }
		public IEnumerable<ISummaryItem> RelatedSummaryItems { get; protected set; } = Enumerable.Empty<SummaryItem>();
		#endregion


		#region fields
		private readonly IRelatedService relatedService;
		#endregion


		public ContentDetailViewModel
		(
			ILayoutProvider layoutProvider,
			IRelatedService relatedService
		)
			: base(layoutProvider)
		{
			this.relatedService = relatedService;
		}


		protected override void Populate()
		{
			base.Populate();
			PopulateHero();
			PopulateWysiywg();
			PopulatePageBuilder();
			PopulateRelated();
		}

		protected virtual void PopulateRelated()
		{
			Guid[] guids = RelatedContent.ToGuidArray();
			var specification = new RelatedSpecification()
			{
				ClassNames = new List<string>() { BlogDetail.CLASS_NAME }.ToArray(),
				FeaturedGuids = guids,
			};
			RelatedSummaryItems = relatedService.GetRelatedSummaryItems(Node, specification);
		}

		protected virtual void PopulateHero()
		{
			HeroViewModel = new HeroViewModel()
			{
				BackgroundType = BackgroundType.Secondary,
				Headline = Headline,
				Image = HeroBackgroundImage,
				ImageMobile = HeroBackgroundImageMobile,
				Breadcrumbs = Breadcrumbs,
				Date = !HidePublishDate ? PublishDate.ToString("MMMM d, yyyy") : null,
				Tags = new List<string>()
				{
					"Tag 1",
					"Tag 2",
					"Tag 3",
				},
				SectionClass = "hero--detail"
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