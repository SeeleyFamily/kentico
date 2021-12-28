using System;
using System.Collections.Generic;

namespace Launchpad.Core.Models
{

	public class Filter
	{
		public string Label { get; set; }
		public string Specification { get; set; }
		public IEnumerable<FilterOption>  Options { get; set; }		
		
	}
	public class FilterOption
	{
		public string Name { get; set; }
		public string Value { get; set; }
		public IEnumerable<FilterOption> Options { get; set; }
	}
}
