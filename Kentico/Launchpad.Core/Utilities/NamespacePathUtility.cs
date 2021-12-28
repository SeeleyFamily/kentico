using Launchpad.Core.Attributes;
using Launchpad.Core.Enums;
using Launchpad.Core.Extensions;


namespace Launchpad.Core.Utilities
{

	public static class NamespacePathUtility
	{

		public static string GetCommonViewPath()
		{
			return NamespacePath.Common.GetAttribute<CodeDisplayNameTypeAttribute>().DisplayName;
		}


		public static string GetCustomViewPath()
		{
			return NamespacePath.Custom.GetAttribute<CodeDisplayNameTypeAttribute>().DisplayName;
		}

	}

}
