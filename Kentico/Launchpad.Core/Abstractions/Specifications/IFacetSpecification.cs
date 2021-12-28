namespace Launchpad.Core.Abstractions.Specifications
{
	public interface IFacetSpecification : ISpecification,
		IPathSpecification
	{
		string[] Facets { get; set; }
	}
}
