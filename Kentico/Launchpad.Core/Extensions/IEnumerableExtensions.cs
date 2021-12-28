using System.Collections.Generic;
using System.Linq;

namespace Launchpad.Core.Extensions
{
	public static class IEnumerableExtensions
	{
		public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
		{
			if (enumerable == null || !enumerable.Any())
			{
				return true;
			}
			// case where the list is full of null values
			if (enumerable.All(x => x == null))
			{
				return true;
			}
			return false;
		}
	}
}
