using CMS.CustomTables;
using CMS.DataEngine;
using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Infrastructure.Abstractions.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Launchpad.Infrastructure.Services
{

	public class CustomTableService : ICustomTableService, IPerScopeService
	{
		#region Fields
		protected Lazy<ICacheService> CacheService { get; }
		#endregion

		public CustomTableService
		(
			Lazy<ICacheService> cacheService
		)
		{
			CacheService = cacheService;
		}

		/// <summary>
		/// Default Custom Table Service Getter without any cache implementation
		/// </summary>		
		private IEnumerable<CustomTableItem> GetCustomTableItems(string className)
		{
			var customTableItems = new List<CustomTableItem>();
			// Gets the custom table
			DataClassInfo customTable = DataClassInfoProvider.GetDataClassInfo(className);
			if (customTable != null)
			{				
				customTableItems.AddRange(CustomTableItemProvider.GetItems(customTable.ClassName));
			}
			return customTableItems;
		}


		/// <summary>
		/// Th
		/// </summary>		
		public IEnumerable<CustomTableItem> GetByType(string className, bool cached = true)
		{
			var customTableItems = new List<CustomTableItem>();
			if (cached)
			{
				try
				{
					// TypeName should be the be prefixed with the default namespace provided by Kentico
					// Go into any custom table class generated to verify.
					// The typeName is the Namespace + ClassName + "Item"
					var fullTypeName = $"CMS.CustomTables.Types.{className}Item";

					// Using reflection below

					// CustomTableService<T> takes a type argument
					Type typeArgument = Type.GetType(fullTypeName);

					Type genericCustomTableService = typeof(CustomTableService<>);
					Type constructedClass = genericCustomTableService.MakeGenericType(typeArgument);

					// Create an instance of the generic custom table service class using reflection
					// The constructor of the generic service class requires the cache service
					object instance = Activator.CreateInstance(constructedClass, new object[] { CacheService });

					// Use reflection to pull the Method GetItems
					// Method is hardcoded below - beware of changes to CustomTableService<T>
					// Invoke the method in the generic service
					MethodInfo method = constructedClass.GetMethod("GetItems");
					var result = method.Invoke(instance, null);

					// Cast the result set to the expected type for this Custom Table Service
					var typedResult = ((IEnumerable)result).Cast<CustomTableItem>().ToList();
					customTableItems.AddRange(typedResult);
				}
				catch (Exception)
				{
					// The startup cache failed for the specified custom table.				
					// TODO: Log warning/error

					// NOTE
					// We saw the below failing during app start up caching
					// It appears that CustomTableItemProvider requires a request context that is not available during app startup
					// It is like checking for correct license implementation
				}
			}
			else
			{
				customTableItems.AddRange(GetCustomTableItems(className));
			}
			return customTableItems;
		}
	}
}