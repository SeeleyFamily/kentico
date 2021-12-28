namespace Launchpad.Core.Abstractions.Specifications
{
	public interface ITopicSpecification
	{
		string[] Topics { get; set; }
		string[] ExcludedTopics { get; set; }
	}
}
