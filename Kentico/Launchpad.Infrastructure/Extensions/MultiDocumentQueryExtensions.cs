using CMS.DocumentEngine;
using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Core.Abstractions.Specifications;
using Launchpad.Core.Extensions;
using Launchpad.Core.Models;
using Launchpad.Infrastructure.Extensions.Query;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace Launchpad.Infrastructure.Extensions
{


	public static class MultiDocumentQueryExtensions
	{
		/// <summary>
		/// Applies <see cref="IDocumentQueryConfiguration"/> settings to the document query.
		/// </summary>
		public static MultiDocumentQuery ApplyConfiguration(this MultiDocumentQuery query, IDocumentQueryConfiguration configuration)
		{
			if (configuration == null)
			{
				return query;
			}


			if (!String.IsNullOrWhiteSpace(configuration.Culture))
			{
				query.Culture(configuration.Culture);
			}

			if (configuration.SiteId > 0)
			{
				query.OnSite(configuration.SiteId);
			}


			if (configuration.IsPreview)
			{
				// Preview = LatestVersion, publish state doesn't matter
				query.Published(false)
					 .LatestVersion();

				if (!configuration.IncludeArchived)
				{
					query.WhereEquals(nameof(TreeNode.DocumentIsArchived), false);
				}					 
			}
			else
			{
				// Not preview, latest published version
				query.PublishedVersion()
					 .Published();
			}


			if (configuration.CheckPermissions)
			{
				query.CheckPermissions();
			}


			return query;
		}


		public static MultiDocumentQuery ApplyDocumentSpecification(this MultiDocumentQuery query, IDocumentSpecification documentSpecification)
		{
			if (documentSpecification == null)
			{
				return query;
			}

			query.ApplyNodesSpecification(documentSpecification);
			query.ApplyGuidsSpecification(documentSpecification);
			query.ApplyClassNameSpecification(documentSpecification);
			query.ApplyCategoriesSpecification(documentSpecification);
			query.ApplyNodeLevelSpecification(documentSpecification);
			query.ApplyNestingLevelSpecification(documentSpecification);
			query.ApplyPathSpecification(documentSpecification);


			return query;
		}


		public static MultiDocumentQuery ApplyPagingSpecification( this MultiDocumentQuery query, IPagedSpecification specification )
		{
			if( specification == null )
			{
				return query;
			}


			// Validate Paged Specification to ensure correct paging parameters
			specification.Validate();  // TODO: This takes page size = 0, which some would desire to mean "no limit" and changes it to "1", which starts returning pages of one record per page, certainly an unexpected result

			if( specification.PageIndex >= 0 && specification.PageSize > 0 )
			{
				// Set query to page
				query.Page( specification.PageIndex, specification.PageSize );
			}


			return query;
		}


		public static PagedResult<T> ToPagedResult<T>(this MultiDocumentQuery query, IPagedSpecification pagedSpecification, Func<PageNode,T> ToModelFunction)
		where T : PageNode, new()
		{
			query.ApplyPagingSpecification( pagedSpecification );


			// Execute the query and return its results so we have paging data in the query object			
			var pageNodeResult = query.ToPageNodes();
			var tResult = pageNodeResult.Select(x => ToModelFunction(x));
			T[] results = tResult.ToArray();

			// Return Paged Result
			return new PagedResult<T>(results, query.TotalRecords, pagedSpecification);			
		}


		public static IEnumerable<object> GetMultiDocumentQueryDistinct(this MultiDocumentQuery query, string column)
		{
			// The following line is import for Multi Document Query
			// Removes the Sort // Order By SQL
			query.OrderByResultColumns = "";
			return DataQueryBaseExtensions.GetDistinct(query, column);
		}


		public static MultiDocumentQuery WithFeaturedMulti(this MultiDocumentQuery query, string[] featuredNodeGuids)
		{
			if (featuredNodeGuids == null || !featuredNodeGuids.Any())
			{
				return query;
			}

			query.WithFeatured(featuredNodeGuids).As<MultiDocumentQuery>();
			query.ResultOrderByDescending(nameof(PageNode.FeatureOrder));
			return query;
		}
	}

}
