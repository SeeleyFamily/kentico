using System;
using System.Collections.Generic;
using CMS.DocumentEngine;
using CMS.Helpers;


namespace Launchpad.Infrastructure.Extensions
{

	public static class CacheSettingsExtensions
	{

		/// <summary>
		/// Adds the path of the <see cref="TreeNode"/> and any of its children cache dependency key to the <see cref="CacheSettings"/>.
		/// </summary>
		/// <returns>The collection of key strings set in the <see cref="CacheSettings"/>.</returns>
		public static IEnumerable<string> AddNodePathChildDependencies( this CacheSettings cacheSettings, TreeNode node )
		{
			return AddNodePathChildDependencies( cacheSettings, node.NodeSiteName, node.NodeAliasPath );
		}


		/// <summary>
		/// Adds the path and any of its child nodes cache dependency key to the <see cref="CacheSettings"/>.
		/// </summary>
		/// <returns>The collection of key strings set in the <see cref="CacheSettings"/>.</returns>
		public static IEnumerable<string> AddNodePathChildDependencies( this CacheSettings cacheSettings, string siteName, string nodeAliasPath )
		{
			return AddKey( cacheSettings, $"node|{siteName}|{nodeAliasPath}|childnodes" );
		}


		/// <summary>
		/// Adds the <see cref="TreeNode"/> NodeID cache dependency key to the <see cref="CacheSettings"/>.
		/// </summary>
		/// <returns>The collection of key strings set in the <see cref="CacheSettings"/>.</returns>
		public static IEnumerable<string> AddNodeDependencies( this CacheSettings cacheSettings, IEnumerable<TreeNode> nodes )
		{
			List<string> keys = new List<string>();


			// Add the node dependency keys
			foreach( TreeNode node in nodes )
			{
				keys.Add( $"nodeid|{node.NodeID}" );
			}


			return AddKeys( cacheSettings, keys );
		}


		/// <summary>
		/// Adds the NodeID cache dependency key to the <see cref="CacheSettings"/>.
		/// </summary>
		/// <returns>The collection of key strings set in the <see cref="CacheSettings"/>.</returns>
		public static IEnumerable<string> AddNodeDependency( this CacheSettings cacheSettings, int nodeId )
		{
			return AddKey( cacheSettings, $"nodeid|{nodeId}" );
		}


		/// <summary>
		/// Adds the NodeID cache dependency key to the <see cref="CacheSettings"/>.
		/// </summary>
		/// <returns>The collection of key strings set in the <see cref="CacheSettings"/>.</returns>
		public static IEnumerable<string> AddNodeDependency( this CacheSettings cacheSettings, string siteName, string nodeAliasPath )
		{
			return AddKey( cacheSettings, $"node|{siteName}|{nodeAliasPath}" );
		}

		/// <summary>
		/// Adds the node GUID cache dependency key to the <see cref="CacheSettings"/>.
		/// </summary>
		/// <returns>The collection of key strings set in the <see cref="CacheSettings"/>.</returns>
		public static IEnumerable<string> AddNodeDependency( this CacheSettings cacheSettings, string siteName, Guid nodeGuid )
		{
			return AddKey( cacheSettings, $"nodeguid|{siteName}|{nodeGuid}" );
		}


		/// <summary>
		/// Adds the <see cref="TreeNode"/> NodeID cache dependency key to the <see cref="CacheSettings"/>.
		/// </summary>
		/// <returns>The collection of key strings set in the <see cref="CacheSettings"/>.</returns>
		public static IEnumerable<string> AddNodeDependency( this CacheSettings cacheSettings, TreeNode node )
		{
			return AddNodeDependency( cacheSettings, node.NodeID );
		}


		/// <summary>
		/// Adds the page type class name cache dependency key to the <see cref="CacheSettings"/>.
		/// </summary>
		/// <returns>The collection of key strings set in the <see cref="CacheSettings"/>.</returns>
		public static IEnumerable<string> AddTypeDependency( this CacheSettings cacheSettings, string siteName, string className )
		{
			return AddKey( cacheSettings, $"nodes|{siteName}|{className}|all" );
		}



		private static List<string> AddKey( CacheSettings cacheSettings, string key )
		{
			List<string> keys = GetCurrentCacheDependencyKeys( cacheSettings );


			// Add the node dependency key
			keys.Add( key );

			// Finalize all dependency keys into settings
			cacheSettings.CacheDependency = CacheHelper.GetCacheDependency( keys );


			return keys;
		}


		private static List<string> AddKeys( CacheSettings cacheSettings, IEnumerable<string> keys )
		{
			List<string> list = GetCurrentCacheDependencyKeys( cacheSettings );


			// Add the node dependency keys
			list.AddRange( keys );

			// Finalize all dependency keys into settings
			cacheSettings.CacheDependency = CacheHelper.GetCacheDependency( list );


			return list;
		}


		private static List<string> GetCurrentCacheDependencyKeys( CacheSettings cacheSettings )
		{
			List<string> keys = new List<string>();
			CMSCacheDependency currentDependency = cacheSettings.CacheDependency;

			if( currentDependency != null )
			{
				// Retrieve any current keys
				keys.AddRange( currentDependency.CacheKeys );
			}


			return keys;
		}

	}

}
