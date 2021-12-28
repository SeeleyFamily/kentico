using System.Collections.Generic;


namespace Launchpad.Core.Models
{

	/// <summary>
	/// A container for content sections including a title, description and collection of items.
	/// </summary>
	public class ListSection<T>
	{
		public string Description { get; set; }
		public IEnumerable<T> Items { get; set; }
		public string Title { get; set; }
	}

}
