using CMS.DocumentEngine.Types.Common;
using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Models.Summary;
using Launchpad.Core.Specifications;


namespace Launchpad.Infrastructure.Services
{

	public class PeopleService : PeopleService<PeopleSummaryItem, PeopleSpecification>,
		IPeopleService,
		IPerScopeService
	{


		public PeopleService(

			ICategoryService categoryService,
			IDocumentService<PeopleProfile> peopleProfileDocumentService
		) : base(categoryService, peopleProfileDocumentService)
		{
		}


	}

}