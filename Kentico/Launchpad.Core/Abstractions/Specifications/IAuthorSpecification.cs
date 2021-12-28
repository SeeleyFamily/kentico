using System;

namespace Launchpad.Core.Abstractions.Specifications
{
	public interface IAuthorSpecification
	{
		Guid[] Authors { get; set; }
	}
}
