using CMS.DocumentEngine.Types.Common;
using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Models.Summary;
using Launchpad.Core.Specifications;

namespace Launchpad.Infrastructure.Services
{

	public class BlogAuthorService : BlogAuthorService<BlogAuthorSummaryItem, BlogAuthorSpecification>,
		IBlogAuthorService,
		IPerScopeService
	{


		public BlogAuthorService(

			ICategoryService categoryService,
			IDocumentService<BlogAuthor> blogAuthorDocumentService
		) : base(categoryService, blogAuthorDocumentService)
		{
		}


	}

}