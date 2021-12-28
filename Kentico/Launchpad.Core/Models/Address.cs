


namespace Launchpad.Core.Models
{

	public class Address
	{
		public string Address1 { get; set; }
		public string Address2 { get; set; }
		public string City { get; set; }
		public State State { get; set; }
		public Country Country { get; set; }
		public string Zipcode { get; set; }

		public decimal? Latitude { get; set; }
		public decimal? Longitude { get; set; }
	}

}
