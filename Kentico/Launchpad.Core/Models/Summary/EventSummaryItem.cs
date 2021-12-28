using System;
using System.Collections.Generic;

namespace Launchpad.Core.Models.Summary
{
	public class EventSummaryItem : SummaryItem
	{
		public bool Featured { get; set; }
		public DateTime? Date { get; set; }
		public Address Address { get; set; }
	}
}
