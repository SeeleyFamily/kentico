using System;
using System.Data;
using System.Runtime.Serialization;

using CMS;
using CMS.DataEngine;
using CMS.Helpers;
using CMS.Module.Redirects;

[assembly: RegisterObjectType(typeof(RegexRedirectsInfo), RegexRedirectsInfo.OBJECT_TYPE)]

namespace CMS.Module.Redirects
{
    /// <summary>
    /// Data container class for <see cref="RegexRedirectsInfo"/>.
    /// </summary>
    [Serializable]
    public partial class RegexRedirectsInfo : AbstractInfo<RegexRedirectsInfo>
    {
        /// <summary>
        /// Object type.
        /// </summary>
        public const string OBJECT_TYPE = "redirects.regexredirects";


        /// <summary>
        /// Type information.
        /// </summary>
#warning "You will need to configure the type info."
        public static readonly ObjectTypeInfo TYPEINFO = new ObjectTypeInfo(typeof(RegexRedirectsInfoProvider), OBJECT_TYPE, "Redirects.RegexRedirects", "RegexRedirectsID", "RegexRedirectsLastModified", "RegexRedirectsGuid", null, null, null, "SiteID", null, null)
        {
            ModuleName = "Redirects",
            TouchCacheDependencies = true,
        };


        /// <summary>
        /// Regex redirects ID.
        /// </summary>
        [DatabaseField]
        public virtual int RegexRedirectsID
        {
            get
            {
                return ValidationHelper.GetInteger(GetValue("RegexRedirectsID"), 0);
            }
            set
            {
                SetValue("RegexRedirectsID", value);
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
        /// Regex replace.
        /// </summary>
        [DatabaseField]
        public virtual bool RegexReplace
        {
            get
            {
                return ValidationHelper.GetBoolean(GetValue("RegexReplace"), false);
            }
            set
            {
                SetValue("RegexReplace", value);
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
        /// Regex redirects guid.
        /// </summary>
        [DatabaseField]
        public virtual Guid RegexRedirectsGuid
        {
            get
            {
                return ValidationHelper.GetGuid(GetValue("RegexRedirectsGuid"), Guid.Empty);
            }
            set
            {
                SetValue("RegexRedirectsGuid", value);
            }
        }


        /// <summary>
        /// Regex redirects last modified.
        /// </summary>
        [DatabaseField]
        public virtual DateTime RegexRedirectsLastModified
        {
            get
            {
                return ValidationHelper.GetDateTime(GetValue("RegexRedirectsLastModified"), DateTimeHelper.ZERO_TIME);
            }
            set
            {
                SetValue("RegexRedirectsLastModified", value);
            }
        }


        /// <summary>
        /// Deletes the object using appropriate provider.
        /// </summary>
        protected override void DeleteObject()
        {
            RegexRedirectsInfoProvider.DeleteRegexRedirectsInfo(this);
        }


        /// <summary>
        /// Updates the object using appropriate provider.
        /// </summary>
        protected override void SetObject()
        {
            RegexRedirectsInfoProvider.SetRegexRedirectsInfo(this);
        }


        /// <summary>
        /// Constructor for de-serialization.
        /// </summary>
        /// <param name="info">Serialization info.</param>
        /// <param name="context">Streaming context.</param>
        protected RegexRedirectsInfo(SerializationInfo info, StreamingContext context)
            : base(info, context, TYPEINFO)
        {
        }


        /// <summary>
        /// Creates an empty instance of the <see cref="RegexRedirectsInfo"/> class.
        /// </summary>
        public RegexRedirectsInfo()
            : base(TYPEINFO)
        {
        }


        /// <summary>
        /// Creates a new instances of the <see cref="RegexRedirectsInfo"/> class from the given <see cref="DataRow"/>.
        /// </summary>
        /// <param name="dr">DataRow with the object data.</param>
        public RegexRedirectsInfo(DataRow dr)
            : base(TYPEINFO, dr)
        {
        }
    }
}