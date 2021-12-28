using Launchpad.Core.Abstractions.Specifications;
using System.Collections.Specialized;


namespace Launchpad.Core.Specifications
{

	public class PeopleSpecification : DocumentSpecification, IDocumentSpecification
	{


		#region Properties		
		#endregion


		public PeopleSpecification()
		{

		}


		public PeopleSpecification(NameValueCollection keyValues)
			: base(keyValues)
		{
		}


	}

}