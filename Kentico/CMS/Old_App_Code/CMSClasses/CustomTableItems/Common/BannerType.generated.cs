//--------------------------------------------------------------------------------------------------
// <auto-generated>
//
//     This code was generated by code generator tool.
//
//     To customize the code use your own partial class. For more info about how to use and customize
//     the generated code see the documentation at http://docs.kentico.com.
//
// </auto-generated>
//--------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

using CMS;
using CMS.Base;
using CMS.Helpers;
using CMS.DataEngine;
using CMS.CustomTables.Types.Common;
using CMS.CustomTables;

[assembly: RegisterCustomTable(BannerTypeItem.CLASS_NAME, typeof(BannerTypeItem))]

namespace CMS.CustomTables.Types.Common
{
	/// <summary>
	/// Represents a content item of type BannerTypeItem.
	/// </summary>
	public partial class BannerTypeItem : CustomTableItem
	{
		#region "Constants and variables"

		/// <summary>
		/// The name of the data class.
		/// </summary>
		public const string CLASS_NAME = "Common.BannerType";


		/// <summary>
		/// The instance of the class that provides extended API for working with BannerTypeItem fields.
		/// </summary>
		private readonly BannerTypeItemFields mFields;

		#endregion


		#region "Properties"

		/// <summary>
		/// Code Name.
		/// </summary>
		[DatabaseField]
		public string CodeName
		{
			get
			{
				return ValidationHelper.GetString(GetValue("CodeName"), @"");
			}
			set
			{
				SetValue("CodeName", value);
			}
		}


		/// <summary>
		/// Display Name.
		/// </summary>
		[DatabaseField]
		public string DisplayName
		{
			get
			{
				return ValidationHelper.GetString(GetValue("DisplayName"), @"");
			}
			set
			{
				SetValue("DisplayName", value);
			}
		}


		/// <summary>
		/// CSS Class.
		/// </summary>
		[DatabaseField]
		public string CssClass
		{
			get
			{
				return ValidationHelper.GetString(GetValue("CssClass"), @"");
			}
			set
			{
				SetValue("CssClass", value);
			}
		}


		/// <summary>
		/// Gets an object that provides extended API for working with BannerTypeItem fields.
		/// </summary>
		[RegisterProperty]
		public BannerTypeItemFields Fields
		{
			get
			{
				return mFields;
			}
		}


		/// <summary>
		/// Provides extended API for working with BannerTypeItem fields.
		/// </summary>
		[RegisterAllProperties]
		public partial class BannerTypeItemFields : AbstractHierarchicalObject<BannerTypeItemFields>
		{
			/// <summary>
			/// The content item of type BannerTypeItem that is a target of the extended API.
			/// </summary>
			private readonly BannerTypeItem mInstance;


			/// <summary>
			/// Initializes a new instance of the <see cref="BannerTypeItemFields" /> class with the specified content item of type BannerTypeItem.
			/// </summary>
			/// <param name="instance">The content item of type BannerTypeItem that is a target of the extended API.</param>
			public BannerTypeItemFields(BannerTypeItem instance)
			{
				mInstance = instance;
			}


			/// <summary>
			/// Code Name.
			/// </summary>
			public string CodeName
			{
				get
				{
					return mInstance.CodeName;
				}
				set
				{
					mInstance.CodeName = value;
				}
			}


			/// <summary>
			/// Display Name.
			/// </summary>
			public string DisplayName
			{
				get
				{
					return mInstance.DisplayName;
				}
				set
				{
					mInstance.DisplayName = value;
				}
			}


			/// <summary>
			/// CSS Class.
			/// </summary>
			public string CssClass
			{
				get
				{
					return mInstance.CssClass;
				}
				set
				{
					mInstance.CssClass = value;
				}
			}
		}

		#endregion


		#region "Constructors"

		/// <summary>
		/// Initializes a new instance of the <see cref="BannerTypeItem" /> class.
		/// </summary>
		public BannerTypeItem() : base(CLASS_NAME)
		{
			mFields = new BannerTypeItemFields(this);
		}

		#endregion
	}
}