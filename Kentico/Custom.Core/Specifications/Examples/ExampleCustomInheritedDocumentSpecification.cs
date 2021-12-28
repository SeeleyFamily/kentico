using System.Collections.Specialized;

namespace Custom.Core.Specifications.Examples
{
	public class ExampleCustomInheritedDocumentSpecification : ExampleCustomDocumentSpecification
	{
		public string CustomInheritedFilterProperty { get; set; }
		public ExampleCustomInheritedDocumentSpecification()
			: base()
		{

		}
		public ExampleCustomInheritedDocumentSpecification(NameValueCollection keyValues)
			: base(keyValues)
		{
		}
	}
}
