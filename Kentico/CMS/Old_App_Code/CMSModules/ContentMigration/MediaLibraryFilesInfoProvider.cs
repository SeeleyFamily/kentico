using System;
using System.Data;

using CMS.Base;
using CMS.DataEngine;
using CMS.Helpers;

namespace ContentMigration
{
    /// <summary>
    /// Class providing <see cref="MediaLibraryFilesInfo"/> management.
    /// </summary>
    public partial class MediaLibraryFilesInfoProvider : AbstractInfoProvider<MediaLibraryFilesInfo, MediaLibraryFilesInfoProvider>
    {
        /// <summary>
        /// Creates an instance of <see cref="MediaLibraryFilesInfoProvider"/>.
        /// </summary>
        public MediaLibraryFilesInfoProvider()
            : base(MediaLibraryFilesInfo.TYPEINFO)
        {
        }


        /// <summary>
        /// Returns a query for all the <see cref="MediaLibraryFilesInfo"/> objects.
        /// </summary>
        public static ObjectQuery<MediaLibraryFilesInfo> GetMediaLibraryFiles()
        {
            return ProviderObject.GetObjectQuery();
        }


        /// <summary>
        /// Returns <see cref="MediaLibraryFilesInfo"/> with specified ID.
        /// </summary>
        /// <param name="id"><see cref="MediaLibraryFilesInfo"/> ID.</param>
        public static MediaLibraryFilesInfo GetMediaLibraryFilesInfo(int id)
        {
            return ProviderObject.GetInfoById(id);
        }


        /// <summary>
        /// Returns <see cref="MediaLibraryFilesInfo"/> with specified name.
        /// </summary>
        /// <param name="name"><see cref="MediaLibraryFilesInfo"/> name.</param>
        public static MediaLibraryFilesInfo GetMediaLibraryFilesInfo(string name)
        {
            return ProviderObject.GetInfoByCodeName(name);
        }


        /// <summary>
        /// Sets (updates or inserts) specified <see cref="MediaLibraryFilesInfo"/>.
        /// </summary>
        /// <param name="infoObj"><see cref="MediaLibraryFilesInfo"/> to be set.</param>
        public static void SetMediaLibraryFilesInfo(MediaLibraryFilesInfo infoObj)
        {
            ProviderObject.SetInfo(infoObj);
        }


        /// <summary>
        /// Deletes specified <see cref="MediaLibraryFilesInfo"/>.
        /// </summary>
        /// <param name="infoObj"><see cref="MediaLibraryFilesInfo"/> to be deleted.</param>
        public static void DeleteMediaLibraryFilesInfo(MediaLibraryFilesInfo infoObj)
        {
            ProviderObject.DeleteInfo(infoObj);
        }


        /// <summary>
        /// Deletes <see cref="MediaLibraryFilesInfo"/> with specified ID.
        /// </summary>
        /// <param name="id"><see cref="MediaLibraryFilesInfo"/> ID.</param>
        public static void DeleteMediaLibraryFilesInfo(int id)
        {
            MediaLibraryFilesInfo infoObj = GetMediaLibraryFilesInfo(id);
            DeleteMediaLibraryFilesInfo(infoObj);
        }
    }
}