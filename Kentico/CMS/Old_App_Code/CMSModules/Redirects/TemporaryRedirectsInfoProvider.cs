using System;
using System.Data;

using CMS.Base;
using CMS.DataEngine;
using CMS.Helpers;
using CMS.SiteProvider;

namespace CMS.Module.Redirects
{
    /// <summary>
    /// Class providing <see cref="TemporaryRedirectsInfo"/> management.
    /// </summary>
    public partial class TemporaryRedirectsInfoProvider : AbstractInfoProvider<TemporaryRedirectsInfo, TemporaryRedirectsInfoProvider>
    {
        /// <summary>
        /// Creates an instance of <see cref="TemporaryRedirectsInfoProvider"/>.
        /// </summary>
        public TemporaryRedirectsInfoProvider()
            : base(TemporaryRedirectsInfo.TYPEINFO)
        {
        }


        /// <summary>
        /// Returns a query for all the <see cref="TemporaryRedirectsInfo"/> objects.
        /// </summary>
        public static ObjectQuery<TemporaryRedirectsInfo> GetTemporaryRedirects()
        {
            return ProviderObject.GetObjectQuery();
        }


        /// <summary>
        /// Returns <see cref="TemporaryRedirectsInfo"/> with specified ID.
        /// </summary>
        /// <param name="id"><see cref="TemporaryRedirectsInfo"/> ID.</param>
        public static TemporaryRedirectsInfo GetTemporaryRedirectsInfo(int id)
        {
            return ProviderObject.GetInfoById(id);
        }


        /// <summary>
        /// Sets (updates or inserts) specified <see cref="TemporaryRedirectsInfo"/>.
        /// </summary>
        /// <param name="infoObj"><see cref="TemporaryRedirectsInfo"/> to be set.</param>
        public static void SetTemporaryRedirectsInfo(TemporaryRedirectsInfo infoObj)
        {
            ProviderObject.SetInfo(infoObj);
        }


        /// <summary>
        /// Deletes specified <see cref="TemporaryRedirectsInfo"/>.
        /// </summary>
        /// <param name="infoObj"><see cref="TemporaryRedirectsInfo"/> to be deleted.</param>
        public static void DeleteTemporaryRedirectsInfo(TemporaryRedirectsInfo infoObj)
        {
            ProviderObject.DeleteInfo(infoObj);
        }


        /// <summary>
        /// Deletes <see cref="TemporaryRedirectsInfo"/> with specified ID.
        /// </summary>
        /// <param name="id"><see cref="TemporaryRedirectsInfo"/> ID.</param>
        public static void DeleteTemporaryRedirectsInfo(int id)
        {
            TemporaryRedirectsInfo infoObj = GetTemporaryRedirectsInfo(id);
            DeleteTemporaryRedirectsInfo(infoObj);
        }
    }
}