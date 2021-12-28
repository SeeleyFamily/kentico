/*
 * Built with Common Launchpad 2.0.2
 */

using CMS.DocumentEngine.Types.Common;
using Launchpad.Core.Abstractions.Providers;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Extensions;
using Launchpad.Core.Models.Summary;
using Launchpad.Core.Specifications;
using Launchpad.Infrastructure.Kentico.Web.Attributes;

namespace Launchpad.Web.Models.Common.ViewModels
{

	[RegisterForPageType(BlogListing.CLASS_NAME)]
	public class BlogListingViewModel : BlogListingViewModel<BlogSummaryItem, BlogSpecification, IBlogService>
	{
		public BlogListingViewModel
		(
			ILayoutProvider layoutProvider,
			IBlogService blogService,
			ICategoryFilterProvider categoryFilterProvider
		)
			: base(layoutProvider, blogService, categoryFilterProvider)
		{
			SearchableService = blogService;
		}


		protected override void PopulateSpecification()
		{
			Specification = new BlogSpecification(HttpContext.Request.QueryString)
			{
				Path = Node.NodeAliasPath,
				FeaturedGuids = FeaturedContent.ToGuidArray()
			};
		}


	}
}