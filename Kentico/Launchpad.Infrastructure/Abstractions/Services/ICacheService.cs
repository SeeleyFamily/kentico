using System;
using System.Collections.Generic;
using CMS.DocumentEngine;
using CMS.Helpers;
using Launchpad.Core.Abstractions.Configuration;


namespace Launchpad.Infrastructure.Abstractions.Services
{

	public interface ICacheService : IPerScopeService
	{
		/// <summary>
		/// Gets an ICacheConfiguration instance with IsCached = false
		/// </summary>
		ICacheConfiguration NoCache { get; }

		/// <summary>
		/// Clears an item from cache by touching its cache key.
		/// </summary>
		void ClearFromCache( string cacheKey, ICacheConfiguration cacheConfiguration = null );


		/// <summary>
		/// Creates an <see cref="ICacheConfiguration"/> instance set to global, non-site specific settings.
		/// </summary>
		/// <param name="cacheConfiguration">Optional configuration to duplicate settings from.</param>
		ICacheConfiguration CreateGlobalConfiguration( ICacheConfiguration cacheConfiguration = null );


		/// <summary>
		/// Ensures a single <see cref="TreeNode"/> is placed into cache with its common retrieval keys such as "{cache prefix}|node|{id}" and "{cache prefix}|node|{path}".
		/// </summary>
		void EnsureNodeIsCached( TreeNode node, ICacheConfiguration cacheConfiguration = null );


		/// <summary>
		/// Ensures each  <see cref="TreeNode"/> in a collection is placed into cache with its common retrieval keys such as "{cache prefix}|node|{id}" and "{cache prefix}|node|{path}".
		/// </summary>
		void EnsureNodesAreCached( IEnumerable<TreeNode> nodes, ICacheConfiguration cacheConfiguration = null );


		/// <summary>
		/// Retrieves <typeparamref name="T"/> from cache, or if it isn't present, puts the result of <paramref name="loadFunction"/> into cache and returns it.
		/// </summary>
		T GetFromCache<T>( Func<CacheSettings, T> loadFunction, string cacheKey, IEnumerable<string> cacheDependencies = null, ICacheConfiguration cacheConfiguration = null, bool alwaysCache = false);


		/// <summary>
		/// Retrieves <typeparamref name="T"/> from cache or a rotating backup cache, or if it isn't present, puts the result of <paramref name="loadFunction"/> into cache and returns it. This method ensures
		/// the <paramref name="loadFunction"/> is run in the background after its first execution and users are always provided with the last generated, cached item without blocking the current thread.
		/// </summary>
		T GetFromRotatingCache<T>( Func<CacheSettings, T> loadFunction, string cacheKey, IEnumerable<string> cacheDependencies = null, ICacheConfiguration cacheConfiguration = null, bool alwaysCache = false );


		/// <summary>
		/// Retrieves a <see cref="TreeNode"/> from cache, or if isn't present, puts the result of <paramref name="loadFunction"/> into cache and returns it. Additionally, ensures
		/// caching keys for common node retrieval mechanisms such as by <see cref="TreeNode.NodeID"/>, <see cref="TreeNode.NodeGUID"/> and <see cref="TreeNode.NodeAliasPath"/>.
		/// </summary>
		T GetNodeFromCache<T>( Func<T> loadFunction, string cacheKey, IEnumerable<string> cacheDependencies = null, ICacheConfiguration cacheConfiguration = null )
			where T : TreeNode, new();


		/// <summary>
		/// Sets an item into cache, replacing any object with the same cache key if it exists already.
		/// </summary>
		void SetCacheItem<T>( T item, string cacheKey, IEnumerable<string> cacheDependencies = null, ICacheConfiguration cacheConfiguration = null );

    }

}
