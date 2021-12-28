using Launchpad.Core.Abstractions.Models.Summary;
using System;
using System.Collections.Generic;

namespace Launchpad.Core.Models.Summary
{

	/// <summary>
	/// Represents a summary of an item in a list in a listing page.
	/// </summary>
	public class SummaryItem : ISummaryItem
	{
		public string Id { get; set; }
		public Guid Guid { get; set; }
		public string Type { get; set; }
		public string Title { get; set; }
		public string Summary { get; set; }
		public string Url { get; set; }
		public string Image { get; set; }
		public Cta Cta { get; set; }
		public IEnumerable<Tag> Tags { get; set; }


		public IEnumerable<Breadcrumb> Breadcrumbs { get; set; }
	}

}
