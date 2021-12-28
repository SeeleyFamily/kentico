/*
 * Built with Common Launchpad 2.0.2
 */

using CMS.DocumentEngine.Types.Common;
using Launchpad.Core.Abstractions.Providers;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Models.Summary;
using Launchpad.Core.Specifications;
using Launchpad.Infrastructure.Kentico.Web.Attributes;

namespace Launchpad.Web.Models.Common.ViewModels
{

	[RegisterForPageType(BlogAuthorListing.CLASS_NAME)]
	public class BlogAuthorListingViewModel : BlogAuthorListingViewModel<BlogAuthorSummaryItem, BlogAuthorSpecification, IBlogAuthorService>
	{

		public BlogAuthorListingViewModel
		(
			ILayoutProvider layoutProvider,
			IBlogAuthorService blogAuthorService
		)
			: base(layoutProvider, blogAuthorService)
		{
			SearchableService = blogAuthorService;
		}


		protected override void PopulateSpecification()
		{
			Specification = new BlogAuthorSpecification(HttpContext.Request.QueryString)
			{
				Path = Node.NodeAliasPath,
			};
		}


	}

}