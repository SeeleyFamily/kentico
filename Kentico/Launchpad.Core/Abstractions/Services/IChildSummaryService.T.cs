using Launchpad.Core.Abstractions.Specifications;
using Launchpad.Core.Models;
using Launchpad.Core.Models.Summary;
using Launchpad.Core.Specifications;
using System;
using System.Collections.Generic;

namespace Launchpad.Core.Abstractions.Services
{
	public interface IChildSummaryService<TSummaryItem, TChildSummarySpecification>
		where TSummaryItem : SummaryItem
		where TChildSummarySpecification : IChildSummarySpecification
	{
		IEnumerable<TSummaryItem> GetChildSummaryItems(int nodeId, TChildSummarySpecification childSummarySpecification);
		IEnumerable<TSummaryItem> GetChildSummaryItems(Guid nodeGuid, TChildSummarySpecification childSummarySpecification);
		IEnumerable<TSummaryItem> GetChildSummaryItems(PageNode pageNode, TChildSummarySpecification childSummarySpecification);
	}
}
