using System;
using System.Linq;


namespace Launchpad.Core.Utilities
{

	/// <summary>
	/// Contains methods for coalescing values.
	/// </summary>
	public static class CoalesceUtility
	{
		/// <summary>
		/// Returns the first non NULL value in a list of objects calling their ToString() method.
		/// </summary>
		public static string Coalesce( params object[] objects )
		{
			string value = objects.FirstOrDefault( o => o != null )?.ToString();

			return value ?? String.Empty;
		}


		/// <summary>
		/// Returns the first non NULL and non-Whitespace only value in a list of objects calling their ToString() method.
		/// </summary>
		public static string CoalesceWithoutWhitespace( params object[] objects )
		{
			string value = objects.FirstOrDefault( o => !String.IsNullOrWhiteSpace( o?.ToString() ) )?.ToString();

			return value ?? String.Empty;
		}
	}

}
