using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using Launchpad.Core.Abstractions.Specifications;
using Launchpad.Core.Extensions;


namespace Launchpad.Core.Specifications
{

	public class SearchIndexSpecification : ISearchIndexSpecification
	{
		#region Properties
		public IEnumerable<string> Facets { get; set; }
		public string Filter { get; set; }
		public string Query { get; set; }
		public int PageIndex { get; set; }
		public int PageSize { get; set; }

		[IgnoreDataMember]
		public string IndexName { get; set; }
		#endregion


		public SearchIndexSpecification()
		{
		}


		public SearchIndexSpecification(NameValueCollection keyValues)
		{
			if( keyValues.Count == 0 )
			{
				return;
			}


			this.Parse( keyValues, nameof( PageIndex ) );
			this.Parse( keyValues, nameof( PageSize ) );

			Query = keyValues[ "Query" ] ?? keyValues[ "SearchTerm" ];
		}



		/// <remarks>
		/// This is a typical prime number based hashing algorthm that should result in a unique hash code value for any set of values in a specification.
		/// </remarks>
		public override int GetHashCode( )
		{
			unchecked
			{
				int hash = 17;

				hash = hash * 23 + IndexName.GetHashCode();
				hash = hash * 23 + ( Query?.GetHashCode() ?? 0 );
				hash = hash * 23 + PageIndex.GetHashCode();
				hash = hash * 23 + PageSize.GetHashCode();

				return hash;
			}
		}

	}

}
