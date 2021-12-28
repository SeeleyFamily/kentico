using System.Linq;
using CMS.Globalization;
using CMS.Tests;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Infrastructure.Utilities;
using NUnit.Framework;


namespace Launchpad.Infrastructure.Tests.Services
{

	public class CountryServiceTests : IntegrationTests
	{
		#region Fields
		private readonly ICountryService service;
		#endregion


		public CountryServiceTests()
		{
			// Default service setup
			service = ServiceCreatorUtility.CreateCountryService();
		}



		[Test]
		public void GetsCountries()
		{
			// Act
			var countries = service.GetCountries();


			// Assert
			Assert.IsNotNull(countries);
			Assert.IsNotEmpty(countries);
			Assert.IsNotNull(countries.FirstOrDefault(c => c.TwoLetterCode == "US"));
		}


		[Test]
		public void GetsSpecificCountry()
		{
			// Arrange
			CountryInfo countryInfo = CountryInfo.Provider.Get()
														 .TopN(1)
														 .WhereNotEmpty("CountryTwoLetterCode")
														 .OrderBy("NewID()")
														 .FirstOrDefault();


			// Act
			var country = service.GetCountry(countryInfo.CountryTwoLetterCode);


			// Assert
			Assert.IsNotNull(country);
			Assert.AreEqual(countryInfo.CountryDisplayName, country.Name);
			Assert.AreEqual(countryInfo.CountryID, country.Id);
		}


		[Test]
		public void GetsStates()
		{
			// Act
			var states = service.GetStates();


			// Assert
			Assert.IsNotNull(states);
			Assert.IsNotEmpty(states);
			Assert.IsNotNull(states.FirstOrDefault(c => c.Abbreviation == "IL"));
		}


		[Test]
		public void GetsSpecificState()
		{
			// Arrange
			StateInfo stateInfo = StateInfo.Provider.Get()
												   .TopN(1)
												   .OrderBy("NewID()")
												   .FirstOrDefault();


			// Act
			var state = service.GetState(stateInfo.StateCode);


			// Assert
			Assert.IsNotNull(state);
			Assert.AreEqual(stateInfo.StateDisplayName, state.Name);
			Assert.AreEqual(stateInfo.StateID, state.Id);
		}

	}

}
