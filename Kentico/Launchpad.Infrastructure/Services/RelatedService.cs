using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Core.Abstractions.Models.Summary;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Models;

namespace Launchpad.Infrastructure.Services
{
	public class RelatedService : RelatedSummaryService<ISummaryItem>, IRelatedService, IPerScopeService
	{
		public RelatedService(IDocumentService documentService, IDocumentQueryConfiguration queryConfiguration, IRelatedService<PageNode> pageNodeRelatedService) : base(documentService, queryConfiguration, pageNodeRelatedService)
		{
		}
	}
}
