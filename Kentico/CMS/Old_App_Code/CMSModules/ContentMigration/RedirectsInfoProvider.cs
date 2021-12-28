using System;
using System.Data;

using CMS.Base;
using CMS.DataEngine;
using CMS.Helpers;

namespace ContentMigration
{
    /// <summary>
    /// Class providing <see cref="RedirectsInfo"/> management.
    /// </summary>
    public partial class RedirectsInfoProvider : AbstractInfoProvider<RedirectsInfo, RedirectsInfoProvider>
    {
        /// <summary>
        /// Creates an instance of <see cref="RedirectsInfoProvider"/>.
        /// </summary>
        public RedirectsInfoProvider()
            : base(RedirectsInfo.TYPEINFO)
        {
        }


        /// <summary>
        /// Returns a query for all the <see cref="RedirectsInfo"/> objects.
        /// </summary>
        public static ObjectQuery<RedirectsInfo> GetRedirects()
        {
            return ProviderObject.GetObjectQuery();
        }


        /// <summary>
        /// Returns <see cref="RedirectsInfo"/> with specified ID.
        /// </summary>
        /// <param name="id"><see cref="RedirectsInfo"/> ID.</param>
        public static RedirectsInfo GetRedirectsInfo(int id)
        {
            return ProviderObject.GetInfoById(id);
        }


        /// <summary>
        /// Sets (updates or inserts) specified <see cref="RedirectsInfo"/>.
        /// </summary>
        /// <param name="infoObj"><see cref="RedirectsInfo"/> to be set.</param>
        public static void SetRedirectsInfo(RedirectsInfo infoObj)
        {
            ProviderObject.SetInfo(infoObj);
        }


        /// <summary>
        /// Deletes specified <see cref="RedirectsInfo"/>.
        /// </summary>
        /// <param name="infoObj"><see cref="RedirectsInfo"/> to be deleted.</param>
        public static void DeleteRedirectsInfo(RedirectsInfo infoObj)
        {
            ProviderObject.DeleteInfo(infoObj);
        }


        /// <summary>
        /// Deletes <see cref="RedirectsInfo"/> with specified ID.
        /// </summary>
        /// <param name="id"><see cref="RedirectsInfo"/> ID.</param>
        public static void DeleteRedirectsInfo(int id)
        {
            RedirectsInfo infoObj = GetRedirectsInfo(id);
            DeleteRedirectsInfo(infoObj);
        }
    }
}