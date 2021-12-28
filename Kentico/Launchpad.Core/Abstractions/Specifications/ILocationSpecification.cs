


namespace Launchpad.Core.Abstractions.Specifications
{

	/// <summary>
	/// An interface defining location specifications.
	/// </summary>
	public interface ILocationSpecification : IPagedSpecification
	{
		int CountryId { get; set; }
		string CountryTwoLetterCode { get; set; }
		string State { get; set; }
		int StateId { get; set; }
	}

}
