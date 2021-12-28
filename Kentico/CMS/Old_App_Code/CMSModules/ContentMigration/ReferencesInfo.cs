using System;
using System.Data;
using System.Runtime.Serialization;

using CMS;
using CMS.DataEngine;
using CMS.Helpers;
using ContentMigration;

[assembly: RegisterObjectType(typeof(ReferencesInfo), ReferencesInfo.OBJECT_TYPE)]

namespace ContentMigration
{
    /// <summary>
    /// Data container class for <see cref="ReferencesInfo"/>.
    /// </summary>
    [Serializable]
    public partial class ReferencesInfo : AbstractInfo<ReferencesInfo>
    {
        /// <summary>
        /// Object type.
        /// </summary>
        public const string OBJECT_TYPE = "contentmigration.references";


        /// <summary>
        /// Type information.
        /// </summary>
#warning "You will need to configure the type info."
        public static readonly ObjectTypeInfo TYPEINFO = new ObjectTypeInfo(typeof(ReferencesInfoProvider), OBJECT_TYPE, "ContentMigration.References", "ReferencesID", "ReferencesLastModified", "ReferencesGuid", null, null, null, null, null, null)
        {
            ModuleName = "ContentMigration",
            TouchCacheDependencies = true,
        };


        /// <summary>
        /// References ID.
        /// </summary>
        [DatabaseField]
        public virtual int ReferencesID
        {
            get
            {
                return ValidationHelper.GetInteger(GetValue("ReferencesID"), 0);
            }
            set
            {
                SetValue("ReferencesID", value);
            }
        }


        /// <summary>
        /// Old reference.
        /// </summary>
        [DatabaseField]
        public virtual string OldReference
        {
            get
            {
                return ValidationHelper.GetString(GetValue("OldReference"), String.Empty);
            }
            set
            {
                SetValue("OldReference", value, String.Empty);
            }
        }


        /// <summary>
        /// New reference.
        /// </summary>
        [DatabaseField]
        public virtual string NewReference
        {
            get
            {
                return ValidationHelper.GetString(GetValue("NewReference"), String.Empty);
            }
            set
            {
                SetValue("NewReference", value, String.Empty);
            }
        }


        /// <summary>
        /// Reference type.
        /// </summary>
        [DatabaseField]
        public virtual string ReferenceType
        {
            get
            {
                return ValidationHelper.GetString(GetValue("ReferenceType"), String.Empty);
            }
            set
            {
                SetValue("ReferenceType", value, String.Empty);
            }
        }


        /// <summary>
        /// References guid.
        /// </summary>
        [DatabaseField]
        public virtual Guid ReferencesGuid
        {
            get
            {
                return ValidationHelper.GetGuid(GetValue("ReferencesGuid"), Guid.Empty);
            }
            set
            {
                SetValue("ReferencesGuid", value);
            }
        }


        /// <summary>
        /// References last modified.
        /// </summary>
        [DatabaseField]
        public virtual DateTime ReferencesLastModified
        {
            get
            {
                return ValidationHelper.GetDateTime(GetValue("ReferencesLastModified"), DateTimeHelper.ZERO_TIME);
            }
            set
            {
                SetValue("ReferencesLastModified", value);
            }
        }


        /// <summary>
        /// Deletes the object using appropriate provider.
        /// </summary>
        protected override void DeleteObject()
        {
            ReferencesInfoProvider.DeleteReferencesInfo(this);
        }


        /// <summary>
        /// Updates the object using appropriate provider.
        /// </summary>
        protected override void SetObject()
        {
            ReferencesInfoProvider.SetReferencesInfo(this);
        }


        /// <summary>
        /// Constructor for de-serialization.
        /// </summary>
        /// <param name="info">Serialization info.</param>
        /// <param name="context">Streaming context.</param>
        protected ReferencesInfo(SerializationInfo info, StreamingContext context)
            : base(info, context, TYPEINFO)
        {
        }


        /// <summary>
        /// Creates an empty instance of the <see cref="ReferencesInfo"/> class.
        /// </summary>
        public ReferencesInfo()
            : base(TYPEINFO)
        {
        }


        /// <summary>
        /// Creates a new instances of the <see cref="ReferencesInfo"/> class from the given <see cref="DataRow"/>.
        /// </summary>
        /// <param name="dr">DataRow with the object data.</param>
        public ReferencesInfo(DataRow dr)
            : base(TYPEINFO, dr)
        {
        }
    }
}