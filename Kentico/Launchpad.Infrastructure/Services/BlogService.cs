using CMS.DocumentEngine.Types.Common;
using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Models.Summary;
using Launchpad.Core.Specifications;

namespace Launchpad.Infrastructure.Services
{

	public class BlogService : BlogService<BlogSummaryItem, BlogSpecification>,
		IBlogService,
		IPerScopeService
	{


		public BlogService(

			ICategoryService categoryService,
			IDocumentService<BlogDetail> blogDetailDocumentService,
			IBlogAuthorService blogAuthorService
		) : base(categoryService, blogDetailDocumentService, blogAuthorService)
		{			
		}	
		

	}

}