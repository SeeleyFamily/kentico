using System;
using System.Data;
using System.Runtime.Serialization;

using CMS;
using CMS.DataEngine;
using CMS.Helpers;
using ContentMigration;

[assembly: RegisterObjectType(typeof(RedirectsInfo), RedirectsInfo.OBJECT_TYPE)]

namespace ContentMigration
{
    /// <summary>
    /// Data container class for <see cref="RedirectsInfo"/>.
    /// </summary>
    [Serializable]
    public partial class RedirectsInfo : AbstractInfo<RedirectsInfo>
    {
        /// <summary>
        /// Object type.
        /// </summary>
        public const string OBJECT_TYPE = "contentmigration.redirects";


        /// <summary>
        /// Type information.
        /// </summary>
#warning "You will need to configure the type info."
        public static readonly ObjectTypeInfo TYPEINFO = new ObjectTypeInfo(typeof(RedirectsInfoProvider), OBJECT_TYPE, "ContentMigration.Redirects", "RedirectsID", "RedirectsLastModified", "RedirectsGuid", null, null, null, null, null, null)
        {
            ModuleName = "ContentMigration",
            TouchCacheDependencies = true,
        };


        /// <summary>
        /// Redirects ID.
        /// </summary>
        [DatabaseField]
        public virtual int RedirectsID
        {
            get
            {
                return ValidationHelper.GetInteger(GetValue("RedirectsID"), 0);
            }
            set
            {
                SetValue("RedirectsID", value);
            }
        }


        /// <summary>
        /// Match url.
        /// </summary>
        [DatabaseField]
        public virtual string MatchUrl
        {
            get
            {
                return ValidationHelper.GetString(GetValue("MatchUrl"), String.Empty);
            }
            set
            {
                SetValue("MatchUrl", value);
            }
        }


        /// <summary>
        /// Redirect url.
        /// </summary>
        [DatabaseField]
        public virtual string RedirectUrl
        {
            get
            {
                return ValidationHelper.GetString(GetValue("RedirectUrl"), String.Empty);
            }
            set
            {
                SetValue("RedirectUrl", value);
            }
        }


        /// <summary>
        /// Redirects guid.
        /// </summary>
        [DatabaseField]
        public virtual Guid RedirectsGuid
        {
            get
            {
                return ValidationHelper.GetGuid(GetValue("RedirectsGuid"), Guid.Empty);
            }
            set
            {
                SetValue("RedirectsGuid", value);
            }
        }


        /// <summary>
        /// Redirects last modified.
        /// </summary>
        [DatabaseField]
        public virtual DateTime RedirectsLastModified
        {
            get
            {
                return ValidationHelper.GetDateTime(GetValue("RedirectsLastModified"), DateTimeHelper.ZERO_TIME);
            }
            set
            {
                SetValue("RedirectsLastModified", value);
            }
        }


        /// <summary>
        /// Deletes the object using appropriate provider.
        /// </summary>
        protected override void DeleteObject()
        {
            RedirectsInfoProvider.DeleteRedirectsInfo(this);
        }


        /// <summary>
        /// Updates the object using appropriate provider.
        /// </summary>
        protected override void SetObject()
        {
            RedirectsInfoProvider.SetRedirectsInfo(this);
        }


        /// <summary>
        /// Constructor for de-serialization.
        /// </summary>
        /// <param name="info">Serialization info.</param>
        /// <param name="context">Streaming context.</param>
        protected RedirectsInfo(SerializationInfo info, StreamingContext context)
            : base(info, context, TYPEINFO)
        {
        }


        /// <summary>
        /// Creates an empty instance of the <see cref="RedirectsInfo"/> class.
        /// </summary>
        public RedirectsInfo()
            : base(TYPEINFO)
        {
        }


        /// <summary>
        /// Creates a new instances of the <see cref="RedirectsInfo"/> class from the given <see cref="DataRow"/>.
        /// </summary>
        /// <param name="dr">DataRow with the object data.</param>
        public RedirectsInfo(DataRow dr)
            : base(TYPEINFO, dr)
        {
        }
    }
}