using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;

using CMS;
using CMS.DataEngine;
using CMS.Helpers;
using CMS.Module.Redirects;

[assembly: RegisterObjectType(typeof(TemporaryRedirectsInfo), TemporaryRedirectsInfo.OBJECT_TYPE)]

namespace CMS.Module.Redirects
{
    /// <summary>
    /// Data container class for <see cref="TemporaryRedirectsInfo"/>.
    /// </summary>
    [Serializable]
    public partial class TemporaryRedirectsInfo : AbstractInfo<TemporaryRedirectsInfo>
    {
        /// <summary>
        /// Object type.
        /// </summary>
        public const string OBJECT_TYPE = "redirects.temporaryredirects";


        /// <summary>
        /// Type information.
        /// </summary>
        public static readonly ObjectTypeInfo TYPEINFO = new ObjectTypeInfo(typeof(TemporaryRedirectsInfoProvider), OBJECT_TYPE, "Redirects.TemporaryRedirects", "TemporaryRedirectsID", "TemporaryRedirectsLastModified", "TemporaryRedirectsGuid", null, null, null, "SiteID", null, null)
        {
            ModuleName = "Redirects",
            TouchCacheDependencies = true,
            SynchronizationSettings =
            {
                LogSynchronization = SynchronizationTypeEnum.LogSynchronization,
                ObjectTreeLocations = new List<ObjectTreeLocation>()
                {
                    new ObjectTreeLocation(GLOBAL, "Temporary Redirects")
                },
            }
        };


        /// <summary>
        /// Temporary redirects ID.
        /// </summary>
        [DatabaseField]
        public virtual int TemporaryRedirectsID
        {
            get
            {
                return ValidationHelper.GetInteger(GetValue("TemporaryRedirectsID"), 0);
            }
            set
            {
                SetValue("TemporaryRedirectsID", value);
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
        /// Site ID.
        /// </summary>
        [DatabaseField]
        public virtual int SiteID
        {
            get
            {
                return ValidationHelper.GetInteger(GetValue("SiteID"), 0);
            }
            set
            {
                SetValue("SiteID", value);
            }
        }


        /// <summary>
        /// Temporary redirects guid.
        /// </summary>
        [DatabaseField]
        public virtual Guid TemporaryRedirectsGuid
        {
            get
            {
                return ValidationHelper.GetGuid(GetValue("TemporaryRedirectsGuid"), Guid.Empty);
            }
            set
            {
                SetValue("TemporaryRedirectsGuid", value);
            }
        }


        /// <summary>
        /// Temporary redirects last modified.
        /// </summary>
        [DatabaseField]
        public virtual DateTime TemporaryRedirectsLastModified
        {
            get
            {
                return ValidationHelper.GetDateTime(GetValue("TemporaryRedirectsLastModified"), DateTimeHelper.ZERO_TIME);
            }
            set
            {
                SetValue("TemporaryRedirectsLastModified", value);
            }
        }


        /// <summary>
        /// Deletes the object using appropriate provider.
        /// </summary>
        protected override void DeleteObject()
        {
            TemporaryRedirectsInfoProvider.DeleteTemporaryRedirectsInfo(this);
        }


        /// <summary>
        /// Updates the object using appropriate provider.
        /// </summary>
        protected override void SetObject()
        {
            TemporaryRedirectsInfoProvider.SetTemporaryRedirectsInfo(this);
        }


        /// <summary>
        /// Constructor for de-serialization.
        /// </summary>
        /// <param name="info">Serialization info.</param>
        /// <param name="context">Streaming context.</param>
        protected TemporaryRedirectsInfo(SerializationInfo info, StreamingContext context)
            : base(info, context, TYPEINFO)
        {
        }


        /// <summary>
        /// Creates an empty instance of the <see cref="TemporaryRedirectsInfo"/> class.
        /// </summary>
        public TemporaryRedirectsInfo()
            : base(TYPEINFO)
        {
        }


        /// <summary>
        /// Creates a new instances of the <see cref="TemporaryRedirectsInfo"/> class from the given <see cref="DataRow"/>.
        /// </summary>
        /// <param name="dr">DataRow with the object data.</param>
        public TemporaryRedirectsInfo(DataRow dr)
            : base(TYPEINFO, dr)
        {
        }
    }
}