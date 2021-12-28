using CMS.DataEngine;
using CMS.DocumentEngine;
using Launchpad.Core.Abstractions.Specifications;
using Launchpad.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launchpad.Infrastructure.Extensions.Query
{
	public static class SearchTermQueryExtensions
	{
		public static DocumentQuery<T> ApplySearchTermSpecification<T>(this DocumentQuery<T> query, ISearchTermSpecification specification, string[] searchableProperties)
			where T : TreeNode, new()
		{
			if (specification == null || searchableProperties == null)
			{
				return query;
			}

			// query.WhereAnyColumnContains did not provide the expected result
			// I believe it only searches the Document / Node Propeties only...

			string terms = specification.SearchTerm?.Trim();

			if (!String.IsNullOrWhiteSpace(terms))
			{
				foreach (string term in terms.Split(' '))
				{
					var whereCondition = new WhereCondition();
					foreach (var column in searchableProperties)
					{
						whereCondition.Or().WhereContains(column, term);
					}
					query.And(
						whereCondition
					);
				}
			}

			return query;
		}

		public static MultiDocumentQuery ApplySearchTermSpecification(this MultiDocumentQuery query, ISearchTermSpecification specification, string[] searchableProperties)
		{
			if (specification == null || searchableProperties == null)
			{
				return query;
			}

			if (string.IsNullOrWhiteSpace(specification.SearchTerm))
			{
				return query;
			}

			// This does not appear to be supported for MultipleDocumentQuery -> Adds 1=0 to Where Query
			/*
			// Document Name filter
			string terms = documentSpecification.SearchTerm?.Trim();

			if (!String.IsNullOrWhiteSpace(terms))
			{
				foreach (string term in terms.Split(' '))
				{
					query.WhereAnyColumnContains(term);					
				}
			}
			*/

			string terms = specification.SearchTerm?.Trim();

			if (!String.IsNullOrWhiteSpace(terms))
			{
				// Typically we can distingush between keyword exact or relevant search with AND / Or term matching.
				// UX has decided that keyword searching on listing page should only be AND exact term matching
				// as a user would would expect the number of results to narrow when adding additional search terms, not increase
				
				/*
				bool exactSearch = false;
				if (terms.StartsWith("\"") & terms.EndsWith("\""))
				{
					exactSearch = true;
				}
				*/

				// exact search is always true in this scenario
				bool exactSearch = true;


				terms = terms.Replace('"',' ').Trim().ToLower();
				var searchTerms = terms.Split(' ');
				var searchTermCondition = new WhereCondition();
				foreach (string term in searchTerms)
				{
					var whereCondition = new WhereCondition();
					foreach (var searchableProperty in searchableProperties)
					{
						whereCondition.Or().WhereContains(searchableProperty, term);
					}
					if (exactSearch)
					{
						searchTermCondition.And(whereCondition);
					}
					else
					{
						searchTermCondition.Or(whereCondition);
					}
				}
				query.And(searchTermCondition);
			}

			return query;

		}

		public static IEnumerable<T> ApplySearchTermSpecification<T>(this IEnumerable<T> result, ISearchTermSpecification specification, string[] searchableProperties = null)
			where T : PageNode, new()
		{
			if (specification == null)
			{
				return result;
			}

			// TODO Searchable Properties currently not applied,
			// Providing a null property will continue to search all properties...

			// Search term filter			
			string terms = specification.SearchTerm?.Trim();
			if (!string.IsNullOrEmpty(terms))
			{
				var keywords = terms.ToLower().Split(' ');
				result = result.Where(x =>
				{
					return keywords.All(k =>
					{
						return x.Fields.Any(kvp =>
						{
							if (kvp.Value != null)
							{
								return kvp.Value.ToString().Contains(k);
							}
							return false;
						});
					});
				});
			}
			return result;
		}
	}
}
