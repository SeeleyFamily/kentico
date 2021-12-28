using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Hosting;
using CMS.DocumentEngine;
using CMS.Helpers;
using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Core.Configuration;
using Launchpad.Infrastructure.Abstractions.Services;


namespace Launchpad.Infrastructure.Services
{

	public class CacheService : ICacheService
	{
		#region Fields
		private readonly Lazy<ICacheConfiguration> CacheConfiguration;

		private const string secondaryCacheSuffix = "-secondary";
		#endregion


		public ICacheConfiguration NoCache => new CacheConfiguration
		{
			IsCached = false
		}; 
		
		
		public CacheService
		(
			Lazy<ICacheConfiguration> cacheConfiguration
		)
		{
			this.CacheConfiguration = cacheConfiguration;
		}



		public virtual void ClearFromCache( string cacheKey, ICacheConfiguration cacheConfiguration = null )
		{
			ICacheConfiguration localConfiguration = EnsureConfiguration( cacheConfiguration );
			List<string> keyParts = CreateCacheKeyParts( cacheKey, localConfiguration );

			string key = String.Join( "|", keyParts );


			CacheHelper.ClearCache( key );
		}


		public virtual ICacheConfiguration CreateGlobalConfiguration( ICacheConfiguration cacheConfiguration = null )
		{
			ICacheConfiguration copyFromConfiguration = EnsureConfiguration( cacheConfiguration );

			ICacheConfiguration configuration = new CacheConfiguration
			{
				CacheMinutes = copyFromConfiguration.CacheMinutes,
				IsCached = copyFromConfiguration.IsCached,
				NamePrefix = String.Empty,
				SiteID = 0
			};

			return configuration;
		}


		public virtual void EnsureNodeIsCached( TreeNode node, ICacheConfiguration cacheConfiguration = null )
		{
			ICacheConfiguration localConfiguration = EnsureConfiguration( cacheConfiguration );

			// If not caching, return immediately
			if( !localConfiguration.IsCached || localConfiguration.CacheMinutes <= 0 )
			{
				return;
			}


			// Cache the node by its common retrieval mechanisms
			CacheSettings cacheSettings = new CacheSettings( cacheConfiguration.CacheMinutes );

			EnsureCacheKey( node.NodeID.ToString(), node, cacheSettings, localConfiguration.NamePrefix );
			EnsureCacheKey( node.NodeAliasPath, node, cacheSettings, localConfiguration.NamePrefix );
		}


		public virtual void EnsureNodesAreCached( IEnumerable<TreeNode> nodes, ICacheConfiguration cacheConfiguration = null )
		{
			ICacheConfiguration localConfiguration = EnsureConfiguration( cacheConfiguration );

			// If not caching, return immediately
			if( !localConfiguration.IsCached || localConfiguration.CacheMinutes <= 0 )
			{
				return;
			}


			// Cache each node in the collection
			foreach( TreeNode node in nodes )
			{
				EnsureNodeIsCached( node, localConfiguration );
			}
		}


		public virtual T GetFromCache<T>( Func<CacheSettings, T> loadFunction, string cacheKey, IEnumerable<string> cacheDependencies = null, ICacheConfiguration cacheConfiguration = null, bool alwaysCache = false)
        {
			ICacheConfiguration localConfiguration = EnsureConfiguration( cacheConfiguration );
			// Configure this call's cache settings
			CacheSettings cacheSettings = CreateCacheSettings( cacheKey, localConfiguration );

            // If not caching, return the result immediately
            if (!alwaysCache && (!localConfiguration.IsCached || localConfiguration.CacheMinutes <= 0))
            {
				return loadFunction( cacheSettings );
			}

			// Retrieve from cache or execute the loading function and cache the item
			T cachedItem = CacheHelper.Cache( cs =>
			{
				// Set the provided cache dependencies, if any
				if( cacheDependencies != null && cacheDependencies.Any() )
				{
					cs.CacheDependency = CacheHelper.GetCacheDependency( cacheDependencies.ToArray() );
				}


				// Load & return the item
				return loadFunction( cacheSettings );

			}, cacheSettings );


			// Return the cached item
			return cachedItem;
		}


		public T GetFromRotatingCache<T>( Func<CacheSettings, T> loadFunction, string cacheKey, IEnumerable<string> cacheDependencies = null, ICacheConfiguration cacheConfiguration = null, bool alwaysCache = false )
		{
			ICacheConfiguration localConfiguration = EnsureConfiguration( cacheConfiguration );

			// Configure this call's cache settings
			CacheSettings cacheSettings = CreateCacheSettings( cacheKey, localConfiguration );

			// If not caching, return immediately
			if( !alwaysCache && ( !localConfiguration.IsCached || localConfiguration.CacheMinutes <= 0 ) )
			{
				return loadFunction( cacheSettings );
			}


			// Create the joined cache keys
			string fullKey = String.Join( "|", CreateCacheKeyParts( cacheKey, localConfiguration ) );
			string fullSecondaryKey = String.Join( "|", CreateCacheKeyParts( $"{cacheKey}{secondaryCacheSuffix}", localConfiguration ) );



			// Is the item in primary cache? If so, return it
			if( CacheHelper.TryGetItem( key: fullKey, caseSensitive: false, output: out T primaryItem ) )
			{
				return primaryItem;
			}


			// Is the item in secondary cache? If so, grab it
			CacheHelper.TryGetItem( key: fullSecondaryKey, caseSensitive: false, output: out T secondaryItem );


			// At this point, we are going to load the primary and overwrite the secondary caches (if any)
			// If we don't have the item yet, we do this in the current thread; otherwise, we set it to run in the background thead
			if( primaryItem == null && secondaryItem == null )
			{
				primaryItem = GetFromCache( loadFunction, cacheKey, cacheDependencies, cacheConfiguration, alwaysCache );
			}


			// If we didn't get anything back from either, stop here
			if( primaryItem == null && secondaryItem == null )
			{
				return default(T);
			}


			// Did we force load the primary item already? If so, let's just set the secondary item and finish
			if( primaryItem != null )
			{
				SetSecondaryCache( primaryItem, cacheKey, localConfiguration );
				return primaryItem;
			}


			// Otherwise, we have a secondary item and need to reload the primary in the background
			// NOTE: Only do this in an ASP.NET environment; in test environments this is likely to fail
			if( HostingEnvironment.IsHosted )
			{
				HostingEnvironment.QueueBackgroundWorkItem( token =>
				{
					primaryItem = GetFromCache( loadFunction, cacheKey, cacheDependencies, cacheConfiguration, alwaysCache );
					SetSecondaryCache( primaryItem, cacheKey, localConfiguration );
				} );
			}


			// After queuing the primary to load in the background, return the secondary
			return secondaryItem;
		}


		public virtual T GetNodeFromCache<T>( Func<T> loadFunction, string cacheKey, IEnumerable<string> cacheDependencies = null, ICacheConfiguration cacheConfiguration = null )
			where T : TreeNode, new()
		{
			ICacheConfiguration localConfiguration = EnsureConfiguration( cacheConfiguration );

			// If not caching, return the result immediately
			if( !localConfiguration.IsCached || localConfiguration.CacheMinutes <= 0 )
			{
				return loadFunction();
			}



			// Retrieve from cache or execute the loading function and cache the node
			T cachedNode = CacheHelper.Cache( cs =>
			{
				T node = loadFunction();                

				// Compile the dependencies for this cache item
				List<string> dependencies = ( cacheDependencies ?? Enumerable.Empty<string>() ).ToList();

				if( node != null )
				{
					dependencies = dependencies.Union( new string[]
					{
						$"node|{node.NodeSiteName.ToLower()}|{node.NodeAliasPath.ToLower()}|{node.DocumentCulture.ToLower()}",
						$"nodeid|{node.NodeID}",
						$"nodeguid|{node.NodeSiteName.ToLower()}|{node.NodeGUID.ToString().ToLower()}"
					} ).ToList();


					// Automatically ensure the site ID is attached to the cache key
					if( localConfiguration.SiteID <= 0 )
					{
						cs.CustomCacheItemName = $"{cs.CacheItemName}|site:{node.NodeSiteID}";
					}
				}

				// Update the cached item's dependencies
				if( dependencies.Count > 0 )
				{
					cs.CacheDependency = CacheHelper.GetCacheDependency( dependencies );
				}


				// Inject other common node-retrieval keys (by node ID, node alias path, etc)
				if( node != null )
				{
					EnsureCacheKey( node.NodeID.ToString(), node, cs, localConfiguration.NamePrefix );
					EnsureCacheKey( node.NodeAliasPath.ToLower(), node, cs, localConfiguration.NamePrefix );
				}
				else
				{
					cs.Cached = false; // Should Resolve The Issue Described Below.
				}

				// TODO: This will purposefully cache non-results too, helpful in avoiding repeated queries for 404 node alias paths; however, those cache items will not 
				// be given any kind of cache dependency, resulting in a valid path being created not overriding the cached null result until it times out or cache is emptied
				// Determine if there's a better way, such as a shorter time limit on 404 cached items, or setting dependencies on the entire tree / any page touch.


				// Return the node
				return node;

			}, CreateCacheSettings( cacheKey, localConfiguration ) );



			// Return the cached or newly retrieved node
			return cachedNode;
		}


        public virtual void SetCacheItem<T>( T item, string cacheKey, IEnumerable<string> cacheDependencies = null, ICacheConfiguration cacheConfiguration = null )
		{
			ICacheConfiguration localConfiguration = EnsureConfiguration( cacheConfiguration );

			// If not caching, return immediately
			if( !localConfiguration.IsCached || localConfiguration.CacheMinutes <= 0 )
			{
				return;
			}


			// Configure this call's cache settings
			CacheSettings cacheSettings = CreateCacheSettings( cacheKey, localConfiguration );


			// Clear the cache item if it already exists
			CacheHelper.ClearCache( cacheSettings.CacheItemName );

			// Set the item into cache
			CacheHelper.Cache( cs => item, cacheSettings );
		}

		

		/// <summary>
		/// Creates a default <see cref="List{String}" /> of cache key parts for use in a <see cref="CacheSettings" /> object. Helps ensure uniformity in key structure, with the <paramref name="cacheConfiguration"/> 
		/// name prefix joined with the supplied <paramref name="cacheKey"/>. If the <paramref name="cacheConfiguration"/> identifies a Site ID, that is added as well. Format: {name prefix}|{cacheKey}|site:{siteID}
		/// </summary>
		/// <returns>test</returns>
		protected virtual List<string> CreateCacheKeyParts( string cacheKey, ICacheConfiguration cacheConfiguration )
		{
			List<string> keyParts = new string[] { cacheConfiguration.NamePrefix }.Union( cacheKey.Split( '|' ) ).ToList();

			if( cacheConfiguration.SiteID > 0 )
			{
				keyParts.Add( $"site:{cacheConfiguration.SiteID}" );
			}

			if( !string.IsNullOrWhiteSpace(cacheConfiguration.Culture) ) {
				keyParts.Add($"culture:{cacheConfiguration.Culture}");
			}

			return keyParts;
		}


		/// <summary>
		/// Creates a default, uniformly configured <see cref="CacheSettings"/> for a given <paramref name="cacheKey"/> and <paramref name="cacheConfiguration"/>.
		/// </summary>
		protected virtual CacheSettings CreateCacheSettings( string cacheKey, ICacheConfiguration cacheConfiguration )
		{
			List<string> keyParts = CreateCacheKeyParts( cacheKey, cacheConfiguration );
			CacheSettings cacheSettings = new CacheSettings( cacheConfiguration.CacheMinutes, String.Join( "|", keyParts ) );   // NOTE: using static cache key here instead of string[], Kentico not honoring string[] name parts

			return cacheSettings;
		}


		/// <summary>
		/// Ensures a <see cref="TreeNode"/> is cached in one of its common, uniform key formats. Used to place nodes into cache under common cache keys.
		/// </summary>
        protected virtual void EnsureCacheKey( string keyPart, TreeNode node, CacheSettings cacheSettings, string namePrefix )
		{
			string[] cacheKeyParts = new string[] { namePrefix, "node", keyPart, $"site:{node.NodeSiteID}" };

			CacheHelper.Cache( ( ) => node, new CacheSettings( cacheSettings.CacheMinutes, String.Join( "|", cacheKeyParts ) )
			{
				CacheDependency = cacheSettings.CacheDependency
			} );
		}


		/// <summary>
		/// Ensures that a <see cref="ICacheConfiguration"/> object is instantiated and used if the supplied configuration is null. Falls back to the service's configuration or a basic configuration if one does not exist for the service.
		/// </summary>
		protected virtual ICacheConfiguration EnsureConfiguration( ICacheConfiguration cacheConfiguration )
		{
			return cacheConfiguration ?? this.CacheConfiguration.Value ?? new CacheConfiguration
			{
				IsCached = true
			};
		}


		protected virtual void SetSecondaryCache<T>( T item, string key, ICacheConfiguration cacheConfiguration )
		{
			// Secondary should never expire, to save users from being exposed to the load function delay
			cacheConfiguration.CacheMinutes = 10080;	// one week, can't do unlimited

			// Set the secondary item, with NO cache dependencies, so that touching dummy keys never touches this item and leaves it in the backup position
			SetCacheItem( item, $"{key}{secondaryCacheSuffix}", cacheConfiguration: cacheConfiguration );
		}

	}

}
