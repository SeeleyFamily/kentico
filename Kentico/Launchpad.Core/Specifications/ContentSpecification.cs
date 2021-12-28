using Launchpad.Core.Abstractions.Specifications;
using Launchpad.Core.Attributes;
using Launchpad.Core.Enums;
using Launchpad.Core.Extensions;
using System;
using System.Collections.Specialized;


namespace Launchpad.Core.Specifications
{
	public class ContentSpecification : DocumentSpecification, IDocumentSpecification,		
		IFacetSpecification,
		IFeaturedSpecification
	{
		#region Properties		
		public new string Sort { get; set; } = SortType.Newest.GetAttribute<CodeDisplayNameTypeAttribute>().CodeName;
		public Guid[] FeaturedGuids { get; set; }
		public string[] Facets { get; set; }
		public string[] Topic { get; set; }
		public string[] Type { get; set; }		
		public Guid[] AuthorGuids { get; set; }						
		#endregion

		public ContentSpecification()
		{

		}

		public ContentSpecification(NameValueCollection keyValues)
			: base(keyValues)
		{			
			this.Parse(keyValues, nameof(Type));
			this.Parse(keyValues, nameof(Topic));			
		}
	}
}
