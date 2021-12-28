using Launchpad.Core.Models;
using System.Collections.Generic;

namespace Launchpad.Core.Abstractions.Services
{
	public interface IDocumentUrlPathInfoService
	{
		DocumentUrlPathInfo Get(string documentUrlPath);

		IEnumerable<DocumentUrlPathInfo> Get();
	}
}
