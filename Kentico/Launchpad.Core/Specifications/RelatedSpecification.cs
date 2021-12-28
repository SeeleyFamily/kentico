using Launchpad.Core.Abstractions.Specifications;
using System;

namespace Launchpad.Core.Specifications
{
	public class RelatedSpecification : DocumentSpecification, IDocumentSpecification,
		IFeaturedSpecification
	{
		#region Properties		
		public new int PageSize { get; set; } = 3;
		public Guid[] FeaturedGuids { get; set; }
		#endregion

		public RelatedSpecification()
		{

		}
	}
}
