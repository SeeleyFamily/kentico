using System;
using System.Data;
using System.Runtime.Serialization;

using CMS;
using CMS.DataEngine;
using CMS.Helpers;
using ContentMigration;

[assembly: RegisterObjectType(typeof(MediaLibraryFilesInfo), MediaLibraryFilesInfo.OBJECT_TYPE)]

namespace ContentMigration
{
    /// <summary>
    /// Data container class for <see cref="MediaLibraryFilesInfo"/>.
    /// </summary>
    [Serializable]
    public partial class MediaLibraryFilesInfo : AbstractInfo<MediaLibraryFilesInfo>
    {
        /// <summary>
        /// Object type.
        /// </summary>
        public const string OBJECT_TYPE = "contentmigration.medialibraryfiles";


        /// <summary>
        /// Type information.
        /// </summary>
#warning "You will need to configure the type info."
        public static readonly ObjectTypeInfo TYPEINFO = new ObjectTypeInfo(typeof(MediaLibraryFilesInfoProvider), OBJECT_TYPE, "ContentMigration.MediaLibraryFiles", "MediaLibraryFilesID", "MediaLibraryFilesLastModified", "MediaLibraryFilesGuid", "FileName", null, null, null, null, null)
        {
            ModuleName = "ContentMigration",
            TouchCacheDependencies = true,
        };


        /// <summary>
        /// Media library files ID.
        /// </summary>
        [DatabaseField]
        public virtual int MediaLibraryFilesID
        {
            get
            {
                return ValidationHelper.GetInteger(GetValue("MediaLibraryFilesID"), 0);
            }
            set
            {
                SetValue("MediaLibraryFilesID", value);
            }
        }


        /// <summary>
        /// File name.
        /// </summary>
        [DatabaseField]
        public virtual string FileName
        {
            get
            {
                return ValidationHelper.GetString(GetValue("FileName"), String.Empty);
            }
            set
            {
                SetValue("FileName", value, String.Empty);
            }
        }


        /// <summary>
        /// File title.
        /// </summary>
        [DatabaseField]
        public virtual string FileTitle
        {
            get
            {
                return ValidationHelper.GetString(GetValue("FileTitle"), String.Empty);
            }
            set
            {
                SetValue("FileTitle", value, String.Empty);
            }
        }


        /// <summary>
        /// File description.
        /// </summary>
        [DatabaseField]
        public virtual string FileDescription
        {
            get
            {
                return ValidationHelper.GetString(GetValue("FileDescription"), String.Empty);
            }
            set
            {
                SetValue("FileDescription", value, String.Empty);
            }
        }


        /// <summary>
        /// File path.
        /// </summary>
        [DatabaseField]
        public virtual string FilePath
        {
            get
            {
                return ValidationHelper.GetString(GetValue("FilePath"), String.Empty);
            }
            set
            {
                SetValue("FilePath", value, String.Empty);
            }
        }


        /// <summary>
        /// File full path.
        /// </summary>
        [DatabaseField]
        public virtual string FileFullPath
        {
            get
            {
                return ValidationHelper.GetString(GetValue("FileFullPath"), String.Empty);
            }
            set
            {
                SetValue("FileFullPath", value, String.Empty);
            }
        }


        /// <summary>
        /// File created when.
        /// </summary>
        [DatabaseField]
        public virtual DateTime FileCreatedWhen
        {
            get
            {
                return ValidationHelper.GetDateTime(GetValue("FileCreatedWhen"), DateTimeHelper.ZERO_TIME);
            }
            set
            {
                SetValue("FileCreatedWhen", value, DateTimeHelper.ZERO_TIME);
            }
        }


        /// <summary>
        /// File custom data.
        /// </summary>
        [DatabaseField]
        public virtual string FileCustomData
        {
            get
            {
                return ValidationHelper.GetString(GetValue("FileCustomData"), String.Empty);
            }
            set
            {
                SetValue("FileCustomData", value, String.Empty);
            }
        }


        /// <summary>
        /// Media library files guid.
        /// </summary>
        [DatabaseField]
        public virtual Guid MediaLibraryFilesGuid
        {
            get
            {
                return ValidationHelper.GetGuid(GetValue("MediaLibraryFilesGuid"), Guid.Empty);
            }
            set
            {
                SetValue("MediaLibraryFilesGuid", value);
            }
        }


        /// <summary>
        /// Media library files last modified.
        /// </summary>
        [DatabaseField]
        public virtual DateTime MediaLibraryFilesLastModified
        {
            get
            {
                return ValidationHelper.GetDateTime(GetValue("MediaLibraryFilesLastModified"), DateTimeHelper.ZERO_TIME);
            }
            set
            {
                SetValue("MediaLibraryFilesLastModified", value);
            }
        }


        /// <summary>
        /// Deletes the object using appropriate provider.
        /// </summary>
        protected override void DeleteObject()
        {
            MediaLibraryFilesInfoProvider.DeleteMediaLibraryFilesInfo(this);
        }


        /// <summary>
        /// Updates the object using appropriate provider.
        /// </summary>
        protected override void SetObject()
        {
            MediaLibraryFilesInfoProvider.SetMediaLibraryFilesInfo(this);
        }


        /// <summary>
        /// Constructor for de-serialization.
        /// </summary>
        /// <param name="info">Serialization info.</param>
        /// <param name="context">Streaming context.</param>
        protected MediaLibraryFilesInfo(SerializationInfo info, StreamingContext context)
            : base(info, context, TYPEINFO)
        {
        }


        /// <summary>
        /// Creates an empty instance of the <see cref="MediaLibraryFilesInfo"/> class.
        /// </summary>
        public MediaLibraryFilesInfo()
            : base(TYPEINFO)
        {
        }


        /// <summary>
        /// Creates a new instances of the <see cref="MediaLibraryFilesInfo"/> class from the given <see cref="DataRow"/>.
        /// </summary>
        /// <param name="dr">DataRow with the object data.</param>
        public MediaLibraryFilesInfo(DataRow dr)
            : base(TYPEINFO, dr)
        {
        }
    }
}