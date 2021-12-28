using CMS.DocumentEngine;
using CMS.Helpers;
using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Extensions;
using Launchpad.Core.Models;
using Launchpad.Core.Specifications;
using Launchpad.Infrastructure.Abstractions.Services;
using Launchpad.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Launchpad.Infrastructure.Services
{
	public class PreviewService : IPreviewService, IPerScopeService
	{
		#region Fields
		private readonly ICacheService cacheService;
		private readonly ICategoryService categoryService;
		private readonly IDocumentService documentService;
		private readonly IDocumentQueryConfiguration queryConfiguration;
		#endregion

		#region Properties
		public List<string> PreviewablePageTypes { get; set; } = new List<string>();
		public string PreviewablePageTypesString { get; set; }
		#endregion

		public PreviewService
		(
			ICacheService cacheService,
			ICategoryService categoryService,
			IDocumentService documentService,
			IDocumentQueryConfiguration queryConfiguration
		)
		{
			this.cacheService = cacheService;
			this.categoryService = categoryService;
			this.documentService = documentService;
			this.queryConfiguration = queryConfiguration;

			var previewablePageTypesAppSetting = ConfigurationManager.AppSettings.GetStringValue("PreviewablePageTypes");
			PreviewablePageTypesString = previewablePageTypesAppSetting;
			if (!string.IsNullOrWhiteSpace(previewablePageTypesAppSetting))
			{
				PreviewablePageTypes = previewablePageTypesAppSetting.Split(',').ToList();
			}
		}

		public IEnumerable<PageNode> GetPreviewNodesFromDocumentQuery()
		{
			IEnumerable<PageNode> loadFunction(CacheSettings cacheSettings)
			{
				var previewQuery = DocumentHelper.GetDocuments()
					.Path("/", PathTypeEnum.Children)
					.WithPageNodeColumns().As<MultiDocumentQuery>()
					.WithCategories().As<MultiDocumentQuery>()
					.ApplyConfiguration(queryConfiguration);

				var classNames = PreviewablePageTypesString;
				if (!string.IsNullOrWhiteSpace(classNames))
				{
					var classNamesList = classNames.Split(',').Join("','");
					previewQuery = previewQuery.Where($"ClassName in ('{ classNamesList }')");
				}

				var previewNodes = previewQuery.ToPageNodes();

				return previewNodes;
			}

			return cacheService.GetFromRotatingCache(
				loadFunction,
				"previewNodes|documentquery|all",
				new string[]{
					$"node|{queryConfiguration.SiteName}|/|childnodes".ToLower(),
				},
				alwaysCache: true
				);
		}

		public IEnumerable<PageNode> GetPreviewNodesFromDocumentService()
		{
			IEnumerable<PageNode> loadFunction(CacheSettings cacheSettings)
			{
				var specification = new DocumentSpecification()
				{
					Path = "/",
					PageSize = int.MaxValue,
					ClassNames = PreviewablePageTypes.ToArray(),
				};
				var result = documentService.Find(specification);
				List<PageNode> previewNodes = result.Items.ToList();

				previewNodes = previewNodes.Where(x => PreviewablePageTypes.Any(y => y.Equals(x.NodeClassName, StringComparison.InvariantCultureIgnoreCase))).ToList();

				// FALLBACK IF CATEGORIES ARE MISSING
				previewNodes.ForEach(x =>
				{
					var pageNodeCategories = categoryService.GetDocumentCategories(x);
					x.Categories = pageNodeCategories;
					if (x.CategoryCodeNamePaths == null)
					{
						x.CategoryCodeNamePaths = pageNodeCategories.Select(y => y.CodeNamePath);
					}

				});
				return previewNodes;
			}

			return cacheService.GetFromRotatingCache(
				loadFunction,
				"previewNodes|documentservice|all",
				new string[]{
					$"node|{queryConfiguration.SiteName}|/|childnodes".ToLower(),
				},
				alwaysCache: true
				);
		}

		public IEnumerable<PageNode> GetPreviewNodes(bool useDocumentService = true)
		{
			if (PreviewablePageTypes.IsNullOrEmpty())
			{
				return new List<PageNode>();
			}

			if (useDocumentService)
			{
				return GetPreviewNodesFromDocumentService();
			}
			else
			{
				return GetPreviewNodesFromDocumentQuery();
			}
		}
	}
}
