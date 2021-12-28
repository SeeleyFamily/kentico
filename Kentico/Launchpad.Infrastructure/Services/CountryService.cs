using CMS.Globalization;
using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Models;
using Launchpad.Infrastructure.Abstractions.Services;
using System.Collections.Generic;
using System.Linq;


namespace Launchpad.Infrastructure.Services
{

	public class CountryService : ICountryService, IPerScopeService
	{
		#region Fields
		private readonly ICacheService cacheService;
		#endregion


		public CountryService
		(
			ICacheService cacheService
		)
		{
			this.cacheService = cacheService;
		}



		public IEnumerable<Country> GetCountries()
		{
			return cacheService.GetFromCache(cs =>
		   {
			   return CountryInfo.Provider.Get()
										 .TypedResult
										 .Select(c => new Country
										 {
											 Id = c.CountryID,
											 Name = c.CountryDisplayName,
											 ThreeLetterCode = c.CountryThreeLetterCode,
											 TwoLetterCode = c.CountryTwoLetterCode
										 });
		   },

			$"countries|all");
		}


		public Country GetCountry(int id)
		{
			return GetCountries().FirstOrDefault(c => c.Id == id);
		}


		public Country GetCountry(string twoLetterCode)
		{
			return GetCountries().FirstOrDefault(c => c.TwoLetterCode == twoLetterCode);
		}


		public IEnumerable<State> GetStates()
		{
			return cacheService.GetFromCache(cs =>
		   {
			   return StateInfo.Provider.Get()
									   .TypedResult
									   .Select(s => new State
									   {
										   Abbreviation = s.StateCode,
										   Id = s.StateID,
										   Name = s.StateDisplayName
									   });
		   },

			$"states|all");
		}


		public State GetState(int id)
		{
			return GetStates().FirstOrDefault(s => s.Id == id);
		}


		public State GetState(string abbreviation)
		{
			return GetStates().FirstOrDefault(s => s.Abbreviation == abbreviation);
		}
	}

}
