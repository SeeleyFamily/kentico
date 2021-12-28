using System;

namespace Launchpad.Core.Models.Summary
{
	public class ContentSummaryItem : SummaryItem
	{
		public CodeDisplayNameType ContentType { get; set; }
		public bool Featured { get; set; }
		public DateTime? Date { get; set; }
	}
}
