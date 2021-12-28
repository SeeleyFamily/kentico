


using Launchpad.Core.Enums;

namespace Launchpad.Core.Abstractions.Specifications
{

	/// <summary>
	/// An interface defining paging properties.
	/// </summary>
	public interface IPagedSpecification : ISpecification
	{
		int PageIndex { get; set; }
		int PageSize { get; set; }		
	}

}
