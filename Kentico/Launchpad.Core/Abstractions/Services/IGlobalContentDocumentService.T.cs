using System.Collections.Generic;

namespace Launchpad.Core.Abstractions.Services
{
	public interface IGlobalContentDocumentService<T> : IDocumentService<T>
		where T : class
	{
		IEnumerable<T> GetFromGlobalContent();
	}
}
