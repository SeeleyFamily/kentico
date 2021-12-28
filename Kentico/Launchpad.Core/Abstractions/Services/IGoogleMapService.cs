using System.Threading.Tasks;
using Launchpad.Core.Models;
using Launchpad.Core.Specifications;


namespace Launchpad.Core.Abstractions.Services
{

	public interface IGoogleMapService
	{
		Task<MapLocation> GetMapLocation( QuerySpecification specification );
	}

}