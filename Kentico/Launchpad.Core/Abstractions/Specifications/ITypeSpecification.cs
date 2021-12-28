namespace Launchpad.Core.Abstractions.Specifications
{
	public interface ITypeSpecification
	{
		string[] Types { get; set; }
		string[] ExcludedTypes { get; set; }
	}
}
