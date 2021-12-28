using CMS.DocumentEngine.Types.Custom;
using Custom.Core.Models;
using Custom.Infrastructure.Abstractions.Services;
using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Infrastructure.Services;

namespace Custom.Infrastructure.Services
{
	public class SiteSettingsService : SiteSettingService<SiteSettings, SiteSettingsModel>, ISiteSettingsService, IPerScopeService
	{
		public SiteSettingsService(IGlobalContentDocumentService<SiteSettings> documentService) : base(documentService)
		{
		}


		protected override SiteSettingsModel Convert(SiteSettings node)
		{
			var model = base.Convert(node);
			//< !--      Add Custom Site Settings Below     -->
			//< !-- ======================================= -->
			//< !-- ======================================= -->

			return model;
		}

		public string GetCopyrightText()
		{
			throw new System.NotImplementedException();
		}
	}
}
