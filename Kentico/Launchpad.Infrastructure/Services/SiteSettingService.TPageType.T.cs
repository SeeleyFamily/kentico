using CMS.DocumentEngine;
using CMS.DocumentEngine.Types.Common;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Models;

namespace Launchpad.Infrastructure.Services
{
	/// <summary>
	/// This class shoud populate the convert method to match the properties found in the generated BaseSiteSettings Page Type
	/// </summary>
	/// <typeparam name="TPageType"></typeparam>
	/// <typeparam name="T"></typeparam>
	public abstract class SiteSettingService<TPageType, T> : GlobalContentDocumentService<TPageType, T>
		where TPageType : TreeNode, new()
		where T : BaseSiteSettingsModel, new()
	{

		public SiteSettingService(IGlobalContentDocumentService<TPageType> documentService) : base(documentService)
		{
		}

		protected override T Convert(TPageType node)
		{
			var model = new T()
			{
				// Common Populate
				CopyrightText = node.GetStringValue(nameof(BaseSiteSettings.CopyrightText), string.Empty),
			};

			return model;
		}
	}
}
