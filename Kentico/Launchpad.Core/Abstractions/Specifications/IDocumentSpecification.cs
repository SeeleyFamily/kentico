


using System;

namespace Launchpad.Core.Abstractions.Specifications
{

	/// <summary>
	/// An interface defining typical document filtering specifications. Includes <see cref="IPagedSpecification"/> specifications
	/// for paging.
	/// </summary>
	public interface IDocumentSpecification : ISpecification,		
		ICategoriesSpecification,
		IClassNameSpecification,
		IPathSpecification,
		ISearchTermSpecification,
		IPagedSpecification,
		ISortSpecification
	{
		int[] ExcludedNodes { get; set; }
		int[] Nodes { get; set; }
		Guid[] ExcludedGuids { get; set; }
		Guid[] Guids { get; set; }
		int NodeLevel { get; set; }
		int NestingLevel { get; set; }
	}

}
