using System.Collections.Specialized;
using Launchpad.Core.Abstractions.Specifications;
using Launchpad.Core.Attributes;
using Launchpad.Core.Enums;
using Launchpad.Core.Extensions;


namespace Launchpad.Core.Specifications
{

	public class LocationSpecification : ISpecification, ILocationSpecification, ISortSpecification
	{
		#region Properties
		public virtual int CountryId { get; set; }
		public virtual string CountryTwoLetterCode { get; set; }
		public virtual int PageIndex { get; set; }
		public virtual int PageSize { get; set; }
		public virtual string Path { get; set; }
		public virtual string Sort { get; set; } = SortType.AZ.GetAttribute<CodeDisplayNameTypeAttribute>().CodeName;
		public virtual string State { get; set; }
		public virtual int StateId { get; set; }
		#endregion


		public LocationSpecification( )
		{

		}


		public LocationSpecification( NameValueCollection keyValues )
		{
			this.Parse( keyValues, nameof( CountryId ) );
			this.Parse( keyValues, nameof( CountryTwoLetterCode ) );
			this.Parse( keyValues, nameof( PageIndex ) );
			this.Parse( keyValues, nameof( PageSize ) );
			this.Parse( keyValues, nameof( Sort ) );
			this.Parse( keyValues, nameof( State ) );
			this.Parse( keyValues, nameof( StateId ) );
		}



		/// <remarks>
		/// This is a typical prime number based hashing algorthm that should result in a unique hash code value for any set of values in a specification.
		/// </remarks>
		public override int GetHashCode( )
		{
			unchecked
			{
				int hash = 17;

				hash = hash * 23 + CountryId.GetHashCode();
				hash = hash * 23 + ( CountryTwoLetterCode?.GetHashCode() ?? 0 );
				hash = hash * 23 + PageIndex.GetHashCode();
				hash = hash * 23 + PageSize.GetHashCode();
				hash = hash * 23 + ( Path?.GetHashCode() ?? 0 );
				hash = hash * 23 + ( Sort?.GetHashCode() ?? 0 );
				hash = hash * 23 + ( State?.GetHashCode() ?? 0 );
				hash = hash * 23 + StateId.GetHashCode();

				return hash;
			}
		}

	}

}
