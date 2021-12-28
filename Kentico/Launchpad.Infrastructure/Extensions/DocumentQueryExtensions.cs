using CMS.DataEngine;
using CMS.DocumentEngine;
using CMS.DocumentEngine.Types.Common;
using CMS.Membership;
using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Core.Abstractions.Specifications;
using Launchpad.Core.Extensions;
using Launchpad.Core.Models;
using Launchpad.Infrastructure.Extensions.Query;
using System;
using System.Linq;

namespace Launchpad.Infrastructure.Extensions
{


	public static class DocumentQueryExtensions
	{

		/// <summary>
		/// Applies <see cref="IDocumentQueryConfiguration"/> settings to the document query.
		/// </summary>
		public static DocumentQuery ApplyConfiguration(this DocumentQuery query, IDocumentQueryConfiguration configuration)
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
					 .LatestVersion()
					 .WhereEquals(nameof(TreeNode.DocumentIsArchived), false);
				
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


		/// <summary>
		/// Applies <see cref="IDocumentQueryConfiguration"/> settings to the document query.
		/// </summary>
		public static DocumentQuery<T> ApplyConfiguration<T>(this DocumentQuery<T> query, IDocumentQueryConfiguration configuration)
			where T : TreeNode, new()
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


		public static DocumentQuery<T> ApplyDocumentSpecification<T>(this DocumentQuery<T> query, IDocumentSpecification documentSpecification)
			where T : TreeNode, new()
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

			// TODO verify this implementation of search term specification
			//query.ApplySearchTermSpecification(documentSpecification);


			// This searches the columns in View_CMS_Tree_Joined rather than the actual columns of the page type
			// Keyword search should be handled in individual specifications
			/*
			// Document Name filter
			string terms = documentSpecification.SearchTerm?.Trim();

			if( !String.IsNullOrWhiteSpace( terms ) )
			{
				foreach( string term in terms.Split( ' ' ) )
				{
					query.WhereAnyColumnContains( term );
				}
			}
			*/


			return query;
		}


		public static DocumentQuery<T> ApplyLocationSpecification<T>(this DocumentQuery<T> query, ILocationSpecification specification)
			where T : TreeNode, new()
		{
			if (specification.CountryId > 0)
			{
				query.WhereEquals(nameof(specification.CountryId), specification.CountryId);
			}
			else if (!String.IsNullOrWhiteSpace(specification.CountryTwoLetterCode))
			{
				query.WhereEquals(nameof(specification.CountryTwoLetterCode), specification.CountryTwoLetterCode);
			}


			if (specification.StateId > 0)
			{
				query.WhereEquals(nameof(specification.StateId), specification.StateId);
			}
			else if (!String.IsNullOrWhiteSpace(specification.State))
			{
				query.WhereEquals(nameof(specification.State), specification.State);
			}


			return query;
		}


		public static DocumentQuery<T> ApplyPagingSpecification<T>(this DocumentQuery<T> query, IPagedSpecification specification)
			where T : TreeNode, new()
		{
			if (specification == null)
			{
				return query;
			}


			// Validate Paged Specification to ensure correct paging parameters
			specification.Validate();  // TODO: This takes page size = 0, which some would desire to mean "no limit" and changes it to "1", which starts returning pages of one record per page, certainly an unexpected result

			if (specification.PageIndex >= 0 && specification.PageSize > 0)
			{
				// Set query to page
				query.Page(specification.PageIndex, specification.PageSize);
			}


			return query;
		}


		/// <summary>
		/// Modifies a <see cref="DocumentQuery"/> to only include content from the <see cref="GlobalContent" /> node.
		/// </summary>
		public static DocumentQuery<T> FromGlobalContent<T>(this DocumentQuery<T> query, IDocumentQueryConfiguration configuration)
			where T : TreeNode, new()
		{
			// Add the inner join to the global content folder
			IDataQuery globalQuery = new DocumentQuery<GlobalContent>()
											.TopN(1)
											.Column("NodeAliasPath as GlobalNodeAliasPath")
											.ApplyConfiguration(configuration)
											.AsSubQuery();

			query.Source(s => s.InnerJoin($"({globalQuery.QueryText}) Global", $"V.NodeAliasPath LIKE Global.GlobalNodeAliasPath + '%'"));


			// Apply configuration
			query.ApplyConfiguration(configuration);

			return query;
		}


		/// <summary>
		/// Takes a DocumentQuery of CMS Type and Paged Specifications and returns a Paged Result set of the type
		/// Useful when only using the default cms page type fields.
		/// When adding additional columns, the alternate ToPagedResult must be used 
		/// </summary>		
		public static PagedResult<T> ToPagedResult<T>(this DocumentQuery<T> query, IPagedSpecification pagedSpecification)
			where T : TreeNode, new()
		{
			query.ApplyPagingSpecification(pagedSpecification);

			// Execute the query and return its results so we have paging data in the query object
			T[] results = query.TypedResult.ToArray();

			// Return Paged Result
			return new PagedResult<T>(results, query.TotalRecords, pagedSpecification);
		}


		/// <summary>
		/// This version of ToPagedResults requires a ToModelFunction to handle the conversion from T1 to T2
		/// Use this method when adding additional fields to the document query as they will be injected in the PageNodes Fields Property
		/// </summary>		
		public static PagedResult<T2> ToPagedResult<T, T2>(this DocumentQuery<T> query, IPagedSpecification pagedSpecification, Func<PageNode, T2> ToModelFunction)
			where T : TreeNode, new()
			where T2 : PageNode, new()
		{
			query.ApplyPagingSpecification(pagedSpecification);

			// Execute the query and return its results so we have paging data in the query object			
			var pageNodeResult = query.ToPageNodes();
			var tResult = pageNodeResult.Select(x => ToModelFunction(x));
			T2[] results = tResult.ToArray();

			// Return Paged Result
			return new PagedResult<T2>(results, query.TotalRecords, pagedSpecification);
		}


		/// <summary>
		/// Adds all the relevent Page Type Columns to the Single Source Document Query
		/// </summary>		
		public static DocumentQuery<T> WithAllPageTypeColumns<T>(this DocumentQuery<T> query)
		where T : TreeNode, new()
		{
			query.WithRequiredColumns(
					new string[]
					{
						"C.*" // This is standard join naming convention for Kentico with TABLE C being the class Page Type and V being the View for Tree Joined
					}
				);
			return query;
		}

		/// <summary>
		/// Adds all the relevent Page Type Columns to the Single Source Document Query
		/// </summary>		
		public static DocumentQuery<T> WithPageTypeColumnsOnly<T>(this DocumentQuery<T> query)
			where T : TreeNode, new()
		{
			var pageTypeColumns = new T().ColumnNames.Except(TreeNode.New().ColumnNames).ToArray();			
			query = query.WithRequiredColumns(pageTypeColumns).WithPageNodeColumns().As<DocumentQuery<T>>();
			return query;
		}

		public static DocumentQuery<T> WhereUserIsAllowed<T>(this DocumentQuery<T> query)
				where T : TreeNode, new()
		{
			return WhereUserIsAllowed(query, MembershipContext.AuthenticatedUser);
		}


		public static DocumentQuery<T> WhereUserIsAllowed<T>(this DocumentQuery<T> query, UserInfo userInfo)
			where T : TreeNode, new()
		{
			// Join the query against ACL Items for the nodes and the user
			query.Source(qs => qs.LeftJoin("View_Custom_Acl_Items_Expanded UserAcl", $"UserAcl.ACLID = V.NodeACLID AND UserAcl.UserID = {userInfo.UserID}"));

			// Establish WHERE clauses to exclude denied items
			query.Where("( UserAcl.Denied IS NULL OR UserAcl.Denied = 0 )");

			// Establish WHERE clauses to include allowed items
			WhereCondition allowedConditions = new WhereCondition();
			allowedConditions.Where("UserAcl.Allowed = 1");
			allowedConditions.Or();

			DataQuery allowedRoles = new DataQuery().From("View_Custom_Acl_Items_Expanded Item")
													.Column("*")
													.Source(qs => qs.InnerJoin("CMS_UserRole UserRole", $"Item.RoleID = UserRole.RoleID AND UserRole.UserID = {userInfo.UserID}"))
													.Where("Item.ACLID = V.NodeACLID")
													.Where("Item.Allowed = 1");

			allowedConditions.Where($"EXISTS ( {allowedRoles.QueryText} ) ");

			query.Where(allowedConditions);


			return query;
		}

	}
}
