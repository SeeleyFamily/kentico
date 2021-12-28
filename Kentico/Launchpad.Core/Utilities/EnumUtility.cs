using Launchpad.Core.Attributes;
using Launchpad.Core.Extensions;
using Launchpad.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Launchpad.Core.Utilities
{
	public static class EnumUtility<T>
		where T : Enum
	{
		public static IEnumerable<CodeDisplayNameType> GetCodeDisplayNames()
		{
			var codeDisplayNames = Enum.GetValues(typeof(T))
					  .Cast<T>()
					  .Select(d =>
					  {
						  var attribute = d.GetAttribute<CodeDisplayNameTypeAttribute>();
						  var codeDisplayNameType = new CodeDisplayNameType()
						  {
							  CodeName = attribute.CodeName,
							  DisplayName = attribute.DisplayName
						  };
						  return codeDisplayNameType;
					  })
					  .ToList();

			return codeDisplayNames;
		}

		public static string GetDisplayName(string codeName)
		{
			var codeDisplayNames = GetCodeDisplayNames();
			var codeDisplayName = codeDisplayNames.Where(x => x.CodeName.Equals(codeName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
			if (codeDisplayName != null)
			{
				return codeDisplayName.DisplayName;
			}
			return string.Empty;
		}
	}
}
