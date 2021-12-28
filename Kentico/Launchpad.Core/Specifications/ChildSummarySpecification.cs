using Launchpad.Core.Abstractions.Specifications;
using Launchpad.Core.Attributes;
using Launchpad.Core.Enums;
using Launchpad.Core.Extensions;
using System.Collections.Specialized;

namespace Launchpad.Core.Specifications
{
	public class ChildSummarySpecification : IChildSummarySpecification
	{
		public int NodeLevels { get; set; } = 1;
		public string[] ClassNames { get; set; }
		public string[] ExcludedClassNames { get; set; }
		public int PageIndex { get; set; }
		public int PageSize { get; set; }
		public string Sort { get; set; } = SortType.NodeOrder.GetAttribute<CodeDisplayNameTypeAttribute>().CodeName;


		public ChildSummarySpecification()
		{

		}

		public ChildSummarySpecification(NameValueCollection keyValues)
		{
			this.Parse(keyValues, nameof(ClassNames));
			this.Parse(keyValues, nameof(ExcludedClassNames));

			this.Parse(keyValues, nameof(NodeLevels));

			this.Parse(keyValues, nameof(PageIndex));
			this.Parse(keyValues, nameof(PageSize));

			this.Parse(keyValues, nameof(Sort));
		}
	}
}

