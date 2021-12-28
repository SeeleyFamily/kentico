using System.Collections.Generic;
using Launchpad.Core.Enums;
using Launchpad.Core.Models;


namespace Launchpad.Infrastructure.Abstractions.Services
{

	public interface IRedirectService
    {

        /// <summary>
        /// Retrieves a enumerable of <see cref="Redirect"/>
        /// </summary>
        IEnumerable<Redirect> GetRedirects();

        IEnumerable<Redirect> GetRedirects(RedirectMode redirectMode);

        Redirect MatchRedirect(string matchUrl, RedirectMode redirectMode = RedirectMode.AbsolutePath);

        /// <summary>
        /// Clears the cache for <see cref="Redirect"/>
        /// </summary>
        void ClearCache();
    }

}
