using Launchpad.Core.Models;
using System.Collections.Generic;

namespace Launchpad.Core.Abstractions.Services
{
	public interface IPreviewService
	{
		IEnumerable<PageNode> GetPreviewNodes(bool useDocumentService = true);
		IEnumerable<PageNode> GetPreviewNodesFromDocumentQuery();
		IEnumerable<PageNode> GetPreviewNodesFromDocumentService();
	}
}
