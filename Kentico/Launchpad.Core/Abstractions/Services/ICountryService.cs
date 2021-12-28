using System.Collections.Generic;
using Launchpad.Core.Models;


namespace Launchpad.Core.Abstractions.Services
{

	public interface ICountryService
	{
		IEnumerable<Country> GetCountries( );
		Country GetCountry( int id );
		Country GetCountry( string twoLetterCode );
		IEnumerable<State> GetStates( );
		State GetState( int id );
		State GetState( string abbreviation );
	}

}
