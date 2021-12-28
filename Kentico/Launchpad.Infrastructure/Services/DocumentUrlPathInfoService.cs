using CMS.DataEngine;
using CMS.Helpers;
using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Models;
using Launchpad.Infrastructure.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Launchpad.Infrastructure.Services
{
	public class DocumentUrlPathInfoService : IDocumentUrlPathInfoService, IPerScopeService
	{
		private readonly ICacheService cacheService;
		private readonly ISiteContextConfiguration siteContextConfiguration;

		public DocumentUrlPathInfoService(ICacheService cacheService, ISiteContextConfiguration siteContextConfiguration)
		{
			this.cacheService = cacheService;
			this.siteContextConfiguration = siteContextConfiguration;
		}

		public DocumentUrlPathInfo Get(string documentUrlPath)
		{
			//var dataQuery = new DataQuery();
			//dataQuery.CustomQueryText = $"SELECT TOP(1) * FROM View_Custom_DocumentUrlPath WHERE DocumentUrlPath  = '{documentUrlPath}';";

			//var result = ExecuteQuery(dataQuery).FirstOrDefault();

			var documentUrlPathInfos = Get();
			var result = documentUrlPathInfos.Where(x => x.DocumentUrlPath == documentUrlPath).FirstOrDefault();

			return result;
		}


		public IEnumerable<DocumentUrlPathInfo> Get()
		{
			IEnumerable<DocumentUrlPathInfo> getDocumentUrlPathInfos(CacheSettings cs)
			{
				var dataQuery = new DataQuery();
				dataQuery.CustomQueryText = $"SELECT * FROM View_Custom_DocumentUrlPath WHERE NodeSiteID = ${siteContextConfiguration.SiteId};";

				var result = ExecuteQuery(dataQuery);

				return result;
			}

			IEnumerable<DocumentUrlPathInfo> documentUrlPathInfos = cacheService.GetFromRotatingCache(
				loadFunction: getDocumentUrlPathInfos,
				cacheKey: $"documenturlpathinfo|all",
				cacheDependencies: new string[] {
					$"node|{siteContextConfiguration.SiteName}|/|childnodes".ToLower()
				},
				alwaysCache: true
			);

			return documentUrlPathInfos;
		}

		private IEnumerable<DocumentUrlPathInfo> ExecuteQuery(DataQuery dataQuery)
		{
			var dataSet = dataQuery.Result;
			var documentUrlPathInfos = dataSet.Tables[0].AsEnumerable().Select(x => Convert(x)).ToList();
			return documentUrlPathInfos;
		}


		private DocumentUrlPathInfo Convert(DataRow x)
		{
			return new DocumentUrlPathInfo()
			{
				DocumentUrlPath = x.Field<string>(nameof(DocumentUrlPathInfo.DocumentUrlPath)),
				NodeGuid = x.Field<Guid>(nameof(DocumentUrlPathInfo.NodeGuid)),
				NodeId = x.Field<int>(nameof(DocumentUrlPathInfo.NodeId)),
			};
		}
	}
}
