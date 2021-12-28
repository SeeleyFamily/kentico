using CMS.Tests;
using Launchpad.Core.Utilities;
using NUnit.Framework;


namespace Launchpad.Infrastructure.Tests.Extensions
{

	public class CoalesceUtilityTests : IntegrationTests
	{

		[Test]
		public void CoalescesNull()
		{
			// Arrange
			string string1 = null;
			string string2 = "";
			string string3 = "string";

			int? int1 = null;
			int? int2 = null;
			int? int3 = 3;


			// Act
			string string4 = CoalesceUtility.Coalesce( string1, string2, string3 );
			string string5 = CoalesceUtility.Coalesce( int1, int2, int3 );


			// Assert
			Assert.AreEqual( string2, string4 );
			Assert.AreEqual( int3.ToString(), string5 );
		}


		[Test]
		public void CoalescesWhitespace( )
		{
			// Arrange
			string string1 = null;
			string string2 = "";
			string string3 = "string";


			// Act
			string string4 = CoalesceUtility.CoalesceWithoutWhitespace( string1, string2, string3 );


			// Assert
			Assert.AreEqual( string3, string4 );
		}

	}

}
