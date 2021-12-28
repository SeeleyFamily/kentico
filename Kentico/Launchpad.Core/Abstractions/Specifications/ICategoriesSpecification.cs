namespace Launchpad.Core.Abstractions.Specifications
{
	public interface ICategoriesSpecification
	{
		string[] Categories { get; set; }
		string[] ExcludedCategories { get; set; }
	}
}
