using CMS.DataEngine;
using CMS.DocumentEngine;
using CMS.Taxonomy;
using Launchpad.Core.Constants;
using Launchpad.Core.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml.Linq;

namespace Launchpad.Infrastructure.Extensions
{


	public static class DataQueryBaseExtensions
	{
		public static DataQueryBase<TQuery> UrlPath<TQuery>(this DataQueryBase<TQuery> query, string path, PathTypeEnum pageTypeEnum = PathTypeEnum.Explicit)
			where TQuery : DataQueryBase<TQuery>, new()
		{
			// Add the path as a SQL Inject projected parameter
			query.EnsureParameters();
			query.Parameters.Add("@UrlPath", path, true);


			// Set the subquery against the DocumentUrlPath view (Alias NodeID to not conflict with Kentico's own same name column)
			string subquery = @"
				SELECT NodeID AS PathNodeID, DocumentUrlPath
				FROM View_Custom_DocumentUrlPath
			";


			// Path comparison
			switch (pageTypeEnum)
			{
				case PathTypeEnum.Explicit:
				case PathTypeEnum.Single:
					query.Source(qs => qs.InnerJoin($"({subquery}) AS PathQuery", "SubData.NodeID = PathQuery.PathNodeID AND PathQuery.DocumentUrlPath = @UrlPath"));
					break;
				case PathTypeEnum.Children:
					query.Source(qs => qs.InnerJoin($"({subquery}) AS PathQuery", "SubData.NodeID = PathQuery.PathNodeID AND PathQuery.DocumentUrlPath LIKE @UrlPath + '%'"));
					break;
				case PathTypeEnum.Section:
					query.Source(qs => qs.InnerJoin($"({subquery}) AS PathQuery", "SubData.NodeID = PathQuery.PathNodeID AND (PathQuery.DocumentUrlPath = @UrlPath OR PathQuery.DocumentUrlPath LIKE @UrlPath + '%')"));
					break;
				default:
					break;
			}
			return query;
		}


		/// <summary>
		/// Adds a Common Table Expression (CTE) to a query.
		/// </summary>
		public static DataQueryBase<TQuery> WithCte<TQuery>(this DataQueryBase<TQuery> query, IDataQuery cteQuery, string cteAlias = "cte")
			where TQuery : DataQueryBase<TQuery>, new()
		{
			if (cteQuery == null)
			{
				return query;
			}


			// Set the CTE to appear before the query
			query.EnsureParameters();


			// Check to see if a CTE has already been added
			if (!query.Parameters.QueryBefore.Contains("WITH"))
			{
				query.Parameters.QueryBefore += "; WITH ";
			}
			else
			{
				query.Parameters.QueryBefore += ", ";
			}


			query.Parameters.QueryBefore += $"{cteAlias} AS ( {cteQuery} )\n\n";


			return query;
		}

		/// <summary>
		/// Useful utility that will trim the necessary columns coming from an object query
		/// Uses NodeID as the default column
		/// </summary>		
		public static DataQueryBase<TQuery> WithRequiredColumns<TQuery>(this DataQueryBase<TQuery> query, string[] columns = null)
			where TQuery : DataQueryBase<TQuery>, new()
		{
			// Defining a column ensures that no additional columns are returned
			if (!query.SelectColumnsList.Any())
			{
				query.Column(nameof(TreeNode.NodeID));
			}

			if (columns == null || !columns.Any())
			{
				return query;
			}

			// Add the additional columns
			query.AddColumns(columns);

			return query;
		}

		/// <summary>
		/// Useful utility to ensure that the columns used to populate a PageNode are added to the Object Query
		/// </summary>		
		public static DataQueryBase<TQuery> WithPageNodeColumns<TQuery>(this DataQueryBase<TQuery> query)
			where TQuery : DataQueryBase<TQuery>, new()
		{
			// Defining a column ensures that no additional columns are returned
			query.WithRequiredColumns(
					new string[]{
						nameof(TreeNode.DocumentName),
						nameof(TreeNode.DocumentID),
						nameof(TreeNode.NodeACLID),
						nameof(TreeNode.NodeAliasPath),
						nameof(TreeNode.ClassName),
						nameof(TreeNode.NodeGUID),
						nameof(TreeNode.NodeID),
						nameof(TreeNode.NodeLevel),
						nameof(TreeNode.NodeOrder) ,
						nameof(TreeNode.NodeParentID),
						nameof(TreeNode.NodeSiteID),
						nameof(TreeNode.DocumentPageDescription),
						nameof(TreeNode.DocumentPageTitle),
						//nameof(TreeNode.DocumentUrlPath),
						nameof(TreeNode.NodeAliasPath),
						nameof(TreeNode.DocumentCustomData),
						nameof(TreeNode.DocumentIsArchived),
				}
			);

			return query;
		}

		/// <summary>
		/// Uses CTE to join the Document Category Table and Category Table into the Query
		/// and returns a Comma Delimited String of the CategoryName, CategoryDisplayName, CategoryCodeNamePath for each DocumentID
		/// Implementation of the CodeNamePath makes easier use for verifying // filtering by categories.
		/// Parent Categories are not stored by default, so the path makes it easy to verify parent categories
		/// Also InEnabledCategories is only allowed to be called once, so AND operations on multiple categorie filters needed a custom path implementation
		/// </summary>
		/// <param name="query"></param>
		/// <returns></returns>
		public static DataQueryBase<TQuery> WithCategories<TQuery>(this DataQueryBase<TQuery> query)
			where TQuery : DataQueryBase<TQuery>, new()
		{
			var hcq = $@"
				SELECT C.*, CategoryCodeNamePath 
				FROM CMS_Category C
				INNER JOIN
				(
					SELECT HCQ.HCID, '/' + STRING_AGG(CategoryName, '/') + '/' as CategoryCodeNamePath 
					FROM CMS_Category
					INNER JOIN
					(
						SELECT CategoryID as 'HCID', CAST(value as int) as 'HCIDPV' 
						FROM CMS_Category
						CROSS APPLY STRING_SPLIT(CategoryIDPath, '/') 
					) as HCQ
					ON CategoryID = HCQ.HCIDPV
					WHERE HCQ.HCIDPV !=0
				GROUP BY HCQ.HCID) AS PHCQ
				ON PHCQ.HCID = CategoryID
			";

			var categoryQuery = DocumentCategoryInfo.Provider.Get()
			.Columns(new string[] {
				$"STRING_AGG(HCQ.{nameof(CategoryInfo.CategoryName)}, '{Delimeter}') AS {nameof(PageNode.CategoryNames)}",
				$"STRING_AGG(HCQ.{nameof(CategoryInfo.CategoryDisplayName)}, '{Delimeter}') AS {nameof(PageNode.CategoryDisplayNames)}",
				$"STRING_AGG(CategoryCodeNamePath, '{Delimeter}') AS {nameof(PageNode.CategoryCodeNamePaths)}"
			})
			.AddColumns(
				new QueryColumn(nameof(TreeNode.DocumentID)).As("CategoryDocumentID")
			)
			.Source(qs => qs.InnerJoin<CategoryInfo>(nameof(DocumentCategoryInfo.CategoryID), nameof(CategoryInfo.CategoryID)))
			.Source(qs => qs.InnerJoin($"({hcq}) as HCQ", "[CMS_DocumentCategory].[CategoryID] = HCQ.CategoryID"))
			.GroupBy(nameof(TreeNode.DocumentID));

			query
				.Source(qs => qs.LeftJoin($"({categoryQuery}) AS CategoryQuery", "DocumentID = CategoryQuery.CategoryDocumentID"));

			// For Document Query - we need to add the required columns
			// For Multi Document, the query generated auto joins the columns.
			// If we call it on the multi document, it adds it to the typed subqueries which don't have the category subquery.
			if (!query.GetType().Equals(typeof(MultiDocumentQuery)))
			{
				query.WithRequiredColumns(
					new string[]{
						nameof(PageNode.CategoryNames),
						nameof(PageNode.CategoryDisplayNames),
						nameof(PageNode.CategoryCodeNamePaths),
					}
				);
			}


			return query;
		}

		/// <summary>		
		/// Takes a MultipleDocumentQuery and uses the query's Result (Not the Typed Result) to access the resulting DataSet.
		/// The DataSet will contain all the columns of the query including any custom column definitions.
		/// This DataSet is converted to a PageNode with any additional columns inserted into the PageNode's Fields.
		/// </summary>
		/// <param name="query"></param>
		/// <returns></returns>
		public static IEnumerable<PageNode> ToPageNodes<TQuery>(this DataQueryBase<TQuery> query)
			where TQuery : DataQueryBase<TQuery>, new()

		{
			var pageNodes = new List<PageNode>();
			var dataSet = query.Result;
			var dataTable = dataSet.Tables[0];

			foreach (DataRow x in dataTable.Rows)
			{

				// Fill custom properties
				var fields = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);

				foreach (DataColumn dc in dataTable.Columns)
				{
					fields.Add(dc.ColumnName, x[dc.ColumnName]);
				}

				// There may be instances where the query has defined columns and does have the columns below
				// Better Error Handling
				// Separated for easier debugging
				var documentName = (fields.ContainsKey(nameof(TreeNode.DocumentName)) ? fields[nameof(TreeNode.DocumentName)].ToString() : "");
				var documentID = (int)(fields.ContainsKey(nameof(TreeNode.DocumentID)) ? fields[nameof(TreeNode.DocumentID)] : 0);
				var documentCustomData = x.GetDocumentCustomData();
				var documentUrlPath = documentCustomData.GetStringValue(Constants.DocumentUrlPath);
				//var documentUrlPath = (fields.ContainsKey(nameof(TreeNode.DocumentUrlPath)) ? fields[nameof(TreeNode.DocumentUrlPath)].ToString() : "");
				var nodeAclID = (int)(fields.ContainsKey(nameof(TreeNode.NodeACLID)) ? fields[nameof(TreeNode.NodeACLID)] : 0);
				var nodeAliasPath = (fields.ContainsKey(nameof(TreeNode.NodeAliasPath)) ? fields[nameof(TreeNode.NodeAliasPath)].ToString() : "");
				var nodeClassName = (fields.ContainsKey(nameof(TreeNode.ClassName)) ? fields[nameof(TreeNode.ClassName)].ToString() : "");
				var nodeGuid = (Guid)(fields.ContainsKey(nameof(TreeNode.NodeGUID)) ? fields[nameof(TreeNode.NodeGUID)] : null);
				var nodeID = (int)(fields.ContainsKey(nameof(TreeNode.NodeID)) ? fields[nameof(TreeNode.NodeID)] : 0);
				var nodeLevel = (int)(fields.ContainsKey(nameof(TreeNode.NodeLevel)) ? fields[nameof(TreeNode.NodeLevel)] : 0);
				var nodeOrder = (int)(fields.ContainsKey(nameof(TreeNode.NodeOrder)) ? fields[nameof(TreeNode.NodeOrder)] : 0);
				var nodeParentID = (int)(fields.ContainsKey(nameof(TreeNode.NodeParentID)) ? fields[nameof(TreeNode.NodeParentID)] : 0);
				var nodeSiteID = (int)(fields.ContainsKey(nameof(TreeNode.NodeSiteID)) ? fields[nameof(TreeNode.NodeSiteID)] : 0);
				var categoryNames = (fields.ContainsKey(nameof(PageNode.CategoryNames)) ? fields[nameof(PageNode.CategoryNames)].ToString() : "");
				var categoryDisplayNames = (fields.ContainsKey(nameof(PageNode.CategoryDisplayNames)) ? fields[nameof(PageNode.CategoryDisplayNames)].ToString() : "");
				var categoryCodeNamePaths = (fields.ContainsKey(nameof(PageNode.CategoryCodeNamePaths)) ? fields[nameof(PageNode.CategoryCodeNamePaths)].ToString() : "");
				var featuredOrder = (int)(fields.ContainsKey(nameof(PageNode.FeatureOrder)) ? fields[nameof(PageNode.FeatureOrder)] : 0);

				double relatedRating = 0;
				if (fields.ContainsKey(nameof(PageNode.RelatedRating)) && fields[nameof(PageNode.RelatedRating)] != null)
				{
					double.TryParse(fields[nameof(PageNode.RelatedRating)].ToString(), out relatedRating);
				}
				var isContentOnly = false;
				if (fields.ContainsKey(nameof(PageNode.IsContentOnly)) && fields[nameof(PageNode.IsContentOnly)] != null)
				{
					bool.TryParse(fields[nameof(PageNode.IsContentOnly)].ToString(), out isContentOnly);
				}

				PageNode pageNode = new PageNode
				{
					DocumentName = documentName,
					DocumentID = documentID,
					DocumentUrlPath = documentUrlPath,
					NodeAliasPath = nodeAliasPath,
					NodeClassName = nodeClassName,
					NodeGuid = nodeGuid,
					NodeID = nodeID,
					NodeLevel = nodeLevel,
					NodeOrder = nodeOrder,
					NodeParentID = nodeParentID,
					NodeSiteID = nodeSiteID,
					CategoryNames = categoryNames.Split(new string[] { Delimeter }, StringSplitOptions.RemoveEmptyEntries),
					CategoryDisplayNames = categoryDisplayNames.Split(new string[] { Delimeter }, StringSplitOptions.RemoveEmptyEntries),
					CategoryCodeNamePaths = categoryCodeNamePaths.Split(new string[] { Delimeter }, StringSplitOptions.RemoveEmptyEntries),
					FeatureOrder = featuredOrder,
					RelatedRating = relatedRating,
					IsContentOnly = isContentOnly,
					CustomData = documentCustomData,
				};

				var description = (fields.ContainsKey(nameof(TreeNode.DocumentPageDescription)) ? fields[nameof(TreeNode.DocumentPageDescription)].ToString() : "");
				var title = (fields.ContainsKey(nameof(TreeNode.DocumentPageTitle)) ? fields[nameof(TreeNode.DocumentPageTitle)].ToString() : "");
				var url = (fields.ContainsKey(nameof(TreeNode.NodeAliasPath)) ? fields[nameof(TreeNode.NodeAliasPath)].ToString() : "");

				// Fill metadata
				pageNode.Metadata = new PageMetadata
				{
					Description = description,
					Title = title,
					Url = url
				};

				// Add the fields
				pageNode.Fields = fields;

				pageNodes.Add(pageNode);
			}
			return pageNodes;
		}

		/// <summary>
		/// This can be called directly only when working with Single Document Query, when using MultiDocumentQuery, call it using the Multi Extension
		/// Default Distinct does not work as expected when used on Multi Typed or Joined Content as it restricts the column to one property and this typically breask subqueries
		/// Instead we implement our own version of GetDistinct which removes Order By and then returns specified Column on the overal query
		/// Custom Cross Apply Implementation for Delimited Categories
		/// </summary>		
		public static IEnumerable<object> GetDistinct<TQuery>(this DataQueryBase<TQuery> query, string column)
			where TQuery : DataQueryBase<TQuery>, new()
		{
			List<object> result = new List<object>();

			try
			{
				query.EnsureParameters();
				query.OrderByColumns = "";
				var categoryQueryAfter = "";
				var distinctColumn = "";
				if (CategoryFields.Contains(column))
				{
					distinctColumn = $"{column}.value AS {column}";
					categoryQueryAfter += $"CROSS APPLY STRING_SPLIT({column}, '{Delimeter}') AS {column}";
				}
				else
				{
					distinctColumn = column;
				}
				query.Parameters.QueryBefore = $"SELECT DISTINCT {distinctColumn} FROM (" + query.Parameters.QueryBefore;
				query.Parameters.QueryAfter = query.Parameters.QueryAfter + ") AS RESULT " + categoryQueryAfter;

				var dataSet = query.Result;
				var dataTable = dataSet.Tables[0];


				foreach (DataRow x in dataTable.Rows)
				{
					result.Add(x[column]);
				}
			}
			catch (Exception)
			{
				// TODO LOG
				// Error occurs here due to a SQL Exception
				// This Object Query Implementation may be prone to issues in writing properly syntaxed SQL				
			}


			return result;
		}

		/// <summary>
		/// This should not be called directly, instead -- call it from the Document Extension or Multi Document Extension to incorporate correct sorting.
		/// Takes an array of NodeGuids and orders the query based on these the order they appear.
		/// </summary>		
		public static DataQueryBase<TQuery> WithFeatured<TQuery>(this DataQueryBase<TQuery> query, string[] featuredNodeGuids)
			where TQuery : DataQueryBase<TQuery>, new()
		{
			if (featuredNodeGuids == null || !featuredNodeGuids.Any())
			{
				return query;
			}

			var caseStatement = "";
			for (var i = 0; i < featuredNodeGuids.Length; i++)
			{
				caseStatement += $"WHEN {nameof(PageNode.NodeGuid)} = '{featuredNodeGuids[i]}' THEN {featuredNodeGuids.Length - i}" + Environment.NewLine;
			}
			query.WithRequiredColumns(
				new string[]{
					new QueryColumn(
						$@"
							CASE 
								{caseStatement}
								ELSE 0
							END
						"
					).As(nameof(PageNode.FeatureOrder)).ToString()
				}
			);

			if (!query.GetType().Equals(typeof(MultiDocumentQuery)))
			{
				query.OrderByDescending(nameof(PageNode.FeatureOrder)); // Breaks on MultiDocumentQuery
			}
			return query;
		}

		public const string Delimeter = "|";
		public static string[] CategoryFields = new string[]{
			nameof(PageNode.CategoryNames),
			nameof(PageNode.CategoryDisplayNames),
			nameof(PageNode.CategoryCodeNamePaths)
		};

		public static Hashtable GetDocumentCustomData(this DataRow row)
		{
			var documentCustomDataXmlString = row.Field<string>(nameof(TreeNode.DocumentCustomData));
			var documentCustomData = new Dictionary<string, string>();
			if (!string.IsNullOrWhiteSpace(documentCustomDataXmlString))
			{
				try
				{
					XDocument doc = XDocument.Parse(documentCustomDataXmlString);
					Dictionary<string, string> result = (from element in doc.Descendants() select new KeyValuePair<string, string>(element.Name.ToString(), element.Value)).ToDictionary(element => element.Key, element => element.Value);
					documentCustomData = result;
				}
				catch (Exception)
				{

				}
			}

			return new Hashtable(documentCustomData);
		}
	}

}
