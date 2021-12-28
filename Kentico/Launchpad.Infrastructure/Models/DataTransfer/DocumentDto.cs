using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Launchpad.Core.Abstractions.Models.Document;
using Launchpad.Core.Models;
using Newtonsoft.Json;


namespace Launchpad.Infrastructure.Models.DataTransfer
{

	public class DocumentDto : IDocumentDto
	{
		[Key, JsonProperty("nodeid")]
		public virtual string Id { get; set; }

		[Key, JsonProperty("nodeguid")]
		public virtual Guid Guid { get; set; }

		[JsonProperty("summary_image")]
		public virtual string Image { get; set; }

		[JsonProperty("summary_text")]
		public virtual string Summary { get; set; }

		[JsonProperty("summary_title")]
		public virtual string Title { get; set; }

		[JsonProperty("classname")]
		public virtual string Type { get; set; }

		[JsonProperty("summary_url")]
		public virtual string Url { get; set; }

		public virtual Cta Cta { get; set; }

		public IEnumerable<Tag> Tags { get; set; }

		[JsonProperty("breadcrumbs")]
		public string BreadcrumbString { get; set; }
	}

}
