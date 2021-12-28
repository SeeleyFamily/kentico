using Launchpad.Core.Enums;


namespace Launchpad.Core.Models
{

	public class MapLocation
	{
		public string City { get; set; }
		public decimal Latitude { get; set; }
		public decimal Longitude { get; set; }
		public LocationType LocationType { get; set; }
		public string State { get; set; }
		public string StateAbbreviation { get; set; }
	}

}