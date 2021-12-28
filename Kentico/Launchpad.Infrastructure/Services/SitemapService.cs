using CMS.DataEngine;
using CMS.DocumentEngine;
using CMS.DocumentEngine.Types.Common;
using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Configuration;
using Launchpad.Core.Constants;
using Launchpad.Core.Models;
using Launchpad.Infrastructure.Abstractions.Services;
using Launchpad.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;


namespace Launchpad.Infrastructure.Services
{

	/// <summary>
	/// Provides a class to query any type of <see cref="TreeNode"/>. Use this class when
	/// you want to query and return multiple types of documents and return them as <see cref="PageNode"/> objects.
	/// </summary>
	public class SitemapService : ISitemapService, IPerScopeService
	{
		#region Fields
		private readonly Lazy<ISiteService> siteService;
		private readonly XNamespace xmlns = "http://www.sitemaps.org/schemas/sitemap/0.9";
		protected readonly Lazy<ICacheService> cacheService;
		protected readonly IDocumentQueryConfiguration queryConfiguration;
		#endregion


		public SitemapService
		(
			Lazy<ICacheService> cacheService,
			IDocumentQueryConfiguration queryConfiguration,
			Lazy<ISiteService> siteService
		)
		{
			this.siteService = siteService;
			this.cacheService = cacheService;
			this.queryConfiguration = queryConfiguration;
		}


		public XDocument GetSitemap()
		{
			// Retrieve sitemap XML from cache
			return cacheService.Value.GetFromCache((cs) =>
			{
				return CreateSitemapXml(GetSitemapNodes());
			},
			"sitemap.xml|tree");
		}

		/// <summary>
		/// Adds static routes using references from a supplied collection of <see cref="TreeNode"/> objects. For example, the home page root "/" is added using a reference to the <see cref="HomeNode"/> for its DocumentModifiedWhen date.
		/// </summary>
		protected virtual void AddStaticSitemapRoutes(XElement root, string baseUrl, IEnumerable<TreeNode> tree)
		{
			// Add the Home page
			TreeNode homeNode = tree.FirstOrDefault(t => t.ClassName == Home.CLASS_NAME);

			if (homeNode != null)
			{
				root.Add(CreateSitemapNode(baseUrl.TrimEnd('/'), homeNode.DocumentModifiedWhen));
			}
			else
			{
				root.Add(CreateSitemapNode(baseUrl.TrimEnd('/'), DateTime.Now));
			}
		}


		/// <summary>
		/// Creates a <see cref="XNode" /> suitable for a sitemap node using the supplied <see cref="TreeNode"/>.
		/// </summary>
		protected virtual XNode CreateSitemapNode(TreeNode node, string baseUrl)
		{
			Uri uri = new Uri(new Uri(baseUrl), node.DocumentCustomData[ Constants.DocumentUrlPath ]?.ToString());

			return CreateSitemapNode(uri.ToString(), node.DocumentModifiedWhen);
		}


		/// <summary>
		/// Creates a <see cref="XNode" /> suitable for a sitemap node using the supplied relative URL and last modified date.
		/// </summary>
		protected virtual XNode CreateSitemapNode(string url, DateTime lastModified)
		{
			XElement element = new XElement(
				xmlns + "url",
				new XElement(xmlns + "loc", url.ToLower()),
				new XElement(xmlns + "lastmod", lastModified.ToString("yyyy-MM-ddTHH:mm:sszzz"))
			);

			return element;
		}


		/// <summary>
		/// Creates the sitemap <see cref="XDocument"/> xml using the collection of supplied <see cref="TreeNode"/> objects.
		/// </summary>
		protected virtual XDocument CreateSitemapXml(IEnumerable<TreeNode> tree)
		{
			XElement root = new XElement(xmlns + "urlset");


			// Get the presentation URL for the tree's site
			string baseUrl = siteService.Value.GetSite(tree.FirstOrDefault()?.NodeSiteID ?? 1).PresentationUrl; // TODO: Get rid of the magic number here in the event there are no nodes yet; app config value


			AddStaticSitemapRoutes(root, baseUrl, tree);

			// Add the Kentico nodes, minus the home page, which is already added with its root path ("/" instead of whatever node alias its been given, typically "/home")
			foreach (TreeNode node in tree.Where(t => t.ClassName != Home.CLASS_NAME))
			{
				root.Add(CreateSitemapNode(node, baseUrl));
			}

			XDocument document = new XDocument(root);
			return document;
		}


		protected virtual IEnumerable<TreeNode> GetSitemapNodes()
		{
			// Query configuration should never be preview, and include all cultures for the configuration's Site
			IDocumentQueryConfiguration sitemapQueryConfiguration = new DocumentQueryConfiguration
			{
				Culture = String.Empty,
				IsPreview = false,
				SiteId = queryConfiguration.SiteId
			};

			if (queryConfiguration.SiteId == 0)
			{
				throw new Exception("Site ID must be provided to retrieve sitemap.");
			}


			// Retrieve only documents with page types having a Content URL Pattern
			IDataQuery innerJoin = new DataQuery().Column("ClassID")
												  .From("CMS_Class")
												  .WhereNotNull("ClassURLPattern")
												  .AsSubQuery();


			// Execute and return the query
			return DocumentHelper.GetDocuments<TreeNode>()
								 .Columns("DocumentModifiedWhen", "NodeAliasPath", "NodeSiteID", nameof(TreeNode.DocumentCustomData))
								 .Source(s => s.Join($"({innerJoin.QueryText}) AS Class", "Class.ClassID", "V.NodeClassID"))
								 .Published()
								 //.WhereNotIn( "ClassName", new string[] {  } )		// TODO: Allow other excluded page types configurable by the project
								 .Where("( DocumentSearchExcluded IS NULL OR DocumentSearchExcluded != 1 )")
								 .WhereNotEquals(nameof(TreeNode.ClassName), Placeholder.CLASS_NAME)
								 .ApplyConfiguration(queryConfiguration)
								 .OrderBy("NodeLevel, NodeOrder")
								 .ExcludePath("/home",PathTypeEnum.Single)
								 .ExcludePath("/Content-Migration", PathTypeEnum.Section)
								 .ExcludePath("/Migrated-Content", PathTypeEnum.Section)
								 .ExcludePath("/Example-Content", PathTypeEnum.Section)
								 .ExcludePath("/Global-Content", PathTypeEnum.Section)
								 .ExcludePath("/CMS", PathTypeEnum.Section)
								 .TypedResult // Ensures that the result of the query is saved, not the query itself
								 .Where(x => x.HasUrl());
		}
	}
}
