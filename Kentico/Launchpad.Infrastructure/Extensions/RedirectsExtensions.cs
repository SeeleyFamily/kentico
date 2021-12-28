using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CMS.Base;
using CMS.DocumentEngine;
using CMS.DocumentEngine.Types.Common;
using Launchpad.Core.Models;
using CMS.Module.Redirects;

namespace Launchpad.Infrastructure.Extensions
{

    public static class RedirectsExtensions
    {
        /// <summary>
        /// Converts a <see cref="PermanentRedirectsInfo"/> to its <see cref="Redirect"/> model equivalent.
        /// </summary>
        public static Redirect ToRedirect(this PermanentRedirectsInfo permanentRedirectsInfo)
        {
            if (permanentRedirectsInfo == null)
            {
                return null;
            }
            var redirect = new Redirect(
                           permanentRedirectsInfo.MatchUrl,
                           permanentRedirectsInfo.RedirectUrl
                           );
            return redirect;
        }

        /// <summary>
        /// Converts a <see cref="TemporaryRedirectsInfo"/> to its <see cref="Redirect"/> model equivalent.
        /// </summary>
        public static Redirect ToRedirect(this TemporaryRedirectsInfo temporaryRedirectsInfo)
        {
            if (temporaryRedirectsInfo == null)
            {
                return null;
            }
            var redirect = new Redirect(
                           temporaryRedirectsInfo.MatchUrl,
                           temporaryRedirectsInfo.RedirectUrl,
                           true
                           );
            return redirect;
        }

        /// <summary>
        /// Converts a <see cref="RegexRedirectsInfo"/> to its <see cref="Redirect"/> model equivalent.
        /// </summary>
        public static Redirect ToRedirect(this RegexRedirectsInfo regexRedirectsInfo)
        {
            if (regexRedirectsInfo == null)
            {
                return null;
            }
            var redirect = new Redirect(
                        regexRedirectsInfo.MatchUrl,
                        regexRedirectsInfo.RedirectUrl,
                        false,
                        true,
                        regexRedirectsInfo.RegexReplace,
                        regexRedirectsInfo.Priority
                        );
            return redirect;
        }

        /// <summary>
        /// Cleans the URL to return lower case and removes the trailing slash.
        /// </summary>
        /// <param name="URL"></param>
        /// <returns></returns>
        public static string CleanRedirectsURLs(this string URL)
        {
			if( URL.ToLower().StartsWith("http"))
			{
				return URL;
			}

            if (URL.Equals("/"))
            {
                return URL;
            }else if (URL.EndsWith("/"))
			{
				int lastSlash = URL.LastIndexOf('/');
				URL = (lastSlash > -1) ? URL.Substring(0, lastSlash) : URL;
			}
            return URL.ToLower();
        }
    }

}
