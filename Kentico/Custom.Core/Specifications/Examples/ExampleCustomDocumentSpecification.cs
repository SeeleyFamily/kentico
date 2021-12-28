using Launchpad.Core.Specifications;
using System.Collections.Specialized;

namespace Custom.Core.Specifications.Examples
{
	public class ExampleCustomDocumentSpecification : DocumentSpecification
	{
		public string CustomFilterProperty { get; set; }
		public ExampleCustomDocumentSpecification()
			: base()
		{

		}
		public ExampleCustomDocumentSpecification(NameValueCollection keyValues)
			: base(keyValues)
		{			
		}
	}
}
