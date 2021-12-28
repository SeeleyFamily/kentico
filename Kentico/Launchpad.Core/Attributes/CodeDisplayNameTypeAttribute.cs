using System;

namespace Launchpad.Core.Attributes
{
	public class CodeDisplayNameTypeAttribute : Attribute
	{
		public string CodeName { get; private set; }
		public string DisplayName { get; private set; }
		public CodeDisplayNameTypeAttribute(string codeName, string displayName)
		{
			CodeName = codeName;
			DisplayName = displayName;			
		}		
	}
}
