using Launchpad.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launchpad.Core.Abstractions.Models.Document
{
	public interface IDocumentDto
    {
		string Id { get; set; }


		Guid Guid { get; set; }


		string Image { get; set; }


		string Summary { get; set; }


		string Title { get; set; }


		string Type { get; set; }


		string Url { get; set; }

		Cta Cta { get; set; }

		IEnumerable<Tag> Tags { get; set; }


		string BreadcrumbString { get; set; }
	}
}
