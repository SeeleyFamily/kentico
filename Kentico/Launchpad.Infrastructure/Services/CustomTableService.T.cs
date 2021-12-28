using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CMS.CustomTables;
using CMS.DataEngine;
using CMS.Helpers;
using Launchpad.Core.Configuration;
using Launchpad.Infrastructure.Abstractions.Services;


namespace Launchpad.Infrastructure.Services
{

	public class CustomTableService<T> : ICustomTableService<T>
		where T : CustomTableItem, new()
	{
		#region Fields
		protected Lazy<ICacheService> CacheService { get; }
		protected readonly string ClassName;
		protected const string NamePrefix = "customtable";
		protected const int CacheMinutes = 43800; // default redirect cache time should be for one month
		protected CacheConfiguration CacheConfiguration { get; set; }
		#endregion

		// NOTE - The non generic version of this class - CustomTableService refers to this constructor
		// Changes to ctor params will need to be updated in the CustomTableService class.
		public CustomTableService(
		Lazy<ICacheService> cacheService
		)
		{
			CacheService = cacheService;			
			CacheConfiguration = new CacheConfiguration()
			{
				IsCached = true,
				CacheMinutes = CacheMinutes,
				NamePrefix = NamePrefix,
			};
		}
		
		// Basic load method for Getting Generic Custom Table Items
		private IEnumerable<T> GetCustomTableItems()
		{
			// NOTE
			// We saw the below failing during app start up caching
			// It appears that CustomTableItemProvider requires a request context that is not available during app startup
			// It is like checking for correct license implementation
			return CustomTableItemProvider.GetItems<T>().TypedResult;		
		}

		// NOTE - The non generic version of this class - CustomTableService refers to this method (hard-coded)
		// Changes to this method signature will need to be updated in the CustomTableService class.
		public IEnumerable<T> GetItems()
		{
			/*
			 * 
			// The following lines use reflection to get the className property of the instance of the Typed Custom Table Item
			// This is required due to the cache dependency and generic nature of the serice
			// The cache dependency uses the className as a trigger for when content updates are made to the custom table.
			// Consider re-implemting
			var instance = (T)Activator.CreateInstance(typeof(T), new object[] { });			
			var className = instance.ClassName;			

			*/
			var fullTypeName = typeof(T).FullName;
			var className = fullTypeName.Replace("CMS.CustomTables.Types.", "");
			if (className.LastIndexOf("Item") >= 0)
			{
				className = className.Substring(0, className.LastIndexOf("Item"));
			}

			// Use the Cache Dependency for Custom Tables
			IEnumerable<string> cacheDependencies = new List<string> { ($"customtableitem.{className}|all").ToLower() };

			// Use the cache service.			
			return CacheService.Value.GetFromCache<IEnumerable<T>>(cs => GetCustomTableItems(), ($"{NamePrefix}|{className}").ToLower(), cacheDependencies, CacheConfiguration);			
		}

	}

}
