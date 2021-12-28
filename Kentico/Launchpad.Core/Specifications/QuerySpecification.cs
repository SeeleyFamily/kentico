using System.Collections.Specialized;
using Launchpad.Core.Abstractions.Specifications;
using Launchpad.Core.Extensions;


namespace Launchpad.Core.Specifications
{

	public class QuerySpecification : ISpecification
	{
		#region Properties
		public string Query { get; set; }
		#endregion


		public QuerySpecification( )
		{
		}


		public QuerySpecification( NameValueCollection keyValues )
		{
			if( keyValues.Count == 0 )
			{
				return;
			}


			this.Parse( keyValues, nameof( Query ) );
		}




		/// <remarks>
		/// This is a typical prime number based hashing algorthm that should result in a unique hash code value for any set of values in a specification.
		/// </remarks>
		public override int GetHashCode( )
		{
			unchecked
			{
				int hash = 17;

				hash = hash * 23 + Query.GetHashCode();

				return hash;
			}
		}
	}

}