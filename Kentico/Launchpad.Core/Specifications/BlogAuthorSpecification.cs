using Launchpad.Core.Abstractions.Specifications;
using Launchpad.Core.Attributes;
using Launchpad.Core.Enums;
using Launchpad.Core.Extensions;
using System.Collections.Specialized;

namespace Launchpad.Core.Specifications
{
	public class BlogAuthorSpecification : DocumentSpecification, IDocumentSpecification
	{


		#region Properties		
		public new string Sort { get; set; } = SortType.AZ.GetAttribute<CodeDisplayNameTypeAttribute>().CodeName;
		#endregion


		public BlogAuthorSpecification()
		{
		}


		public BlogAuthorSpecification(NameValueCollection keyValues)
			: base(keyValues)
		{
		}


	}

}