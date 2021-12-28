using System;
using System.Collections.Generic;
using System.Linq;
using Launchpad.Core.Abstractions.Specifications;
using Launchpad.Core.Models;
using Microsoft.Azure.Search.Models;


namespace Launchpad.Infrastructure.Extensions
{

	public static class AzureSearchExtensions
	{


		public static PagedResult<T2> CreatePagedResult<T, T2>(this DocumentSearchResult<T> searchResult, IPagedSpecification specification, Func<T, T2> mapper)
			where T : class
			where T2 : class, new()
		{
			PagedResult<T2> result = CreatePagedResult<T, T2>(searchResult, specification);

			// Map the result documents to their converted types using mapper
			result.Items = searchResult.Results.Select(i => mapper(i.Document)).ToArray();


			return result;
		}


		/// <summary>
		/// Converts a typed Azure Search <see cref="DocumentSearchResult{T}"/> to a <see cref="PagedResult{T2}"/> container, filling in paging properties, and if provided,
		/// converting search results to their <typeparamref name="T2"/> representation in the <see cref="PagedResult{T2}.Items"/> collection.
		/// </summary>
		public static PagedResult<T2> CreatePagedResult<T, T2>(this DocumentSearchResult<T> searchResult, IPagedSpecification specification, Func<SearchResult<T>, T2> resultConverter = null)
			where T : class
			where T2 : class, new()
		{
			// Create the basic PagedResult
			PagedResult<T2> result = new PagedResult<T2>
			{
				Facets = searchResult.Facets?.Select( ToLaunchpadFacet ).ToArray(),
				PageIndex = specification.PageIndex,
				PageSize = specification.PageSize,
				Specification = specification,
				Total = ( int ) searchResult.Count.GetValueOrDefault()
			};


			// Calculate paging values if necessary
			if (searchResult.Results.Any() && specification.PageSize > 0)
			{
				result.RowStart = Math.Max(0, specification.PageIndex) * specification.PageSize;
				result.RowEnd = result.RowStart + searchResult.Results.Count - 1;
				result.TotalPages = (int)Math.Ceiling((decimal)result.Total / specification.PageSize);
			}


			// Convert the results to PagedResult.Items?
			if (!searchResult.Results.Any())
			{
				result.Items = Enumerable.Empty<T2>().ToArray();
			}
			else if (resultConverter != null)
			{
				result.Items = searchResult.Results.Select(resultConverter).ToArray();
			}


			return result;
		}


		/// <summary>
		/// Sets the <see cref="SearchParameters"/> Take and Skip parameters to match the paging settings in the provided <see cref="IPagedSpecification"/> <paramref name="specification"/>.
		/// </summary>
		public static void SetPagingParameters(this SearchParameters parameters, IPagedSpecification specification)
		{
			if (specification.PageSize <= 0)
			{
				// No paging
				return;
			}

			// Page Size
			parameters.Top = specification.PageSize;


			// Page Index
			if( specification.PageIndex <= 0 )
			{
				return;
			}

			parameters.Skip = specification.PageSize * ( specification.PageIndex );
		}



		private static Facet ToLaunchpadFacet( KeyValuePair<string, IList<FacetResult>> facetResult )
		{
			Facet facet = new Facet
			{
				Name = facetResult.Key,
				Values = facetResult.Value.Select( fv => new FacetValue { Value = fv.Value, Count = fv.Count } ).ToArray()
			};


			return facet;
		}
	}

}