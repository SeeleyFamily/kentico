using Launchpad.Core.Abstractions.Specifications;
using Launchpad.Core.Models;

namespace Launchpad.Core.Abstractions.Services
{

	/// <summary>
	/// An interface defining a searchable document service.
	/// T is a generic type with no constraint, the IDocumentSpecification is designed to work off a base model of either TreeNode or PageNode.
	/// </summary>
	public interface ISearchableDocumentService<T, TDocumentSpecification> : ISearchableService<T, TDocumentSpecification>
		where T: class
		where TDocumentSpecification : IDocumentSpecification
	{
		new PagedResult<T> Find(TDocumentSpecification specification);
	}

}
