using Launchpad.Core.Attributes;

namespace Launchpad.Core.Enums
{
	public enum NamespacePath
	{
		[CodeDisplayNameType(nameof(Common), "Common")] Common = 0,
		[CodeDisplayNameType(nameof(Custom), "Custom")] Custom = 1,
	}
}
