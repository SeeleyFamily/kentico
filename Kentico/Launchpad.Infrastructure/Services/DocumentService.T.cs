using CMS.DataEngine;
using CMS.DocumentEngine;
using CMS.Helpers;
using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Core.Abstractions.Models;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Abstractions.Specifications;
using Launchpad.Core.Extensions;
using Launchpad.Core.Models;
using Launchpad.Infrastructure.Abstractions.Services;
using Launchpad.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Launchpad.Infrastructure.Services
{

	/// <summary>
	/// Provides a class to return specific tree nodes of type <typeparamref name="T"/>. Use this class when
	/// you want to query and return specific page types and those types only.
	/// </summary>
	public class DocumentService<T> : IDocumentService<T>, ISearchableDocumentService<T, IDocumentSpecification>, IPerScopeService
		where T : TreeNode, new()
	{
		#region Fields
		protected readonly ICacheService cacheService;
		protected readonly IDocumentQueryConfiguration queryConfiguration;
		#endregion


		public DocumentService
		(
			IDocumentQueryConfiguration queryConfiguration,
			ICacheService cacheService
		)
		{
			this.cacheService = cacheService;
			this.queryConfiguration = queryConfiguration;
		}



		public virtual PagedResult<T> Find(IDocumentSpecification specification)
		{
			// Apply a default query with the filters specified in the specification, converted to a paged result
			return DocumentHelper.GetDocuments<T>()
								 .ApplyDocumentSpecification(specification)
								 .ApplyConfiguration(queryConfiguration)
								 .ToPagedResult(specification);
		}


		public virtual IEnumerable<T> Get(ICacheConfiguration cacheConfiguration = null)
		{
			var className = new T().ClassName;

			IEnumerable<T> result = cacheService.GetFromRotatingCache(cs =>
			{
				return DocumentHelper.GetDocuments<T>()
									 .ApplyConfiguration(queryConfiguration)
									 .ToArray();
			},
				cacheKey: $"documentService|type|get|all|{className}".ToLower(),
				cacheDependencies: new List<string>()
				{
					$"nodes|{queryConfiguration.SiteName}|{className}|all"
				},
				cacheConfiguration: cacheConfiguration
			);
			return result;
		}


		public virtual IEnumerable<T> Get(string[] columns)
		{
			var className = new T().ClassName;
			var columnsString = columns.Join("|");
			IEnumerable<T> result = cacheService.GetFromRotatingCache(cs =>
			{
				var query = DocumentHelper.GetDocuments<T>();

				if (!columns.IsNullOrEmpty())
				{
					query = query.WithRequiredColumns(columns).WithPageNodeColumns().As<DocumentQuery<T>>();
				}

				return query.ApplyConfiguration(queryConfiguration)
									 .ToArray();
			},
				cacheKey: $"documentService|type|get|columns|{columnsString}|all|{className}".ToLower(),
				cacheDependencies: new List<string>()
				{
					$"nodes|{queryConfiguration.SiteName}|{className}|all"
				}
			);
			return result;
		}


		public virtual T Get(int id)
		{
			T getNode()
			{
				return DocumentHelper.GetDocuments<T>()
							 .TopN(1)
							 .WhereEquals("NodeID", id)
							 .ApplyConfiguration(queryConfiguration)
							 .FirstOrDefault()
							 .ApplyMetadataSettings();
			}


			return cacheService.GetNodeFromCache(getNode, $"nodeid|{id}");
		}


		public virtual T Get(string path)
		{
			T getNode()
			{
				return DocumentHelper.GetDocuments<T>()
							 .TopN(1)
							 .Path(path, PathTypeEnum.Explicit)
							 .ApplyConfiguration(queryConfiguration)
							 .FirstOrDefault()
							 .ApplyMetadataSettings();
			}


			return cacheService.GetNodeFromCache(getNode, $"node|{path.ToLower()}");
		}


		public virtual T Get(Guid guid)
		{
			T getNode()
			{
				return DocumentHelper.GetDocuments<T>()
							 .TopN(1)
							 .WhereEquals("NodeGUID", guid)
							 .ApplyConfiguration(queryConfiguration)
							 .WithPageTypeColumnsOnly()
							 .FirstOrDefault()
							 .ApplyMetadataSettings();
			}


			return cacheService.GetNodeFromCache(getNode, $"nodeguid|{guid.ToString().ToLower()}");
		}


		public virtual IEnumerable<T> Get(IEnumerable<Guid> guids)
		{
			List<T> nodes = new List<T>();
			foreach (var guid in guids)
			{
				var node = Get(guid);
				if (node != null)
				{
					nodes.Add(node);
				}
			}
			return nodes;
		}


		public virtual IEnumerable<T> GetByParent(int id, int count = 0)
		{
			IEnumerable<T> getChildren(CacheSettings cs)
			{
				DocumentQuery<T> x = DocumentHelper.GetDocuments<T>()
												   .WhereEquals("NodeParentID", id)
												   .ApplyConfiguration(queryConfiguration);

				cs.CacheDependency = CacheHelper.GetCacheDependency(x.Select(doc => $"nodeguid|{queryConfiguration.SiteName}|{doc.NodeGUID}").ToList());

				return x.ToArray();
			}


			string className = typeof(T).FullName;
			IEnumerable<T> items = cacheService.GetFromRotatingCache(getChildren, $"nodesByParent|{id}|{className}");

			if (count > 0)
			{
				items = items.Take(count);
			}


			return items;
		}


		public virtual IEnumerable<T> GetByParent(string path, int count = 0)
		{
			IEnumerable<T> getChildren(CacheSettings cs)
			{
				cs.CacheDependency = CacheHelper.GetCacheDependency(($"node|{queryConfiguration.SiteName}|{path}|childnodes").ToLower());

				DocumentQuery<T> x = DocumentHelper.GetDocuments<T>()
													.Path(path, PathTypeEnum.Children)
													.ApplyConfiguration(queryConfiguration)
													.WithPageTypeColumnsOnly()
													.OrderBy(nameof(TreeNode.NodeParentID), nameof(TreeNode.NodeOrder))
													;

				return x.ToArray();
			}


			string className = typeof(T).FullName;
			IEnumerable<T> items = cacheService.GetFromRotatingCache(getChildren, $"nodesByParent|{path.ToLower()}|{className}");

			if (count > 0)
			{
				items = items.Take(count);
			}


			return items;
		}


		public IEnumerable<T> GetByParent(Guid guid, int count = 0)
		{
			//set up Subequery to get NodeAliasPath
			IDataQuery globalQuery = new DocumentQuery()
												.TopN(1)
												.Column("NodeID  as GlobalNodeID")
												.Where($"NodeGuid = '{guid}'")
												.ApplyConfiguration(queryConfiguration)
												.AsSubQuery();


			IEnumerable<T> getChildren(CacheSettings cs)
			{
				DocumentQuery<T> x = DocumentHelper.GetDocuments<T>()
												   .Source(qs => qs.InnerJoin($"({globalQuery.QueryText}) [Page]", "V.NodeParentID = [Page].GlobalNodeID"))
												   .ApplyConfiguration(queryConfiguration);

				cs.CacheDependency = CacheHelper.GetCacheDependency(x.Select(doc => $"nodeguid|{queryConfiguration.SiteName}|{doc.NodeGUID}").ToList());

				return x.ToArray();
			}


			string className = typeof(T).FullName;
			IEnumerable<T> items = cacheService.GetFromRotatingCache(getChildren, $"nodesByParent|{guid}|{className}");

			if (count > 0)
			{
				items = items.Take(count);
			}


			return items;
		}


		public virtual IEnumerable<T> GetFromFolder<TFolderType>(string path, int count = 0)
			where TFolderType : class
		{
			Type type = typeof(TFolderType);

			if (!type.IsSubclassOf(typeof(TreeNode)))
			{
				throw new ArgumentException($"{type.Name} is not of type TreeNode and cannot be used in this method.");
			}

			string className = type.GetFields(BindingFlags.Static | BindingFlags.Public)
								   .First(f => f.Name == "CLASS_NAME")
								   .GetValue(null)
								   .ToString();


			IEnumerable<T> getChildren(CacheSettings cs)
			{
				cs.CacheDependency = CacheHelper.GetCacheDependency(($"node|{queryConfiguration.SiteName}|{path}|childnodes").ToLower());

				DocumentQuery<T> x = DocumentHelper.GetDocuments<T>()
													.Path(path, PathTypeEnum.Children)
													.Source(qs => qs.InnerJoin($"( SELECT NodeID AS ContainerID FROM View_CMS_Tree_Joined F WHERE F.ClassName = N'{className}' ) F", "F.ContainerID = V.NodeParentID"))
													.ApplyConfiguration(queryConfiguration)
													.WithPageTypeColumnsOnly()
													.OrderBy(nameof(TreeNode.NodeParentID), nameof(TreeNode.NodeOrder))
													;

				return x.ToArray();
			}


			IEnumerable<T> items = cacheService.GetFromRotatingCache(getChildren, $"nodes|foldercontent|{path.ToLower()}|{className}");

			if (count > 0)
			{
				items = items.Take(count);
			}


			return items;
		}


		public virtual IEnumerable<T> GetFromGlobalContent(int count = 0)
		{
			var className = (new T()).ClassName;
			IEnumerable<T> getChildren(CacheSettings cs)
			{
				cs.CacheDependency = CacheHelper.GetCacheDependency(($"nodes|{queryConfiguration.SiteName}|{className}|all").ToLower());

				DocumentQuery<T> x = DocumentHelper.GetDocuments<T>()
													.FromGlobalContent(queryConfiguration)
													.ApplyConfiguration(queryConfiguration)
													.WithPageTypeColumnsOnly()
													.OrderBy(nameof(TreeNode.NodeParentID), nameof(TreeNode.NodeOrder))
													;

				return x.ToArray();
			}

			IEnumerable<T> items = cacheService.GetFromRotatingCache(getChildren, $"nodes|globalcontent|{className}");

			if (count > 0)
			{
				items = items.Take(count);
			}


			return items;
		}


		public bool IsNodeAuthorizedForUser(PageNode node, IUser user)
		{
			// Is an admin inside Kentico in preview mode?
			if (queryConfiguration.IsPreview && (user?.IsAuthenticated ?? false))
			{
				// Allow preview always
				return true;
			}


			// Get the ACL ID
			int aclId = (node.AclID > 0) ? node.AclID : Get(node.NodeID)?.NodeACLID ?? 0;

			if (aclId == 0)
			{
				throw new Exception("Cannot determine Node ACL.");
			}


			// Get the applicable ACL Items from the user
			IEnumerable<AccessControlItem> items = user.AccessControlList.Where(acl => acl.AclId == aclId);

			if (!items.Any())
			{
				// No permissions with the given ACL; implicit denied
				return false;
			}

			else if (!items.Any(acl => acl.IsAllowed))
			{
				// Nothing explicity allowing access
				return false;
			}

			else if (items.Any(acl => acl.IsDenied))
			{
				// Denied somewhere, user is not permitted
				return false;
			}


			// The user is permitted
			return true;
		}
	}

}
