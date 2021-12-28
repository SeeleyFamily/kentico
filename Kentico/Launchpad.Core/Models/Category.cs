using System;
using System.Collections.Generic;


namespace Launchpad.Core.Models
{

	public class Category
	{
		#region Properties
		public string DisplayName { get; set; }
		public string CodeName { get; set; }		
		public int Id { get; set; }
		public Guid Guid { get; set; }
		public Category Parent { get; set; }
		public IEnumerable<Category> Children { get; set; }
		public string DisplayNamePath { get; set; }
		public string CodeNamePath { get; set; }
		public int Level { get; set; }
		#endregion
	}

}
