using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;

using CMS;
using CMS.DataEngine;
using CMS.Helpers;
using CMS.Module.Redirects;

[assembly: RegisterObjectType(typeof(PermanentRedirectsInfo), PermanentRedirectsInfo.OBJECT_TYPE)]

namespace CMS.Module.Redirects
{
    /// <summary>
    /// Data container class for <see cref="PermanentRedirectsInfo"/>.
    /// </summary>
    [Serializable]
    public partial class PermanentRedirectsInfo : AbstractInfo<PermanentRedirectsInfo>
    {
        /// <summary>
        /// Object type.
        /// </summary>
        public const string OBJECT_TYPE = "redirects.permanentredirects";


        /// <summary>
        /// Type information.
        /// </summary>
        public static readonly ObjectTypeInfo TYPEINFO = new ObjectTypeInfo(typeof(PermanentRedirectsInfoProvider), OBJECT_TYPE, "Redirects.PermanentRedirects", "PermanentRedirectsID", "PermanentRedirectsLastModified", "PermanentRedirectsGuid", null, null, null, "SiteID", null, null)
        {
            ModuleName = "Redirects",
            TouchCacheDependencies = true,
            SynchronizationSettings =
            {
                LogSynchronization = SynchronizationTypeEnum.LogSynchronization,
                ObjectTreeLocations = new List<ObjectTreeLocation>()
                {
                    new ObjectTreeLocation(GLOBAL, "Permanent Redirects")
                },
            }
        };


        /// <summary>
        /// Permanent redirects ID.
        /// </summary>
        [DatabaseField]
        public virtual int PermanentRedirectsID
        {
            get
            {
                return ValidationHelper.GetInteger(GetValue("PermanentRedirectsID"), 0);
            }
            set
            {
                SetValue("PermanentRedirectsID", value);
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
        /// Permanent redirects guid.
        /// </summary>
        [DatabaseField]
        public virtual Guid PermanentRedirectsGuid
        {
            get
            {
                return ValidationHelper.GetGuid(GetValue("PermanentRedirectsGuid"), Guid.Empty);
            }
            set
            {
                SetValue("PermanentRedirectsGuid", value);
            }
        }


        /// <summary>
        /// Permanent redirects last modified.
        /// </summary>
        [DatabaseField]
        public virtual DateTime PermanentRedirectsLastModified
        {
            get
            {
                return ValidationHelper.GetDateTime(GetValue("PermanentRedirectsLastModified"), DateTimeHelper.ZERO_TIME);
            }
            set
            {
                SetValue("PermanentRedirectsLastModified", value);
            }
        }


        /// <summary>
        /// Deletes the object using appropriate provider.
        /// </summary>
        protected override void DeleteObject()
        {
            PermanentRedirectsInfoProvider.DeletePermanentRedirectsInfo(this);
        }


        /// <summary>
        /// Updates the object using appropriate provider.
        /// </summary>
        protected override void SetObject()
        {
            PermanentRedirectsInfoProvider.SetPermanentRedirectsInfo(this);
        }


        /// <summary>
        /// Constructor for de-serialization.
        /// </summary>
        /// <param name="info">Serialization info.</param>
        /// <param name="context">Streaming context.</param>
        protected PermanentRedirectsInfo(SerializationInfo info, StreamingContext context)
            : base(info, context, TYPEINFO)
        {
        }


        /// <summary>
        /// Creates an empty instance of the <see cref="PermanentRedirectsInfo"/> class.
        /// </summary>
        public PermanentRedirectsInfo()
            : base(TYPEINFO)
        {
        }


        /// <summary>
        /// Creates a new instances of the <see cref="PermanentRedirectsInfo"/> class from the given <see cref="DataRow"/>.
        /// </summary>
        /// <param name="dr">DataRow with the object data.</param>
        public PermanentRedirectsInfo(DataRow dr)
            : base(TYPEINFO, dr)
        {
        }
    }
}