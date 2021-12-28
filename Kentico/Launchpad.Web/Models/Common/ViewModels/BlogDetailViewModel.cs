/*
 * Built with Common Launchpad 2.0.2
 */

using CMS.DocumentEngine.Types.Common;
using Launchpad.Core.Abstractions.Providers;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Attributes;
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

	[EnablePageBuilder, RegisterForPageType(BlogDetail.CLASS_NAME)]
	public class BlogDetailViewModel : BaseViewModel
	{


		#region KenticoProperties
		public string Headline { get; protected set; }
		public DateTime PublishDate { get; protected set; }
		public string HeroBackgroundImage { get; protected set; }
		public string HeroBackgroundImageMobile { get; protected set; }
		public string Authors { get; protected set; }
		public string Content { get; protected set; }
		public string RelatedContent { get; set; }
		#endregion


		#region Properties				
		public override string ViewPath { get; protected set; } = $"BlogDetail/Index";
		public HeroViewModel HeroViewModel { get; protected set; }
		public WysiwygViewModel WysiwygViewModel { get; protected set; }
		public PageBuilderViewModel PageBuilderViewModel { get; protected set; }
		public IEnumerable<BlogAuthorSummaryItem> AuthorSummaryItems { get; protected set; } = Enumerable.Empty<BlogAuthorSummaryItem>();
		public IEnumerable<BlogSummaryItem> RelatedBlogSummaryItems { get; protected set; } = Enumerable.Empty<BlogSummaryItem>();
		#endregion


		#region fields
		private readonly IBlogAuthorService blogAuthorService;
		private readonly IRelatedService relatedService;
		private readonly IRelatedBlogService relatedBlogService;
		#endregion


		public BlogDetailViewModel
		(
			ILayoutProvider layoutProvider,
			IBlogAuthorService blogAuthorService,
			IRelatedService relatedService,
			IRelatedBlogService relatedBlogService
		)
			: base(layoutProvider)
		{
			this.blogAuthorService = blogAuthorService;
			this.relatedService = relatedService;
			this.relatedBlogService = relatedBlogService;
		}


		protected override void Populate()
		{
			base.Populate();
			PopulateHero();
			PopulateWysiywg();
			PopulatePageBuilder();
			PopulateAuthors();
			PopulateRelated();
		}


		protected virtual void PopulateAuthors()
		{
			if (string.IsNullOrEmpty(Authors)) return;
			Guid[] guids = Authors.ToGuidArray();
			var specification = new BlogAuthorSpecification()
			{
				Guids = guids,
				Sort = SortType.Guids.GetAttribute<CodeDisplayNameTypeAttribute>().CodeName,
				PageSize = int.MaxValue,
			};

			var result = blogAuthorService.Find(specification);

			AuthorSummaryItems = result.Items;
		}


		protected virtual void PopulateRelated()
		{
			Guid[] guids = RelatedContent.ToGuidArray();
			var specification = new RelatedSpecification()
			{
				ClassNames = new List<string>() { BlogDetail.CLASS_NAME }.ToArray(),
				FeaturedGuids = guids,
			};
			//relatedService.GetRelatedSummaryItems(Node, specification);
			RelatedBlogSummaryItems = relatedBlogService.GetRelatedSummaryItems(Node, specification);
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
				Date = PublishDate.ToString("MMMM d, yyyy"),
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