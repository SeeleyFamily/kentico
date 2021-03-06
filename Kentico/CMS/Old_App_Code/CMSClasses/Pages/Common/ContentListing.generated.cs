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
using CMS.DocumentEngine.Types.Common;

[assembly: RegisterDocumentType(ContentListing.CLASS_NAME, typeof(ContentListing))]

namespace CMS.DocumentEngine.Types.Common
{
	/// <summary>
	/// Represents a content item of type ContentListing.
	/// </summary>
	public partial class ContentListing : TreeNode
	{
		#region "Constants and variables"

		/// <summary>
		/// The name of the data class.
		/// </summary>
		public const string CLASS_NAME = "Common.ContentListing";


		/// <summary>
		/// The instance of the class that provides extended API for working with ContentListing fields.
		/// </summary>
		private readonly ContentListingFields mFields;

		#endregion


		#region "Properties"

		/// <summary>
		/// ContentListingID.
		/// </summary>
		[DatabaseIDField]
		public int ContentListingID
		{
			get
			{
				return ValidationHelper.GetInteger(GetValue("ContentListingID"), 0);
			}
			set
			{
				SetValue("ContentListingID", value);
			}
		}


		/// <summary>
		/// Page Alias.
		/// </summary>
		[DatabaseField]
		public string NodeAlias1
		{
			get
			{
				return ValidationHelper.GetString(GetValue("NodeAlias"), @"");
			}
			set
			{
				SetValue("NodeAlias", value);
			}
		}


		/// <summary>
		/// Headline.
		/// </summary>
		[DatabaseField]
		public string Headline
		{
			get
			{
				return ValidationHelper.GetString(GetValue("Headline"), @"");
			}
			set
			{
				SetValue("Headline", value);
			}
		}


		/// <summary>
		/// Description.
		/// </summary>
		[DatabaseField]
		public string Description
		{
			get
			{
				return ValidationHelper.GetString(GetValue("Description"), @"");
			}
			set
			{
				SetValue("Description", value);
			}
		}


		/// <summary>
		/// Hero Background Image.
		/// </summary>
		[DatabaseField]
		public string HeroBackgroundImage
		{
			get
			{
				return ValidationHelper.GetString(GetValue("HeroBackgroundImage"), @"");
			}
			set
			{
				SetValue("HeroBackgroundImage", value);
			}
		}


		/// <summary>
		/// Hero Background Image Mobile.
		/// </summary>
		[DatabaseField]
		public string HeroBackgroundImageMobile
		{
			get
			{
				return ValidationHelper.GetString(GetValue("HeroBackgroundImageMobile"), @"");
			}
			set
			{
				SetValue("HeroBackgroundImageMobile", value);
			}
		}


		/// <summary>
		/// Content.
		/// </summary>
		[DatabaseField]
		public string Content
		{
			get
			{
				return ValidationHelper.GetString(GetValue("Content"), @"");
			}
			set
			{
				SetValue("Content", value);
			}
		}


		/// <summary>
		/// Listing Content.
		/// </summary>
		public string ListingContent
		{
			get
			{
				return ValidationHelper.GetString(GetValue("ListingContent"), @"");
			}
			set
			{
				SetValue("ListingContent", value);
			}
		}


		/// <summary>
		/// Featured Content.
		/// </summary>
		[DatabaseField]
		public string FeaturedContent
		{
			get
			{
				return ValidationHelper.GetString(GetValue("FeaturedContent"), @"");
			}
			set
			{
				SetValue("FeaturedContent", value);
			}
		}


		/// <summary>
		/// Hide Filters.
		/// </summary>
		[DatabaseField]
		public bool HideFilters
		{
			get
			{
				return ValidationHelper.GetBoolean(GetValue("HideFilters"), false);
			}
			set
			{
				SetValue("HideFilters", value);
			}
		}


		/// <summary>
		/// Hide Search Filter.
		/// </summary>
		[DatabaseField]
		public bool HideSearchFilter
		{
			get
			{
				return ValidationHelper.GetBoolean(GetValue("HideSearchFilter"), false);
			}
			set
			{
				SetValue("HideSearchFilter", value);
			}
		}


		/// <summary>
		/// Type Filter Explanation.
		/// </summary>
		public string TypeFilterExplanation
		{
			get
			{
				return ValidationHelper.GetString(GetValue("TypeFilterExplanation"), @"");
			}
			set
			{
				SetValue("TypeFilterExplanation", value);
			}
		}


		/// <summary>
		/// Hide Type Filter.
		/// </summary>
		[DatabaseField]
		public bool HideTypeFilter
		{
			get
			{
				return ValidationHelper.GetBoolean(GetValue("HideTypeFilter"), false);
			}
			set
			{
				SetValue("HideTypeFilter", value);
			}
		}


		/// <summary>
		/// Type Filter.
		/// </summary>
		[DatabaseField]
		public string TypeFilter
		{
			get
			{
				return ValidationHelper.GetString(GetValue("TypeFilter"), @"");
			}
			set
			{
				SetValue("TypeFilter", value);
			}
		}


		/// <summary>
		/// Topic Filter Explanation.
		/// </summary>
		public string TopicFilterExplanation
		{
			get
			{
				return ValidationHelper.GetString(GetValue("TopicFilterExplanation"), @"");
			}
			set
			{
				SetValue("TopicFilterExplanation", value);
			}
		}


		/// <summary>
		/// Hide Topic Filter.
		/// </summary>
		[DatabaseField]
		public bool HideTopicFilter
		{
			get
			{
				return ValidationHelper.GetBoolean(GetValue("HideTopicFilter"), false);
			}
			set
			{
				SetValue("HideTopicFilter", value);
			}
		}


		/// <summary>
		/// Topic Filter.
		/// </summary>
		[DatabaseField]
		public string TopicFilter
		{
			get
			{
				return ValidationHelper.GetString(GetValue("TopicFilter"), @"");
			}
			set
			{
				SetValue("TopicFilter", value);
			}
		}


		/// <summary>
		/// CollectionExplanation.
		/// </summary>
		public string CollectionExplanation
		{
			get
			{
				return ValidationHelper.GetString(GetValue("CollectionExplanation"), @"");
			}
			set
			{
				SetValue("CollectionExplanation", value);
			}
		}


		/// <summary>
		/// Collection(s).
		/// </summary>
		[DatabaseField]
		public string Collections
		{
			get
			{
				return ValidationHelper.GetString(GetValue("Collections"), @"");
			}
			set
			{
				SetValue("Collections", value);
			}
		}


		/// <summary>
		/// Collection(s) Path.
		/// </summary>
		[DatabaseField]
		public string CollectionsPath
		{
			get
			{
				return ValidationHelper.GetString(GetValue("CollectionsPath"), @"");
			}
			set
			{
				SetValue("CollectionsPath", value);
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
		/// Rel Canonical.
		/// </summary>
		[DatabaseField]
		public string RelCanonical
		{
			get
			{
				return ValidationHelper.GetString(GetValue("RelCanonical"), @"");
			}
			set
			{
				SetValue("RelCanonical", value);
			}
		}


		/// <summary>
		/// No Index No Follow.
		/// </summary>
		[DatabaseField]
		public bool NoIndexNoFollow
		{
			get
			{
				return ValidationHelper.GetBoolean(GetValue("NoIndexNoFollow"), false);
			}
			set
			{
				SetValue("NoIndexNoFollow", value);
			}
		}


		/// <summary>
		/// Mobile Web App Title.
		/// </summary>
		[DatabaseField]
		public string MobileWebAppTitle
		{
			get
			{
				return ValidationHelper.GetString(GetValue("MobileWebAppTitle"), @"");
			}
			set
			{
				SetValue("MobileWebAppTitle", value);
			}
		}


		/// <summary>
		/// Schema.
		/// </summary>
		[DatabaseField]
		public string Schema
		{
			get
			{
				return ValidationHelper.GetString(GetValue("Schema"), @"");
			}
			set
			{
				SetValue("Schema", value);
			}
		}


		/// <summary>
		/// Open Graph Title.
		/// </summary>
		[DatabaseField]
		public string OgTitle
		{
			get
			{
				return ValidationHelper.GetString(GetValue("OgTitle"), @"");
			}
			set
			{
				SetValue("OgTitle", value);
			}
		}


		/// <summary>
		/// Open Graph Description.
		/// </summary>
		[DatabaseField]
		public string OgDescription
		{
			get
			{
				return ValidationHelper.GetString(GetValue("OgDescription"), @"");
			}
			set
			{
				SetValue("OgDescription", value);
			}
		}


		/// <summary>
		/// Open Graph Image.
		/// </summary>
		[DatabaseField]
		public string OgImage
		{
			get
			{
				return ValidationHelper.GetString(GetValue("OgImage"), @"");
			}
			set
			{
				SetValue("OgImage", value);
			}
		}


		/// <summary>
		/// Open Graph Url.
		/// </summary>
		[DatabaseField]
		public string OgUrl
		{
			get
			{
				return ValidationHelper.GetString(GetValue("OgUrl"), @"");
			}
			set
			{
				SetValue("OgUrl", value);
			}
		}


		/// <summary>
		/// Twitter Card.
		/// </summary>
		[DatabaseField]
		public string TwitterCard
		{
			get
			{
				return ValidationHelper.GetString(GetValue("TwitterCard"), @"");
			}
			set
			{
				SetValue("TwitterCard", value);
			}
		}


		/// <summary>
		/// Twitter Site.
		/// </summary>
		[DatabaseField]
		public string TwitterSite
		{
			get
			{
				return ValidationHelper.GetString(GetValue("TwitterSite"), @"");
			}
			set
			{
				SetValue("TwitterSite", value);
			}
		}


		/// <summary>
		/// Twitter Creator.
		/// </summary>
		[DatabaseField]
		public string TwitterCreator
		{
			get
			{
				return ValidationHelper.GetString(GetValue("TwitterCreator"), @"");
			}
			set
			{
				SetValue("TwitterCreator", value);
			}
		}


		/// <summary>
		/// Is Content Only.
		/// </summary>
		[DatabaseField]
		public bool IsContentOnly
		{
			get
			{
				return ValidationHelper.GetBoolean(GetValue("IsContentOnly"), false);
			}
			set
			{
				SetValue("IsContentOnly", value);
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
		/// Gets an object that provides extended API for working with ContentListing fields.
		/// </summary>
		[RegisterProperty]
		public ContentListingFields Fields
		{
			get
			{
				return mFields;
			}
		}


		/// <summary>
		/// Provides extended API for working with ContentListing fields.
		/// </summary>
		[RegisterAllProperties]
		public partial class ContentListingFields : AbstractHierarchicalObject<ContentListingFields>
		{
			/// <summary>
			/// The content item of type ContentListing that is a target of the extended API.
			/// </summary>
			private readonly ContentListing mInstance;


			/// <summary>
			/// Initializes a new instance of the <see cref="ContentListingFields" /> class with the specified content item of type ContentListing.
			/// </summary>
			/// <param name="instance">The content item of type ContentListing that is a target of the extended API.</param>
			public ContentListingFields(ContentListing instance)
			{
				mInstance = instance;
			}


			/// <summary>
			/// ContentListingID.
			/// </summary>
			public int ID
			{
				get
				{
					return mInstance.ContentListingID;
				}
				set
				{
					mInstance.ContentListingID = value;
				}
			}


			/// <summary>
			/// Page Alias.
			/// </summary>
			public string NodeAlias
			{
				get
				{
					return mInstance.NodeAlias1;
				}
				set
				{
					mInstance.NodeAlias1 = value;
				}
			}


			/// <summary>
			/// Headline.
			/// </summary>
			public string Headline
			{
				get
				{
					return mInstance.Headline;
				}
				set
				{
					mInstance.Headline = value;
				}
			}


			/// <summary>
			/// Description.
			/// </summary>
			public string Description
			{
				get
				{
					return mInstance.Description;
				}
				set
				{
					mInstance.Description = value;
				}
			}


			/// <summary>
			/// Hero Background Image.
			/// </summary>
			public string HeroBackgroundImage
			{
				get
				{
					return mInstance.HeroBackgroundImage;
				}
				set
				{
					mInstance.HeroBackgroundImage = value;
				}
			}


			/// <summary>
			/// Hero Background Image Mobile.
			/// </summary>
			public string HeroBackgroundImageMobile
			{
				get
				{
					return mInstance.HeroBackgroundImageMobile;
				}
				set
				{
					mInstance.HeroBackgroundImageMobile = value;
				}
			}


			/// <summary>
			/// Content.
			/// </summary>
			public string Content
			{
				get
				{
					return mInstance.Content;
				}
				set
				{
					mInstance.Content = value;
				}
			}


			/// <summary>
			/// Listing Content.
			/// </summary>
			public string ListingContent
			{
				get
				{
					return mInstance.ListingContent;
				}
				set
				{
					mInstance.ListingContent = value;
				}
			}


			/// <summary>
			/// Featured Content.
			/// </summary>
			public string FeaturedContent
			{
				get
				{
					return mInstance.FeaturedContent;
				}
				set
				{
					mInstance.FeaturedContent = value;
				}
			}


			/// <summary>
			/// Hide Filters.
			/// </summary>
			public bool HideFilters
			{
				get
				{
					return mInstance.HideFilters;
				}
				set
				{
					mInstance.HideFilters = value;
				}
			}


			/// <summary>
			/// Hide Search Filter.
			/// </summary>
			public bool HideSearchFilter
			{
				get
				{
					return mInstance.HideSearchFilter;
				}
				set
				{
					mInstance.HideSearchFilter = value;
				}
			}


			/// <summary>
			/// Type Filter Explanation.
			/// </summary>
			public string TypeFilterExplanation
			{
				get
				{
					return mInstance.TypeFilterExplanation;
				}
				set
				{
					mInstance.TypeFilterExplanation = value;
				}
			}


			/// <summary>
			/// Hide Type Filter.
			/// </summary>
			public bool HideTypeFilter
			{
				get
				{
					return mInstance.HideTypeFilter;
				}
				set
				{
					mInstance.HideTypeFilter = value;
				}
			}


			/// <summary>
			/// Type Filter.
			/// </summary>
			public string TypeFilter
			{
				get
				{
					return mInstance.TypeFilter;
				}
				set
				{
					mInstance.TypeFilter = value;
				}
			}


			/// <summary>
			/// Topic Filter Explanation.
			/// </summary>
			public string TopicFilterExplanation
			{
				get
				{
					return mInstance.TopicFilterExplanation;
				}
				set
				{
					mInstance.TopicFilterExplanation = value;
				}
			}


			/// <summary>
			/// Hide Topic Filter.
			/// </summary>
			public bool HideTopicFilter
			{
				get
				{
					return mInstance.HideTopicFilter;
				}
				set
				{
					mInstance.HideTopicFilter = value;
				}
			}


			/// <summary>
			/// Topic Filter.
			/// </summary>
			public string TopicFilter
			{
				get
				{
					return mInstance.TopicFilter;
				}
				set
				{
					mInstance.TopicFilter = value;
				}
			}


			/// <summary>
			/// CollectionExplanation.
			/// </summary>
			public string CollectionExplanation
			{
				get
				{
					return mInstance.CollectionExplanation;
				}
				set
				{
					mInstance.CollectionExplanation = value;
				}
			}


			/// <summary>
			/// Collection(s).
			/// </summary>
			public string Collections
			{
				get
				{
					return mInstance.Collections;
				}
				set
				{
					mInstance.Collections = value;
				}
			}


			/// <summary>
			/// Collection(s) Path.
			/// </summary>
			public string CollectionsPath
			{
				get
				{
					return mInstance.CollectionsPath;
				}
				set
				{
					mInstance.CollectionsPath = value;
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
			/// Rel Canonical.
			/// </summary>
			public string RelCanonical
			{
				get
				{
					return mInstance.RelCanonical;
				}
				set
				{
					mInstance.RelCanonical = value;
				}
			}


			/// <summary>
			/// No Index No Follow.
			/// </summary>
			public bool NoIndexNoFollow
			{
				get
				{
					return mInstance.NoIndexNoFollow;
				}
				set
				{
					mInstance.NoIndexNoFollow = value;
				}
			}


			/// <summary>
			/// Mobile Web App Title.
			/// </summary>
			public string MobileWebAppTitle
			{
				get
				{
					return mInstance.MobileWebAppTitle;
				}
				set
				{
					mInstance.MobileWebAppTitle = value;
				}
			}


			/// <summary>
			/// Schema.
			/// </summary>
			public string Schema
			{
				get
				{
					return mInstance.Schema;
				}
				set
				{
					mInstance.Schema = value;
				}
			}


			/// <summary>
			/// Open Graph Title.
			/// </summary>
			public string OgTitle
			{
				get
				{
					return mInstance.OgTitle;
				}
				set
				{
					mInstance.OgTitle = value;
				}
			}


			/// <summary>
			/// Open Graph Description.
			/// </summary>
			public string OgDescription
			{
				get
				{
					return mInstance.OgDescription;
				}
				set
				{
					mInstance.OgDescription = value;
				}
			}


			/// <summary>
			/// Open Graph Image.
			/// </summary>
			public string OgImage
			{
				get
				{
					return mInstance.OgImage;
				}
				set
				{
					mInstance.OgImage = value;
				}
			}


			/// <summary>
			/// Open Graph Url.
			/// </summary>
			public string OgUrl
			{
				get
				{
					return mInstance.OgUrl;
				}
				set
				{
					mInstance.OgUrl = value;
				}
			}


			/// <summary>
			/// Twitter Card.
			/// </summary>
			public string TwitterCard
			{
				get
				{
					return mInstance.TwitterCard;
				}
				set
				{
					mInstance.TwitterCard = value;
				}
			}


			/// <summary>
			/// Twitter Site.
			/// </summary>
			public string TwitterSite
			{
				get
				{
					return mInstance.TwitterSite;
				}
				set
				{
					mInstance.TwitterSite = value;
				}
			}


			/// <summary>
			/// Twitter Creator.
			/// </summary>
			public string TwitterCreator
			{
				get
				{
					return mInstance.TwitterCreator;
				}
				set
				{
					mInstance.TwitterCreator = value;
				}
			}


			/// <summary>
			/// Is Content Only.
			/// </summary>
			public bool IsContentOnly
			{
				get
				{
					return mInstance.IsContentOnly;
				}
				set
				{
					mInstance.IsContentOnly = value;
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
			public string ContentMigrationLabel
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
			public string ContentMigrationComment
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
			public string ContentMigrationExternalId
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
			public string ContentMigrationType
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
			public string ContentMigrationRelationshipId
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
			public string ContentMigrationOldUrl
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
			public string ContentMigrationNewUrl
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
			public string ContentMigrationNewNodeAliasPath
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
		/// Initializes a new instance of the <see cref="ContentListing" /> class.
		/// </summary>
		public ContentListing() : base(CLASS_NAME)
		{
			mFields = new ContentListingFields(this);
		}

		#endregion
	}
}