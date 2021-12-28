using System.Threading.Tasks;
using CMS.Tests;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Enums;
using Launchpad.Core.Models;
using Launchpad.Core.Specifications;
using Launchpad.Infrastructure.Utilities;
using NUnit.Framework;


namespace Launchpad.Infrastructure.Tests.Services
{

	public class GoogleMapServiceTests : IntegrationTests
	{
		#region Fields
		private readonly IGoogleMapService service;
		#endregion


		public GoogleMapServiceTests( )
		{
			// Default service setup
			service = ServiceCreatorUtility.CreateGoogleMapService();
		}



		[Test]
		public async Task GetsLocationType( )
		{
			// Arrange
			QuerySpecification specification = new QuerySpecification
			{
				Query = "Chicago"
			};


			// Act
			MapLocation location = await service.GetMapLocation( specification );


			// Assert
			Assert.IsNotNull( location.LocationType == LocationType.State );
			Assert.IsNotEmpty( location.State );
			Assert.IsNotNull( location.State );
			Assert.IsNotEmpty( location.StateAbbreviation );
			Assert.IsNotNull( location.StateAbbreviation );
		}


		[Test]
		public async Task GetsMapLocation( )
		{
			// Arrange
			QuerySpecification specification = new QuerySpecification
			{
				Query = "1 S Wacker Dr #3, Chicago, IL 60606"
			};


			// Act
			MapLocation location = await service.GetMapLocation( specification );


			// Assert
			Assert.IsNotNull( location );
			Assert.IsNotEmpty( location.City );
			Assert.IsNotNull( location.City );
			Assert.IsNotEmpty( location.State );
			Assert.IsNotNull( location.State );
			Assert.IsNotEmpty( location.StateAbbreviation );
			Assert.IsNotNull( location.StateAbbreviation );
		}


		[Test]
		public async Task SafelyHandlesOtherCountries( )
		{
			// Arrange
			QuerySpecification specification = new QuerySpecification
			{
				Query = "Brazil"
			};


			// Act
			MapLocation location = await service.GetMapLocation( specification );


			// Assert
			Assert.IsTrue( location.LocationType == LocationType.Country );
		}


		[Test]
		public async Task SafelyHandlesUnknown( )
		{
			// Arrange
			QuerySpecification specification = new QuerySpecification
			{
				Query = "oijrgjogoijgrojireg34124"
			};


			// Act
			MapLocation location = await service.GetMapLocation( specification );


			// Assert
			Assert.IsNull( location );
		}


	}

}