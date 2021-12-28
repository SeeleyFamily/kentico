namespace Launchpad.Core.Abstractions.Specifications
{
	public interface IPathSpecification: ISpecification
	{
		string Path { get; set; }
		bool IncludeDocumentForPath { get; set; }
	}
}
