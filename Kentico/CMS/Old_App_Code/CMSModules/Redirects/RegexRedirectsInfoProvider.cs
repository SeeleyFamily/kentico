using System;
using System.Data;

using CMS.Base;
using CMS.DataEngine;
using CMS.Helpers;
using CMS.SiteProvider;

namespace CMS.Module.Redirects
{
    /// <summary>
    /// Class providing <see cref="RegexRedirectsInfo"/> management.
    /// </summary>
    public partial class RegexRedirectsInfoProvider : AbstractInfoProvider<RegexRedirectsInfo, RegexRedirectsInfoProvider>
    {
        /// <summary>
        /// Creates an instance of <see cref="RegexRedirectsInfoProvider"/>.
        /// </summary>
        public RegexRedirectsInfoProvider()
            : base(RegexRedirectsInfo.TYPEINFO)
        {
        }


        /// <summary>
        /// Returns a query for all the <see cref="RegexRedirectsInfo"/> objects.
        /// </summary>
        public static ObjectQuery<RegexRedirectsInfo> GetRegexRedirects()
        {
            return ProviderObject.GetObjectQuery();
        }


        /// <summary>
        /// Returns <see cref="RegexRedirectsInfo"/> with specified ID.
        /// </summary>
        /// <param name="id"><see cref="RegexRedirectsInfo"/> ID.</param>
        public static RegexRedirectsInfo GetRegexRedirectsInfo(int id)
        {
            return ProviderObject.GetInfoById(id);
        }


        /// <summary>
        /// Sets (updates or inserts) specified <see cref="RegexRedirectsInfo"/>.
        /// </summary>
        /// <param name="infoObj"><see cref="RegexRedirectsInfo"/> to be set.</param>
        public static void SetRegexRedirectsInfo(RegexRedirectsInfo infoObj)
        {
            ProviderObject.SetInfo(infoObj);
        }


        /// <summary>
        /// Deletes specified <see cref="RegexRedirectsInfo"/>.
        /// </summary>
        /// <param name="infoObj"><see cref="RegexRedirectsInfo"/> to be deleted.</param>
        public static void DeleteRegexRedirectsInfo(RegexRedirectsInfo infoObj)
        {
            ProviderObject.DeleteInfo(infoObj);
        }


        /// <summary>
        /// Deletes <see cref="RegexRedirectsInfo"/> with specified ID.
        /// </summary>
        /// <param name="id"><see cref="RegexRedirectsInfo"/> ID.</param>
        public static void DeleteRegexRedirectsInfo(int id)
        {
            RegexRedirectsInfo infoObj = GetRegexRedirectsInfo(id);
            DeleteRegexRedirectsInfo(infoObj);
        }
    }
}