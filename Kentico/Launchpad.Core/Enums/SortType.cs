using Launchpad.Core.Attributes;

namespace Launchpad.Core.Enums
{

	public enum SortType
	{

		[CodeDisplayNameType(nameof(NodeOrder), "Node Order")]
		NodeOrder = 0,
		[CodeDisplayNameType(nameof(AZ), "A to Z")]
		AZ = 1,
		[CodeDisplayNameType(nameof(ZA), "Z to A")]		
		ZA = 2,
		[CodeDisplayNameType(nameof(Newest), "Newest to Oldest")]
		Newest = 3,
		[CodeDisplayNameType(nameof(Oldest), "Oldest to Newest")]
		Oldest = 4,
		[CodeDisplayNameType(nameof(Nodes), "Nodes")]
		Nodes = 5,
		[CodeDisplayNameType(nameof(Guids), "Guids")]
		Guids = 6,

	}

}
