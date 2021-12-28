using System.Collections.Generic;


namespace Launchpad.Core.Models
{

	public class Facet
	{
		public string Name { get; set; }
		public IEnumerable<FacetValue> Values { get; set; }
	}

}