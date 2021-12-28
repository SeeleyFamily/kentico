using Launchpad.Core.Models;
using Launchpad.Core.Models.Summary;
using System.Collections.Generic;

namespace Launchpad.Core.Abstractions.Services
{
	// TODO FLESH OUT THIS EXAMPLE
	public interface IRelatedExampleService
	{
		IEnumerable<ContentSummaryItem> GetRelatedExampleContent(PageNode node, int count);
	}
}
