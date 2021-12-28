namespace Launchpad.Core.Abstractions.Specifications
{
	public interface IChildSummarySpecification : ISpecification,
		IClassNameSpecification,
		IPagedSpecification,
		ISortSpecification
	{
		int NodeLevels { get; set; }
	}

}
