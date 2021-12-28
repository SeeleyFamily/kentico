using Launchpad.Core.Abstractions.Specifications;
using Launchpad.Core.Models;


namespace Launchpad.Core.Abstractions.Services
{

	/// <summary>
	/// An interface which provides strict requirements on the implementation of a searchable service.
	/// A searchable service takes a specification and returns a paged result with a set of items <see cref="T"/>
	/// The implementation must handle the getting, filtering, and paging of the specified resulting set.
	/// </summary>
	public interface ISearchableService<T, TSpecification>		
		where TSpecification : ISpecification
	{
		PagedResult<T> Find(TSpecification specification);		
	}

}
