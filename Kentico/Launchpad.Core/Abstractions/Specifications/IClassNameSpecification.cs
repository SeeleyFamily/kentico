namespace Launchpad.Core.Abstractions.Specifications
{
	public interface IClassNameSpecification
	{
		string[] ClassNames { get; set; }
		string[] ExcludedClassNames { get; set; }
	}
}
