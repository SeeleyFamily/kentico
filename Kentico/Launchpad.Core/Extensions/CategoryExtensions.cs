using System.Collections.Generic;
using System.Linq;
using Launchpad.Core.Models;


namespace Launchpad.Core.Extensions
{

	public static class CategoryExtensions
	{		
		/// <summary>
		/// Use this method to get a category CodeName or CodeNamePath wrapped in a starting and trailing /
		/// The wrapping is necessary for comparison such that each CodeName has a clear begining and ending
		/// All category comparisons should be handled with a wrapped CodeName
		/// EXAMPLE:
		/// Category 1: /launchpad/pdf/
		/// Category 2: /launchpad/types/pdftypes/	
		/// Without wrapped category codename, a search of codename might return unexpected values
		/// </summary>		
		public static string GetWrappedCodeName(this string codeName)
		{
			if (!codeName.StartsWith("/"))
			{
				codeName = "/" + codeName;
			}
			if (!codeName.EndsWith("/"))
			{
				codeName = codeName + "/";
			}
			return codeName;
		}


		public static FilterOption ToFilterOption( this Category category, bool useIdAsValue = true )
		{
			return new FilterOption
			{
				Name = category.DisplayName,
				Value = ( useIdAsValue ? category.Id.ToString() : category.CodeName )
			};
		}


		public static IEnumerable<FilterOption> ToFilterOptions( this IEnumerable<Category> categories, bool useIdAsValue = true )
		{
			return categories.Select( c => ToFilterOption( c, useIdAsValue ) );
		}

	}

}
