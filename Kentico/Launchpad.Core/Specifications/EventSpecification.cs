using Launchpad.Core.Abstractions.Specifications;
using Launchpad.Core.Attributes;
using Launchpad.Core.Enums;
using Launchpad.Core.Extensions;
using System.Collections.Specialized;


namespace Launchpad.Core.Specifications
{

	public class EventSpecification : DocumentSpecification, IDocumentSpecification
	{


		#region Properties		
		public new string Sort { get; set; } = SortType.Newest.GetAttribute<CodeDisplayNameTypeAttribute>().CodeName;
		public bool IncludeUpcoming { get; set; }
		public bool IncludePast { get; set; }
		#endregion


		public EventSpecification()
		{

		}


		public EventSpecification(NameValueCollection keyValues)
			: base(keyValues)
		{
			this.Parse(keyValues, nameof(IncludeUpcoming));
			this.Parse(keyValues, nameof(IncludePast));
		}


	}

}