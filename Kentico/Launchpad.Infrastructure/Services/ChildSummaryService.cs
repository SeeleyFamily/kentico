using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Models.Summary;
using Launchpad.Core.Specifications;
using Launchpad.Infrastructure.Abstractions.Services;

namespace Launchpad.Infrastructure.Services
{
	public class ChildSummaryService : ChildSummaryService<SummaryItem, ChildSummarySpecification>, IChildSummaryService, IPerScopeService
	{
		public ChildSummaryService(
			ICacheService cacheService,
			IDocumentQueryConfiguration queryConfiguration,
			IDocumentService documentService
		) : base(
			cacheService,
			queryConfiguration,
			documentService
		)
		{
		}
	}
}
