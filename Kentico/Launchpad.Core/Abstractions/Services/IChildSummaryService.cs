using Launchpad.Core.Models.Summary;
using Launchpad.Core.Specifications;

namespace Launchpad.Core.Abstractions.Services
{
	public interface IChildSummaryService : IChildSummaryService<SummaryItem, ChildSummarySpecification>
	{
	}
}
