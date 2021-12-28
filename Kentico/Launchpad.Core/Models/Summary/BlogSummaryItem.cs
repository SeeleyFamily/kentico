using System;
using System.Collections.Generic;

namespace Launchpad.Core.Models.Summary
{

	public class BlogSummaryItem : SummaryItem
	{


		public bool Featured { get; set; }
		public DateTime? Date { get; set; }
		public IEnumerable<BlogAuthorSummaryItem> Authors { get; set; }



	}

}
