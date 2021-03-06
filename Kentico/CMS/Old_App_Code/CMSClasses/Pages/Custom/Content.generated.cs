//--------------------------------------------------------------------------------------------------
// <auto-generated>
//
//     This code was generated by code generator tool.
//
//     To customize the code use your own partial class. For more info about how to use and customize
//     the generated code see the documentation at https://docs.xperience.io/.
//
// </auto-generated>
//--------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using CMS;
using CMS.Base;
using CMS.Helpers;
using CMS.DataEngine;
using CMS.DocumentEngine;
using CMS.DocumentEngine.Types.Custom;

[assembly: RegisterDocumentType(Content.CLASS_NAME, typeof(Content))]

namespace CMS.DocumentEngine.Types.Custom
{
	/// <summary>
	/// Represents a content item of type Content.
	/// </summary>
	public partial class Content : TreeNode
	{
		#region "Constants and variables"

		/// <summary>
		/// The name of the data class.
		/// </summary>
		public const string CLASS_NAME = "Custom.Content";


		/// <summary>
		/// The instance of the class that provides extended API for working with Content fields.
		/// </summary>
		private readonly ContentFields mFields;

		#endregion


		#region "Properties"

		/// <summary>
		/// ContentID.
		/// </summary>
		[DatabaseIDField]
		public int ContentID
		{
			get
			{
				return ValidationHelper.GetInteger(GetValue("ContentID"), 0);
			}
			set
			{
				SetValue("ContentID", value);
			}
		}


		/// <summary>
		/// Categories.
		/// </summary>
		[DatabaseField]
		public string Categories1
		{
			get
			{
				return ValidationHelper.GetString(GetValue("Categories"), @"");
			}
			set
			{
				SetValue("Categories", value);
			}
		}


		/// <summary>
		/// Preview Title (Override).
		/// </summary>
		[DatabaseField]
		public string PreviewTitle
		{
			get
			{
				return ValidationHelper.GetString(GetValue("PreviewTitle"), @"");
			}
			set
			{
				SetValue("PreviewTitle", value);
			}
		}


		/// <summary>
		/// Preview Text (Override).
		/// </summary>
		[DatabaseField]
		public string PreviewText
		{
			get
			{
				return ValidationHelper.GetString(GetValue("PreviewText"), @"");
			}
			set
			{
				SetValue("PreviewText", value);
			}
		}


		/// <summary>
		/// Hide Preview Text (Override).
		/// </summary>
		[DatabaseField]
		public bool HidePreviewText
		{
			get
			{
				return ValidationHelper.GetBoolean(GetValue("HidePreviewText"), false);
			}
			set
			{
				SetValue("HidePreviewText", value);
			}
		}


		/// <summary>
		/// Preview Image (Override).
		/// </summary>
		[DatabaseField]
		public string PreviewImage
		{
			get
			{
				return ValidationHelper.GetString(GetValue("PreviewImage"), @"");
			}
			set
			{
				SetValue("PreviewImage", value);
			}
		}


		/// <summary>
		/// Preview Cta Label (Override).
		/// </summary>
		[DatabaseField]
		public string PreviewCtaLabel
		{
			get
			{
				return ValidationHelper.GetString(GetValue("PreviewCtaLabel"), @"");
			}
			set
			{
				SetValue("PreviewCtaLabel", value);
			}
		}


		/// <summary>
		/// CmsFormScripts.
		/// </summary>
		[DatabaseField]
		public string CmsFormScripts
		{
			get
			{
				return ValidationHelper.GetString(GetValue("CmsFormScripts"), @"");
			}
			set
			{
				SetValue("CmsFormScripts", value);
			}
		}


		/// <summary>
		/// Content Migration Label.
		/// </summary>
		[DatabaseField]
		public string ContentMigrationLabel
		{
			get
			{
				return ValidationHelper.GetString(GetValue("ContentMigrationLabel"), @"");
			}
			set
			{
				SetValue("ContentMigrationLabel", value);
			}
		}


		/// <summary>
		/// Content Migration Comment.
		/// </summary>
		[DatabaseField]
		public string ContentMigrationComment
		{
			get
			{
				return ValidationHelper.GetString(GetValue("ContentMigrationComment"), @"");
			}
			set
			{
				SetValue("ContentMigrationComment", value);
			}
		}


		/// <summary>
		/// Content Migration External Id.
		/// </summary>
		[DatabaseField]
		public string ContentMigrationExternalId
		{
			get
			{
				return ValidationHelper.GetString(GetValue("ContentMigrationExternalId"), @"");
			}
			set
			{
				SetValue("ContentMigrationExternalId", value);
			}
		}


		/// <summary>
		/// Content Migration Type.
		/// </summary>
		[DatabaseField]
		public string ContentMigrationType
		{
			get
			{
				return ValidationHelper.GetString(GetValue("ContentMigrationType"), @"");
			}
			set
			{
				SetValue("ContentMigrationType", value);
			}
		}


		/// <summary>
		/// Content Migration Relationship Id.
		/// </summary>
		[DatabaseField]
		public string ContentMigrationRelationshipId
		{
			get
			{
				return ValidationHelper.GetString(GetValue("ContentMigrationRelationshipId"), @"");
			}
			set
			{
				SetValue("ContentMigrationRelationshipId", value);
			}
		}


		/// <summary>
		/// Content Migration Old Url.
		/// </summary>
		[DatabaseField]
		public string ContentMigrationOldUrl
		{
			get
			{
				return ValidationHelper.GetString(GetValue("ContentMigrationOldUrl"), @"");
			}
			set
			{
				SetValue("ContentMigrationOldUrl", value);
			}
		}


		/// <summary>
		/// Content Migration New Url.
		/// </summary>
		[DatabaseField]
		public string ContentMigrationNewUrl
		{
			get
			{
				return ValidationHelper.GetString(GetValue("ContentMigrationNewUrl"), @"");
			}
			set
			{
				SetValue("ContentMigrationNewUrl", value);
			}
		}


		/// <summary>
		/// Content Migration New Node Alias Path.
		/// </summary>
		[DatabaseField]
		public string ContentMigrationNewNodeAliasPath
		{
			get
			{
				return ValidationHelper.GetString(GetValue("ContentMigrationNewNodeAliasPath"), @"");
			}
			set
			{
				SetValue("ContentMigrationNewNodeAliasPath", value);
			}
		}


		/// <summary>
		/// Gets an object that provides extended API for working with Content fields.
		/// </summary>
		[RegisterProperty]
		public ContentFields Fields
		{
			get
			{
				return mFields;
			}
		}


		/// <summary>
		/// Provides extended API for working with Content fields.
		/// </summary>
		[RegisterAllProperties]
		public partial class ContentFields : AbstractHierarchicalObject<ContentFields>
		{
			/// <summary>
			/// The content item of type Content that is a target of the extended API.
			/// </summary>
			private readonly Content mInstance;


			/// <summary>
			/// Initializes a new instance of the <see cref="ContentFields" /> class with the specified content item of type Content.
			/// </summary>
			/// <param name="instance">The content item of type Content that is a target of the extended API.</param>
			public ContentFields(Content instance)
			{
				mInstance = instance;
			}


			/// <summary>
			/// ContentID.
			/// </summary>
			public int ID
			{
				get
				{
					return mInstance.ContentID;
				}
				set
				{
					mInstance.ContentID = value;
				}
			}


			/// <summary>
			/// Categories.
			/// </summary>
			public string Categories
			{
				get
				{
					return mInstance.Categories1;
				}
				set
				{
					mInstance.Categories1 = value;
				}
			}


			/// <summary>
			/// Preview Title (Override).
			/// </summary>
			public string PreviewTitle
			{
				get
				{
					return mInstance.PreviewTitle;
				}
				set
				{
					mInstance.PreviewTitle = value;
				}
			}


			/// <summary>
			/// Preview Text (Override).
			/// </summary>
			public string PreviewText
			{
				get
				{
					return mInstance.PreviewText;
				}
				set
				{
					mInstance.PreviewText = value;
				}
			}


			/// <summary>
			/// Hide Preview Text (Override).
			/// </summary>
			public bool HidePreviewText
			{
				get
				{
					return mInstance.HidePreviewText;
				}
				set
				{
					mInstance.HidePreviewText = value;
				}
			}


			/// <summary>
			/// Preview Image (Override).
			/// </summary>
			public string PreviewImage
			{
				get
				{
					return mInstance.PreviewImage;
				}
				set
				{
					mInstance.PreviewImage = value;
				}
			}


			/// <summary>
			/// Preview Cta Label (Override).
			/// </summary>
			public string PreviewCtaLabel
			{
				get
				{
					return mInstance.PreviewCtaLabel;
				}
				set
				{
					mInstance.PreviewCtaLabel = value;
				}
			}


			/// <summary>
			/// CmsFormScripts.
			/// </summary>
			public string CmsFormScripts
			{
				get
				{
					return mInstance.CmsFormScripts;
				}
				set
				{
					mInstance.CmsFormScripts = value;
				}
			}


			/// <summary>
			/// Content Migration Label.
			/// </summary>
			public string MigrationLabel
			{
				get
				{
					return mInstance.ContentMigrationLabel;
				}
				set
				{
					mInstance.ContentMigrationLabel = value;
				}
			}


			/// <summary>
			/// Content Migration Comment.
			/// </summary>
			public string MigrationComment
			{
				get
				{
					return mInstance.ContentMigrationComment;
				}
				set
				{
					mInstance.ContentMigrationComment = value;
				}
			}


			/// <summary>
			/// Content Migration External Id.
			/// </summary>
			public string MigrationExternalId
			{
				get
				{
					return mInstance.ContentMigrationExternalId;
				}
				set
				{
					mInstance.ContentMigrationExternalId = value;
				}
			}


			/// <summary>
			/// Content Migration Type.
			/// </summary>
			public string MigrationType
			{
				get
				{
					return mInstance.ContentMigrationType;
				}
				set
				{
					mInstance.ContentMigrationType = value;
				}
			}


			/// <summary>
			/// Content Migration Relationship Id.
			/// </summary>
			public string MigrationRelationshipId
			{
				get
				{
					return mInstance.ContentMigrationRelationshipId;
				}
				set
				{
					mInstance.ContentMigrationRelationshipId = value;
				}
			}


			/// <summary>
			/// Content Migration Old Url.
			/// </summary>
			public string MigrationOldUrl
			{
				get
				{
					return mInstance.ContentMigrationOldUrl;
				}
				set
				{
					mInstance.ContentMigrationOldUrl = value;
				}
			}


			/// <summary>
			/// Content Migration New Url.
			/// </summary>
			public string MigrationNewUrl
			{
				get
				{
					return mInstance.ContentMigrationNewUrl;
				}
				set
				{
					mInstance.ContentMigrationNewUrl = value;
				}
			}


			/// <summary>
			/// Content Migration New Node Alias Path.
			/// </summary>
			public string MigrationNewNodeAliasPath
			{
				get
				{
					return mInstance.ContentMigrationNewNodeAliasPath;
				}
				set
				{
					mInstance.ContentMigrationNewNodeAliasPath = value;
				}
			}
		}

		#endregion


		#region "Constructors"

		/// <summary>
		/// Initializes a new instance of the <see cref="Content" /> class.
		/// </summary>
		public Content() : base(CLASS_NAME)
		{
			mFields = new ContentFields(this);
		}

		#endregion
	}
}