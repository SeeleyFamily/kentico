using CMS.Membership;
using CMS.SiteProvider;
using Launchpad.Core.Abstractions.Models;
using Launchpad.Core.Models;


namespace Launchpad.Infrastructure.Abstractions.Services
{

	public interface IKenticoUserService
	{
		AnonymousUser GetPublicAnonymousUser( int siteId );
		UserInfo EnsureUser( IUser user, SiteInfo siteInfo );
	}

}
