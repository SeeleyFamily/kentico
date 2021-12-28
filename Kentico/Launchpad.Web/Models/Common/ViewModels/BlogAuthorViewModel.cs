/*
 * Built with Common Launchpad 2.0.2
 */

using CMS.DocumentEngine.Types.Common;
using Launchpad.Core.Abstractions.Providers;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Models.Summary;
using Launchpad.Core.Specifications;
using Launchpad.Infrastructure.Kentico.Web.Attributes;
using System;

namespace Launchpad.Web.Models.Common.ViewModels
{

	[EnablePageBuilder, RegisterForPageType(BlogAuthor.CLASS_NAME)]
	public class BlogAuthorViewModel : BlogAuthorViewModell<BlogSummaryItem, BlogSpecification, IBlogService>
	{


		public BlogAuthorViewModel
		(
			ILayoutProvider layoutProvider,
			IBlogService blogService
		)
			: base(layoutProvider, blogService)
		{
			SearchableService = blogService;
		}


		protected override void PopulateSpecification()
		{
			Guid[] authors = new Guid[]
			{
				Node.NodeGuid
			};

			Specification = new BlogSpecification(HttpContext.Request.QueryString)
			{
				//Path = Node.NodeAliasPath, // No Path Specification here as blog details can be located else where...
				Authors = authors
			};
		}

	}

}