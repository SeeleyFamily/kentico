using CMS.DocumentEngine;
using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Core.Abstractions.Models.Summary;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Extensions;
using Launchpad.Core.Models;
using Launchpad.Core.Models.Summary;
using Launchpad.Core.Specifications;
using Launchpad.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Launchpad.Infrastructure.Services
{
	public class RelatedSummaryService<T> : IRelatedSummaryService<T>, IPerScopeService
		where T: ISummaryItem
	{
		#region Fields
		private readonly IDocumentService documentService;
		private readonly IDocumentQueryConfiguration queryConfiguration;
		private readonly IRelatedService<PageNode> pageNodeRelatedService;
		#endregion

		public RelatedSummaryService
		(
			IDocumentService documentService,
			IDocumentQueryConfiguration queryConfiguration,
			IRelatedService<PageNode> pageNodeRelatedService
		)
		{
			this.documentService = documentService;
			this.queryConfiguration = queryConfiguration;
			this.pageNodeRelatedService = pageNodeRelatedService;
		}

		public IEnumerable<int> GetRelatedDocumentIds(int documentId, int count, string path = "/", string[] includedClassNames = null, string[] excludedCategories = null)
		{
			return GetRelatedDocuments(documentId, count, path, includedClassNames, excludedCategories).Select(x => x.DocumentID);
		}

		public IEnumerable<PageNode> GetRelatedDocuments(int documentId, int count, string path = "/", string[] includedClassNames = null, string[] excludedCategories = null)
		{
			var ratingQueryExcludedCategories = "";
			if (excludedCategories != null && excludedCategories.Length > 0)
			{
				foreach (var category in excludedCategories)
				{
					ratingQueryExcludedCategories = $"{ratingQueryExcludedCategories} AND [CategoryCodeNamePath] NOT LIKE '%{category}%'";
				}
			}

			var ratingQuery = $@"
				SELECT 
					DocumentID, SUM({nameof(PageNode.RelatedRating)}) AS {nameof(PageNode.RelatedRating)} 
				FROM
					CTE5
				AS FDC
				INNER JOIN (
                    SELECT HCIDPV,RelatedRating
					FROM CMS_DocumentCategory INNER JOIN CTE4 ON CategoryID = HCID
					WHERE [DocumentID] = {documentId} {ratingQueryExcludedCategories}
				) as RatingQuery 
				ON RatingQuery.HCIDPV=FDC.HCIDPV
				GROUP BY DocumentID
			";


			var cteSource = $@"
				WITH CTE AS
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
						GROUP BY HCQ.HCID
					),
				CTE2 AS
					(
						SELECT HCQ.HCID,HCIDPV, CategoryLevel, CTE.CategoryCodeNamePath
						FROM CMS_Category
						INNER JOIN
						(
							SELECT CategoryID as 'HCID', CAST(value as int) as 'HCIDPV' 
							FROM CMS_Category
							CROSS APPLY STRING_SPLIT(CategoryIDPath, '/') 
						) as HCQ
						ON CategoryID = HCQ.HCIDPV	
						INNER JOIN CTE
						ON CTE.HCID = HCQ.HCID
						WHERE HCQ.HCIDPV !=0		
					),
				CTE3 AS
					(
						SELECT CTE2.*, ML.MaxCategoryLevel FROM CTE2
						INNER JOIN 
						(SELECT HCID,MAX(CategoryLevel) as MaxCategoryLevel FROM CTE2 GROUP BY HCID) AS ML
						ON CTE2.HCID = ML.HCID
					),
				CTE4 AS
					(
						SELECT 
						CTE3.*,
						(CAST((CategoryLevel + 1) AS FLOAT) / (MaxCategoryLevel + 1)) AS {nameof(PageNode.RelatedRating)}
						FROM CTE3
					),
				CTE5 AS
					(
						SELECT distinct DC.DocumentID,HCIDPV FROM CMS_DocumentCategory DC INNER JOIN CTE4 ON CategoryID = HCID
					),
				CTE6 AS
					(
						{ratingQuery}	
					)
			";

			var documentQuery = DocumentHelper.GetDocuments()
				.WithRequiredColumns(new string[] { nameof(TreeNode.DocumentID), nameof(TreeNode.DocumentCreatedWhen) })
				.WithPageNodeColumns().As<MultiDocumentQuery>()
				.ApplyConfiguration(queryConfiguration)
				.Path(path, PathTypeEnum.Children)
				.WhereNotEquals(nameof(TreeNode.DocumentID), documentId);

			if (includedClassNames != null && includedClassNames.Length > 0)
			{
				documentQuery.WhereIn(nameof(TreeNode.ClassName), includedClassNames);
			}

			var documentCategoryQuery = DocumentCategoryInfo.Provider.Get();

			documentQuery.Source(qs => qs.LeftJoin("CTE6", "CTE6.DocumentID = SubData.DocumentID"));


			documentQuery.EnsureParameters();
			documentQuery.Parameters.QueryBefore = cteSource + documentQuery.Parameters.QueryBefore;

			documentQuery.ResultOrderByDescending(
				new string[]{
					nameof(PageNode.RelatedRating),
					nameof(TreeNode.DocumentCreatedWhen)
				}
			);
			documentQuery.TopN(count);

			return documentQuery.ToPageNodes();
		}

		public IEnumerable<PageNode> GetRelatedDocuments(int nodeId, RelatedSpecification relatedSpecification, string[] categoryCodeNamePaths = null)
		{
			return pageNodeRelatedService.GetRelatedDocuments(nodeId, relatedSpecification, categoryCodeNamePaths);
		}

		public IEnumerable<PageNode> GetRelatedDocuments(Guid nodeGuid, RelatedSpecification relatedSpecification, string[] categoryCodeNamePaths = null)
		{
			return pageNodeRelatedService.GetRelatedDocuments(nodeGuid, relatedSpecification, categoryCodeNamePaths);
		}

		public IEnumerable<PageNode> GetRelatedDocuments(PageNode pageNode, RelatedSpecification relatedSpecification, string[] categoryCodeNamePaths = null)
		{
			return pageNodeRelatedService.GetRelatedDocuments(pageNode, relatedSpecification, categoryCodeNamePaths);

		}

		public IEnumerable<T> GetRelatedSummaryItems(int nodeId, RelatedSpecification relatedSpecification, string[] categoryCodeNamePaths = null)
		{
			var relatedPageNodes = GetRelatedDocuments(nodeId, relatedSpecification, categoryCodeNamePaths);
			return ToSummaryItems(relatedPageNodes);
		}

		public IEnumerable<T> GetRelatedSummaryItems(Guid nodeGuid, RelatedSpecification relatedSpecification, string[] categoryCodeNamePaths = null)
		{
			var relatedPageNodes = GetRelatedDocuments(nodeGuid, relatedSpecification, categoryCodeNamePaths);
			return ToSummaryItems(relatedPageNodes);
		}

		public IEnumerable<T> GetRelatedSummaryItems(PageNode pageNode, RelatedSpecification relatedSpecification, string[] categoryCodeNamePaths = null)
		{
			var relatedPageNodes = GetRelatedDocuments(pageNode, relatedSpecification, categoryCodeNamePaths);
			return ToSummaryItems(relatedPageNodes);
		}

		public virtual IEnumerable<T> ToSummaryItems(IEnumerable<PageNode> pageNodes)
		{
			var relatedSummaryItems = pageNodes.Select(x =>
			{
				var previewSummaryItem = x.ToPreviewSummary<SummaryItem>();
				return previewSummaryItem;
			});
			return (IEnumerable<T>)relatedSummaryItems;
		}
	}
}
