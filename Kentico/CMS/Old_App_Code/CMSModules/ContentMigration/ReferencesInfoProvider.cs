using System;
using System.Data;

using CMS.Base;
using CMS.DataEngine;
using CMS.Helpers;

namespace ContentMigration
{
    /// <summary>
    /// Class providing <see cref="ReferencesInfo"/> management.
    /// </summary>
    public partial class ReferencesInfoProvider : AbstractInfoProvider<ReferencesInfo, ReferencesInfoProvider>
    {
        /// <summary>
        /// Creates an instance of <see cref="ReferencesInfoProvider"/>.
        /// </summary>
        public ReferencesInfoProvider()
            : base(ReferencesInfo.TYPEINFO)
        {
        }


        /// <summary>
        /// Returns a query for all the <see cref="ReferencesInfo"/> objects.
        /// </summary>
        public static ObjectQuery<ReferencesInfo> GetReferences()
        {
            return ProviderObject.GetObjectQuery();
        }


        /// <summary>
        /// Returns <see cref="ReferencesInfo"/> with specified ID.
        /// </summary>
        /// <param name="id"><see cref="ReferencesInfo"/> ID.</param>
        public static ReferencesInfo GetReferencesInfo(int id)
        {
            return ProviderObject.GetInfoById(id);
        }


        /// <summary>
        /// Sets (updates or inserts) specified <see cref="ReferencesInfo"/>.
        /// </summary>
        /// <param name="infoObj"><see cref="ReferencesInfo"/> to be set.</param>
        public static void SetReferencesInfo(ReferencesInfo infoObj)
        {
            ProviderObject.SetInfo(infoObj);
        }


        /// <summary>
        /// Deletes specified <see cref="ReferencesInfo"/>.
        /// </summary>
        /// <param name="infoObj"><see cref="ReferencesInfo"/> to be deleted.</param>
        public static void DeleteReferencesInfo(ReferencesInfo infoObj)
        {
            ProviderObject.DeleteInfo(infoObj);
        }


        /// <summary>
        /// Deletes <see cref="ReferencesInfo"/> with specified ID.
        /// </summary>
        /// <param name="id"><see cref="ReferencesInfo"/> ID.</param>
        public static void DeleteReferencesInfo(int id)
        {
            ReferencesInfo infoObj = GetReferencesInfo(id);
            DeleteReferencesInfo(infoObj);
        }
    }
}