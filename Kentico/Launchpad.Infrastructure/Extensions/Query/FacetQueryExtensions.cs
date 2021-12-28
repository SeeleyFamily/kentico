using CMS.DocumentEngine;
using Launchpad.Core.Abstractions.Specifications;
using Launchpad.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Launchpad.Infrastructure.Extensions.Query
{
	[Obsolete( "This class is being marked for elimination, as its performance is likely unacceptable." )]
	public static class FacetQueryExtensions
	{
		[Obsolete( "This method is being marked for elimination, as its performance is likely unacceptable." )]
		public static IEnumerable<Facet> GetFacets<T, TSpecification>(TSpecification specification, Func<TSpecification, DocumentQuery<T>> GetDocumentQuery)
		where T : TreeNode, new()
		where TSpecification : IFacetSpecification, new()
		{
			var facets = new List<Facet>();
			var facetSpecification = new TSpecification()
			{
				Path = specification.Path
			};
			if (specification != null && specification.Facets != null && specification.Facets.Any())
			{
				foreach (var facet in specification.Facets)
				{
					var query = GetDocumentQuery(specification);
					var distinctValues = query.GetDistinct(facet);
					facets.Add( new Facet()
					{
						Name = facet,
						Values = distinctValues.Select( v => new FacetValue { Value = v, Count = query.Count( q => ( string ) q[facet] == v.ToString() ) } ),
					} );
				}
			}
			return facets;
		}


		[Obsolete( "This method is being marked for elimination, as its performance is likely unacceptable." )]
		public static PagedResult<TResult> WithFacets<T, TResult, TSpecification>(this PagedResult<TResult> pagedResult, TSpecification specification, Func<TSpecification, DocumentQuery<T>> GetDocumentQuery)
		where T : TreeNode, new()
		where TResult : class
		where TSpecification : IFacetSpecification, new()
		{
			if (specification.Facets == null || !specification.Facets.Any())
			{
				return pagedResult;
			}

			var facets = GetFacets(specification, GetDocumentQuery);
			pagedResult.Facets = facets;
			return pagedResult;
		}


		[Obsolete( "This method is being marked for elimination, as its performance is likely unacceptable." )]
		public static PagedResult<T> WithFacetsMultiDocumentQuery<T, TSpecification>(this PagedResult<T> pagedResult, TSpecification specification, Func<TSpecification, MultiDocumentQuery> GetDocumentQuery)		
		where T : class
		where TSpecification : IFacetSpecification, new()
		{
			if (specification.Facets == null || !specification.Facets.Any())
			{
				return pagedResult;
			}
			
			var facets = GetFacets(specification, GetDocumentQuery);
			pagedResult.Facets = facets;			
			return pagedResult;
		}


		[Obsolete( "This method is being marked for elimination, as its performance is likely unacceptable." )]
		public static IEnumerable<Facet> GetFacets<TSpecification>(TSpecification specification, Func<TSpecification, MultiDocumentQuery> GetDocumentQuery)
			where TSpecification : IFacetSpecification, new()
		{
			var facets = new List<Facet>();
			var facetSpecification = new TSpecification()
			{
				Path = specification.Path
			};
			if (specification != null && specification.Facets != null && specification.Facets.Any())
			{
				foreach (var facet in specification.Facets)
				{
					var query = GetDocumentQuery(specification);
					var distinctValues = query.GetMultiDocumentQueryDistinct(facet);
					facets.Add( new Facet()
					{
						Name = facet,
						Values = distinctValues.Select( v => new FacetValue { Value = v, Count = query.Count( q => ( string ) q[facet] == v.ToString() ) } ),
					} );
				}
			}
			return facets;
		}
	}
}
