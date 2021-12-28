using Launchpad.Core.Attributes;

namespace Launchpad.Core.Enums
{
	public enum DefaultCategory
	{
		[CodeDisplayNameType("Types", "Types")] Types = 0,		
		[CodeDisplayNameType("Topics", "Topics")] Topics = 1,
		[CodeDisplayNameType("PrimaryTopics", "PrimaryTopics")] PrimaryTopics = 2,
		[CodeDisplayNameType("AdditionalTopics", "AdditionalTopics")] AdditionalTopics = 3,
	}
}
