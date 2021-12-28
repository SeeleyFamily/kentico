using Launchpad.Core.Abstractions.Specifications;
using Launchpad.Core.Attributes;
using Launchpad.Core.Enums;
using Launchpad.Core.Extensions;
using System;
using System.Collections.Specialized;

namespace Launchpad.Core.Specifications
{
	public class BlogSpecification : DocumentSpecification, IDocumentSpecification,
		IFeaturedSpecification,
		ITopicSpecification,
		IAuthorSpecification
	{


		#region Properties				
		public new string Sort { get; set; } = SortType.Newest.GetAttribute<CodeDisplayNameTypeAttribute>().CodeName;
		public Guid[] FeaturedGuids { get; set; }
		public string[] Topics { get; set; }
		public string[] ExcludedTopics { get; set; }
		public Guid[] Authors { get; set; }
		#endregion


		public BlogSpecification()
		{
		}


		public BlogSpecification(NameValueCollection keyValues)
			: base(keyValues)
		{
			this.Parse(keyValues, nameof(FeaturedGuids));
			this.Parse(keyValues, nameof(Topics));
			this.Parse(keyValues, nameof(ExcludedTopics));
			this.Parse(keyValues, nameof(Authors));
		}


	}

}