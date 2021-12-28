using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launchpad.Core.Abstractions.Specifications
{
	// Collections are used to prefilter a set of data.
	// Collections will require the data to have ALL of the passed values (AND)
	// ExpandedCollections will require the data to have ANY of the passed values (OR)	
	public interface ICollectionSpecification
	{
		string[] Collections { get; set; }
		string[] ExpandedCollections { get; set; }
	}
}
