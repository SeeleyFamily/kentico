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

[assembly: RegisterDocumentType(CmsManual.CLASS_NAME, typeof(CmsManual))]

namespace CMS.DocumentEngine.Types.Common
{
	/// <summary>
	/// Represents a content item of type CmsManual.
	/// </summary>
	public partial class CmsManual : TreeNode
	{
		#region "Constants and variables"

		/// <summary>
		/// The name of the data class.
		/// </summary>
		public const string CLASS_NAME = "Common.CmsManual";


		/// <summary>
		/// The instance of the class that provides extended API for working with CmsManual fields.
		/// </summary>
		private readonly CmsManualFields mFields;

		#endregion


		#region "Properties"

		/// <summary>
		/// CmsManualID.
		/// </summary>
		[DatabaseIDField]
		public int CmsManualID
		{
			get
			{
				return ValidationHelper.GetInteger(GetValue("CmsManualID"), 0);
			}
			set
			{
				SetValue("CmsManualID", value);
			}
		}


		/// <summary>
		/// Placeholder.
		/// </summary>
		[DatabaseField]
		public string Placeholder
		{
			get
			{
				return ValidationHelper.GetString(GetValue("Placeholder"), @"");
			}
			set
			{
				SetValue("Placeholder", value);
			}
		}


		/// <summary>
		/// Gets an object that provides extended API for working with CmsManual fields.
		/// </summary>
		[RegisterProperty]
		public CmsManualFields Fields
		{
			get
			{
				return mFields;
			}
		}


		/// <summary>
		/// Provides extended API for working with CmsManual fields.
		/// </summary>
		[RegisterAllProperties]
		public partial class CmsManualFields : AbstractHierarchicalObject<CmsManualFields>
		{
			/// <summary>
			/// The content item of type CmsManual that is a target of the extended API.
			/// </summary>
			private readonly CmsManual mInstance;


			/// <summary>
			/// Initializes a new instance of the <see cref="CmsManualFields" /> class with the specified content item of type CmsManual.
			/// </summary>
			/// <param name="instance">The content item of type CmsManual that is a target of the extended API.</param>
			public CmsManualFields(CmsManual instance)
			{
				mInstance = instance;
			}


			/// <summary>
			/// CmsManualID.
			/// </summary>
			public int ID
			{
				get
				{
					return mInstance.CmsManualID;
				}
				set
				{
					mInstance.CmsManualID = value;
				}
			}


			/// <summary>
			/// Placeholder.
			/// </summary>
			public string Placeholder
			{
				get
				{
					return mInstance.Placeholder;
				}
				set
				{
					mInstance.Placeholder = value;
				}
			}
		}

		#endregion


		#region "Constructors"

		/// <summary>
		/// Initializes a new instance of the <see cref="CmsManual" /> class.
		/// </summary>
		public CmsManual() : base(CLASS_NAME)
		{
			mFields = new CmsManualFields(this);
		}

		#endregion
	}
}