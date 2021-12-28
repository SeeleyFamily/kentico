using CMS.DocumentEngine;
using CMS.DocumentEngine.Types.Common;
using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Abstractions.Specifications;
using Launchpad.Core.Configuration;
using Launchpad.Core.Constants;
using Launchpad.Core.Models;
using Launchpad.Infrastructure.Abstractions.Services;
using Launchpad.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Launchpad.Infrastructure.Services
{

	/// <summary>
	/// Provides a class to query any type of <see cref="TreeNode"/>. Use this class when
	/// you want to query and return multiple types of documents and return them as <see cref="PageNode"/> objects.
	/// </summary>
	public class DocumentService : DocumentService<TreeNode>, IDocumentService, IPerScopeService
	{
		#region Fields
		private readonly Lazy<IDocumentUrlPathInfoService> documentUrlPathInfoService;
		#endregion


		public DocumentService
		(
			ICacheService cacheService,
			IDocumentQueryConfiguration queryConfiguration,
			Lazy<IDocumentUrlPathInfoService> documentUrlPathInfoService
		)
			: base(queryConfiguration, cacheService)
		{
			this.documentUrlPathInfoService = documentUrlPathInfoService;
		}



		public new PagedResult<PageNode> Find(IDocumentSpecification specification)
		{
			PagedResult<TreeNode> result = base.Find(specification);


			// Convert the TreeNode results to PageNode objects
			PagedResult<PageNode> converted = new PagedResult<PageNode>
			{
				PageIndex = result.PageIndex,
				PageSize = result.PageSize,
				Items = result.Items.Select(r => r.ToPageNode()).ToArray(),
				RowEnd = result.RowEnd,
				RowStart = result.RowStart,
				Specification = result.Specification,
				Total = result.Total,
				TotalPages = result.TotalPages
			};

			return converted;
		}


		public new IEnumerable<PageNode> Get(ICacheConfiguration cacheConfiguration = null)
		{
			return base.Get(cacheConfiguration).Select(x => x.ToPageNode());
		}


		public new IEnumerable<PageNode> Get(string[] columns)
		{
			return base.Get(columns).Select(x => x.ToPageNode());
		}


		public new PageNode Get(int id)
		{
			return base.Get(id).ToPageNode();
		}


		public PageNode Get(string path, bool useDocumentUrlPath = false)
		{
			if (useDocumentUrlPath)
			{
				return GetByDocumentUrlPath(path); ;
			}
			return Get(path);
		}


		public PageNode GetByDocumentUrlPath(string path)
		{
			var customQueryConfiguration = new DocumentQueryConfiguration(queryConfiguration);
			customQueryConfiguration.IncludeArchived = true;

			TreeNode getNode()
			{
				DocumentUrlPathInfo documentUrlPathInfo = documentUrlPathInfoService.Value.Get(path);
				if (documentUrlPathInfo != null)
				{
					return DocumentHelper.GetDocuments()
								 .TopN(1)
								 .WhereEquals(nameof(TreeNode.NodeID), documentUrlPathInfo.NodeId)
								 .WhereNotNull(Constants.DocumentForeignKeyValueColumnName)
								 .WithCoupledColumns()
								 .ApplyConfiguration(customQueryConfiguration)
								 .FirstOrDefault()
								 .ApplyMetadataSettings();
				}
				else
				{
					// This UrlPath Extension converts a single select into a multi document query which joins all possible page types and fields
					// This is really bad performance as number of pages & page types increases
					// This causes significant database load
					return null;
					/*
					return DocumentHelper.GetDocuments()
								 .TopN(1)
								 //.Path( path, PathTypeEnum.Explicit ) // Use UrlPath instead to use DocumentUrlPath instead of NodeAliasPath
								 .UrlPath(path, PathTypeEnum.Explicit).As<MultiDocumentQuery>()
								 .WhereNotNull(Constants.DocumentForeignKeyValueColumnName)
								 .WithCoupledColumns()
								 .ApplyConfiguration(customQueryConfiguration)
								 .FirstOrDefault()
								 .ApplyMetadataSettings();
					*/
				}
			}


			TreeNode node = cacheService.GetNodeFromCache(
				loadFunction: getNode,
				cacheKey: $"node|documenturlpath|{path.ToLower()}"
			);

			return node?.ToPageNode();
		}


		public new PageNode Get(string path)
		{
			TreeNode getNode()
			{
				return DocumentHelper.GetDocuments()
								.TopN(1)
								.Path(path, PathTypeEnum.Explicit)
								.WhereNotNull(Constants.DocumentForeignKeyValueColumnName)
								.WithCoupledColumns()
								.ApplyConfiguration(queryConfiguration)
								.FirstOrDefault()
								.ApplyMetadataSettings();
			}


			TreeNode node = cacheService.GetNodeFromCache(
				loadFunction: getNode,
				cacheKey: $"node|{path.ToLower()}"
			);

			return node?.ToPageNode();
		}


		public new PageNode Get(Guid guid)
		{
			TreeNode getNode()
			{
				return DocumentHelper.GetDocuments()
							 .TopN(1)
							 .WhereEquals("NodeGuid", guid)
							 .WithCoupledColumns()
							 .ApplyConfiguration(queryConfiguration)
							 .FirstOrDefault()
							 .ApplyMetadataSettings();
			}

			TreeNode node = cacheService.GetNodeFromCache(getNode, $"nodeguid|{guid.ToString().ToLower()}");

			return node?.ToPageNode();
		}

		public new IEnumerable<PageNode> Get(IEnumerable<Guid> guids)
		{
			return base.Get(guids).Select(x => x.ToPageNode());
		}


		public new IEnumerable<PageNode> GetByParent(int id, int count)
		{

			return base.GetByParent(id, count).Select(x => x.ToPageNode());
		}


		public new IEnumerable<PageNode> GetByParent(string path, int count)
		{
			return base.GetByParent(path, count).Select(x => x.ToPageNode());
		}


		public new IEnumerable<PageNode> GetByParent(Guid guid, int count)
		{
			return base.GetByParent(guid, count).Select(x => x.ToPageNode());
		}


		public new IEnumerable<PageNode> GetFromFolder<TFolderType>( string path, int count )
			where TFolderType : class
		{
			return base.GetFromFolder<TFolderType>( path, count ).Select( x => x.ToPageNode() );
		}


		public new IEnumerable<PageNode> GetFromGlobalContent(int count)
		{
			return base.GetFromGlobalContent(count).Select(x => x.ToPageNode());
		}


		public PageNode Get404Page()
		{
			PageNotFound loadFunction()
			{
				PageNotFound node = DocumentHelper.GetDocuments<PageNotFound>()
											   .TopN(1)
											   .ApplyConfiguration(queryConfiguration)
											   .FirstOrDefault()
											   .ApplyMetadataSettings();
				return node;
			}

			return cacheService.GetNodeFromCache(loadFunction, $"node|pagenotfound|404").ToPageNode();
		}


		public IEnumerable<PageNode> GetByType(string className, bool cacheNodes = true)
		{
			DocumentQuery query = DocumentHelper.GetDocuments(className)
												.ApplyConfiguration(queryConfiguration);


			// Ensure individual nodes are placed into cache
			if (cacheNodes)
			{
				cacheService.EnsureNodesAreCached(query);
			}


			// Return them
			return query.ToArray().Select(n => n.ToPageNode()).ToArray();
		}


		public PageNode GetErrorPage(int statusCode)
		{
			ErrorPage loadFunction()
			{
				ErrorPage node = DocumentHelper.GetDocuments<ErrorPage>()
											   .TopN(1)
											   .WhereEquals("ErrorType", statusCode)
											   .ApplyConfiguration(queryConfiguration)
											   .FirstOrDefault()
											   .ApplyMetadataSettings();

				// Fallback to 500 page for any non-400/500 error
				if (node == null && statusCode != 404 && statusCode != 500)
				{
					node = DocumentHelper.GetDocuments<ErrorPage>()
											   .TopN(1)
											   .WhereEquals("ErrorType", 500)
											   .ApplyConfiguration(queryConfiguration)
											   .FirstOrDefault()
											   .ApplyMetadataSettings();
				}


				return node;
			}


			return cacheService.GetNodeFromCache(loadFunction, $"node|error|{statusCode}").ToPageNode();
		}


		public PageNode GetHomePage()
		{
			//Home loadFunction()
			//{
			//	return DocumentHelper.GetDocuments<Home>()
			//								  .TopN(1)
			//								  .ApplyConfiguration(queryConfiguration)
			//								  .FirstOrDefault()
			//								  .ApplyMetadataSettings();
			//}

			// Updated to return /home instead of Common.Home for Custom Home Implementations

			TreeNode loadFunction()
			{
				return DocumentHelper.GetDocuments()
											  .Path("/home", PathTypeEnum.Explicit)
											  .WithCoupledColumns()
											  .TopN(1)
											  .ApplyConfiguration(queryConfiguration)
											  .FirstOrDefault()
											  .ApplyMetadataSettings();
			}

			return cacheService.GetNodeFromCache(loadFunction, $"node|home").ToPageNode();
		}


		public PageNode GetNearestAncestor(PageNode pageNode, string ancestorClassName)
		{
			return cacheService.GetFromRotatingCache(
				cs =>
				{
					cs.AddNodeDependency(pageNode.NodeID);
					TreeNode treeNode = DocumentHelper.GetDocuments()
					.ApplyConfiguration(queryConfiguration)
					.WhereEquals("NodeID", pageNode.NodeID)
					.FirstOrDefault();

					return GetNearestAncestor(treeNode, ancestorClassName);
				},
				cacheKey: $"documentService|type|get|nearestancestor|{pageNode.NodeID}|{ancestorClassName}".ToLower()
			);
		}


		public string GetRobotsFile( )
		{

			string robotsText = cacheService.GetFromCache( cs =>
			{
				var doc = DocumentHelper.GetDocuments()
											  .Path( "/robots", PathTypeEnum.Explicit )
											  .WithCoupledColumns()
											  .TopN( 1 )
											  .ApplyConfiguration( queryConfiguration )
											  .FirstOrDefault();
				if( doc != null )
				{
					foreach( DocumentAttachment attachment in doc.AllAttachments )
					{
						// Get Binary value of robots file
						// takes first attachment for now but logic can be updated in the future
						byte[] bytes = AttachmentBinaryHelper.GetAttachmentBinary( attachment );
						string utfString = Encoding.UTF8.GetString( bytes, 0, bytes.Length );
						return utfString;
					}
				}

				return String.Empty;
			},
				cacheKey: $"node|robots",
				cacheDependencies: new string[]
				{
					$"node|${queryConfiguration.SiteName.ToLower()}|robots"
				},
				alwaysCache: true
			);


			if( !String.IsNullOrEmpty( robotsText ) )
			{
				return robotsText;
			}

			// return default Robots file
			return "User-agent: * \r\nDisallow: /\r\n\r\nSitemap: /sitemap.xml";
		}



		private PageNode GetNearestAncestor(TreeNode treeNode, string ancestorClassName)
		{
			while (treeNode != null)
			{
				if (treeNode.ClassName == ancestorClassName)
				{
					return treeNode.ToPageNode();
				}

				treeNode = treeNode.Parent;
			}

			return null;
		}

	}

}
