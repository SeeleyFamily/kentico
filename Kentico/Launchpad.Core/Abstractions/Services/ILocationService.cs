using Launchpad.Core.Abstractions.Specifications;
using Launchpad.Core.Models;


namespace Launchpad.Core.Abstractions.Services
{

	public interface ILocationService<T, TSpecification> : IDocumentService<T>, ISearchableService<T, TSpecification>
		where T : Location, new()
		where TSpecification : ILocationSpecification
	{

	}

}
