using Launchpad.Core.Models;
using System;
using System.Collections.Generic;

namespace Launchpad.Core.Abstractions.Models.Summary
{

	public interface ISummaryItem
	{
		string Id { get; set; }
		Guid Guid { get; set; }
		string Type { get; set; }
		string Title { get; set; }
		string Summary { get; set; }
		string Url { get; set; }
		string Image { get; set; }
		Cta Cta { get; set; }
		IEnumerable<Tag> Tags { get; set; }
		
		IEnumerable<Breadcrumb> Breadcrumbs { get; set; }
	}

}
