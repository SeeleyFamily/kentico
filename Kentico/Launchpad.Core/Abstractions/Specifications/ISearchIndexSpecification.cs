using System.Collections.Generic;


namespace Launchpad.Core.Abstractions.Specifications
{

	public interface ISearchIndexSpecification : ISpecification,
		IPagedSpecification
	{
		/// <summary>
		///  Gets or sets the list of facet expressions to apply to the search query.
		/// </summary>
		IEnumerable<string> Facets { get; set; }

		/// <summary>
		/// Gets or sets the OData $filter expression to apply to the search query.
		/// </summary>
		string Filter { get; set; }

		string IndexName { get; set; }

		/// <summary>
		/// A full-text search query expression; Use null or "*" to match all documents.
		/// </summary>
		string Query { get; set; }
	}

}
