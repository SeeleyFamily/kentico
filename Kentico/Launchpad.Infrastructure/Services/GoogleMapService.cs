using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Enums;
using Launchpad.Core.Models;
using Launchpad.Core.Specifications;
using Launchpad.Core.Abstractions.Configuration;
using Newtonsoft.Json.Linq;
using Launchpad.Core.Constants;
using System.Configuration;


namespace Launchpad.Infrastructure.Services
{

	public class GoogleMapService : IGoogleMapService, IPerScopeService
	{
		#region Fields
		private readonly HttpClient httpClient;
		private readonly ISettingsService settingsService;
		#endregion


		public GoogleMapService
		(
			HttpClient httpClient,
			ISettingsService settingsService
		)
		{
			this.httpClient = httpClient;
			this.settingsService = settingsService;
		}



		public async Task<MapLocation> GetMapLocation( QuerySpecification specification )
		{
			// Get settings
			string apiUrl = ConfigurationManager.AppSettings[ SettingConstants.GoogleMapsApiUrl ];
			string apiKey = settingsService.GetSetting( SettingConstants.GoogleMapsApiKey );

			// Create endpoint
			//string endpoint = $"/geocode/json?key={apiKey}&address={HttpUtility.UrlEncode( specification.Query )}";
			// Added restriction to USA only - lets parameterize this in the future
			string endpoint = $"/geocode/json?key={apiKey}&address={HttpUtility.UrlEncode(specification.Query)}&components=country:USA";


			// Send the request
			HttpResponseMessage response = await httpClient.GetAsync( apiUrl + endpoint );
			string result = await response.Content.ReadAsStringAsync();

			if( !response.IsSuccessStatusCode )
			{
				throw new Exception( result );
			}


			// Parse the response
			JObject json = JObject.Parse( result );

			if( !( json["error_message"] == null ) )
			{
				throw new Exception( json["error_message"].Value<string>() );
			}


			var results = json["results"].FirstOrDefault();

			if( results == null || !results.Any() )
			{
				return null;
			}


			var addressComponents = results["address_components"];
			var geometry = results["geometry"];
			var types = results["types"];

			var city = addressComponents.FirstOrDefault( jt => jt["types"].Any( t => t.Value<string>() == "locality" ) );
			var state = addressComponents.FirstOrDefault( jt => jt["types"].Any( t => t.Value<string>() == "administrative_area_level_1" ) );
			var location = geometry["location"];
			var type = types.FirstOrDefault();


			return new MapLocation
			{
				City = city?["long_name"]?.Value<string>(),
				Latitude = location["lat"].Value<decimal>(),
				Longitude = location["lng"].Value<decimal>(),
				LocationType = GetLocationType( type.Value<string>() ),
				State = state?["long_name"]?.Value<string>(),
				StateAbbreviation = state?["short_name"]?.Value<string>()
			};
		}



		private LocationType GetLocationType( string locationType )
		{
			switch( locationType.ToLower() )
			{
				default:
					return LocationType.Other;

				case "administrative_area_level_1":
					return LocationType.State;

				case "country":
					return LocationType.Country;

				case "locality":
					return LocationType.City;

				case "postal_code":
					return LocationType.Zipcode;
			}
		}
	}

}