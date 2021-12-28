using Launchpad.Core.Models;
using System.Collections.Generic;

namespace CMSAppCustom.Models
{
	public class CustomDataObject
	{
		public Preview Preview { get; set; }
		public List<string> SearchBlobValues { get; set; }
		public string SearchBlob { get; set; }
		public string CategoryCodeNamePaths { get; set; }
		public string CategoryDisplayNames { get; set; }
		public string CategoryNames { get; set; }

		public string Breadcrumbs { get; set; }
	}
}
