using System;

namespace Launchpad.Core.Abstractions.Specifications
{
	public interface IFeaturedSpecification : ISpecification
	{
		Guid[] FeaturedGuids { get; set; }		
	}
}
