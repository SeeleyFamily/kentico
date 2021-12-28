using CMS.Helpers;
using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Abstractions.Specifications;
using Launchpad.Core.Extensions;
using Launchpad.Core.Models;
using Launchpad.Core.Models.Summary;
using Launchpad.Core.Specifications;
using Launchpad.Infrastructure.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Launchpad.Infrastructure.Services
{
	public class ChildSummaryService<TSummaryItem, TChildSummarySpecification> :
		IChildSummaryService<TSummaryItem, TChildSummarySpecification>,
		IPerScopeService
		where TSummaryItem : SummaryItem, new()
		where TChildSummarySpecification : IChildSummarySpecification, new()
	{

		#region fields
		private readonly ICacheService cacheService;
		private readonly IDocumentQueryConfiguration queryConfiguration;
		private readonly IDocumentService documentService;

		#endregion

		public ChildSummaryService(
			ICacheService cacheService,
			IDocumentQueryConfiguration queryConfiguration,
			IDocumentService documentService
			)
		{
			this.cacheService = cacheService;
			this.queryConfiguration = queryConfiguration;
			this.documentService = documentService;
		}

		public IEnumerable<TSummaryItem> GetChildSummaryItems(int nodeId, TChildSummarySpecification childSummarySpecification)
		{
			var pageNode = documentService.Get(nodeId);
			return GetChildSummaryItems(pageNode, childSummarySpecification);
		}

		public IEnumerable<TSummaryItem> GetChildSummaryItems(Guid nodeGuid, TChildSummarySpecification childSummarySpecification)
		{
			var pageNode = documentService.Get(nodeGuid);
			return GetChildSummaryItems(pageNode, childSummarySpecification);
		}

		public virtual IEnumerable<TSummaryItem> GetChildSummaryItems(PageNode pageNode, TChildSummarySpecification childSummarySpecification)
		{
			var path = pageNode.NodeAliasPath;
			var levels = childSummarySpecification.NodeLevels;

			IEnumerable<TSummaryItem> loadFunction(CacheSettings cs)
			{
				cs.CacheDependency = CacheHelper.GetCacheDependency(
					new string[]{
						$"node|{queryConfiguration.SiteName}|{path}|childnodes".ToLower(),
						$"node|{queryConfiguration.SiteName}|{path}|childnodes".ToLower()
					}
				);

				var documentSpecification = new DocumentSpecification()
				{
					Path = pageNode.NodeAliasPath,
					ClassNames = childSummarySpecification.ClassNames,
					ExcludedClassNames = childSummarySpecification.ExcludedClassNames,
					NodeLevel = childSummarySpecification.NodeLevels + pageNode.NodeLevel,
					PageSize = childSummarySpecification.PageSize,
					PageIndex = childSummarySpecification.PageIndex,
					Sort = childSummarySpecification.Sort,
				};

				var pageNodes = documentService.Find(documentSpecification).Items.OrderBy(x => x.NodeLevel).ThenBy(x => x.NodeOrder);

				var summaryItems = pageNodes.Select(x => ToSummaryItem(x)).ToList();

				return summaryItems;
			}


			return cacheService.GetFromRotatingCache(loadFunction, $"childSummaryIems|{path.ToLower()}|levels|{levels}");
		}


		public virtual TSummaryItem ToSummaryItem(PageNode pageNode)
		{
			// Allow the preview implementation to handle majority of the assignments here
			// This can be overrided on the subclass per service level..
			TSummaryItem previewSummaryItem = pageNode.ToPreviewSummary<TSummaryItem>();
			return previewSummaryItem;
		}
	}
}
