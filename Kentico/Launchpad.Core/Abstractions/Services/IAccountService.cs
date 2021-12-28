using System.Collections.Generic;
using Launchpad.Core.Abstractions.Models;
using Launchpad.Core.Models;


namespace Launchpad.Core.Abstractions.Services
{

	public interface IAccountService
	{
		IEnumerable<Role> GetRoles( int userId );
		IUser GetUser( int userId );
		void SetAcl( IUser user, int userId, int siteId );
	}

}
