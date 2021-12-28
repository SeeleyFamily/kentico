using CMS;
using CMS.DataEngine;
using Launchpad.Infrastructure.Kentico.CMS.Modules;
using Launchpad.Infrastructure.Kentico.CMS.Services;
using Launchpad.Infrastructure.Modules;

[assembly: RegisterModule(typeof(RedirectsModule))]
namespace Launchpad.Infrastructure.Kentico.CMS.Modules
{
	class RedirectsModule : CustomCmsModule
	{
		#region Fields
		private readonly RedirectsModuleService redirectsModuleService;
		#endregion

		public RedirectsModule()
			: base(nameof(RedirectsModule))
		{
			redirectsModuleService = new RedirectsModuleService();
			this.SettingDisableModuleCodeName = "DisableRedirectsModule";
		}

		protected override void RegisterModuleEvents()
		{
			ObjectEvents.Insert.Before += redirectsModuleService.RedirectsModuleBeforeUpdateHandler;
			ObjectEvents.Update.Before += redirectsModuleService.RedirectsModuleBeforeUpdateHandler;

			redirectsModuleService.GetRedirects();
		}
	}
}
