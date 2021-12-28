using System;
using System.Data;

using CMS.Base;
using CMS.DataEngine;
using CMS.Helpers;
using CMS.SiteProvider;

namespace CMS.Module.Redirects
{
    /// <summary>
    /// Class providing <see cref="PermanentRedirectsInfo"/> management.
    /// </summary>
    public partial class PermanentRedirectsInfoProvider : AbstractInfoProvider<PermanentRedirectsInfo, PermanentRedirectsInfoProvider>
    {
        /// <summary>
        /// Creates an instance of <see cref="PermanentRedirectsInfoProvider"/>.
        /// </summary>
        public PermanentRedirectsInfoProvider()
            : base(PermanentRedirectsInfo.TYPEINFO)
        {
        }


        /// <summary>
        /// Returns a query for all the <see cref="PermanentRedirectsInfo"/> objects.
        /// </summary>
        public static ObjectQuery<PermanentRedirectsInfo> GetPermanentRedirects()
        {
            return ProviderObject.GetObjectQuery();
        }


        /// <summary>
        /// Returns <see cref="PermanentRedirectsInfo"/> with specified ID.
        /// </summary>
        /// <param name="id"><see cref="PermanentRedirectsInfo"/> ID.</param>
        public static PermanentRedirectsInfo GetPermanentRedirectsInfo(int id)
        {
            return ProviderObject.GetInfoById(id);
        }


        /// <summary>
        /// Sets (updates or inserts) specified <see cref="PermanentRedirectsInfo"/>.
        /// </summary>
        /// <param name="infoObj"><see cref="PermanentRedirectsInfo"/> to be set.</param>
        public static void SetPermanentRedirectsInfo(PermanentRedirectsInfo infoObj)
        {
            ProviderObject.SetInfo(infoObj);
        }


        /// <summary>
        /// Deletes specified <see cref="PermanentRedirectsInfo"/>.
        /// </summary>
        /// <param name="infoObj"><see cref="PermanentRedirectsInfo"/> to be deleted.</param>
        public static void DeletePermanentRedirectsInfo(PermanentRedirectsInfo infoObj)
        {
            ProviderObject.DeleteInfo(infoObj);
        }


        /// <summary>
        /// Deletes <see cref="PermanentRedirectsInfo"/> with specified ID.
        /// </summary>
        /// <param name="id"><see cref="PermanentRedirectsInfo"/> ID.</param>
        public static void DeletePermanentRedirectsInfo(int id)
        {
            PermanentRedirectsInfo infoObj = GetPermanentRedirectsInfo(id);
            DeletePermanentRedirectsInfo(infoObj);
        }
    }
}