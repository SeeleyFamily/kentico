using CMS;
using CMS.DocumentEngine;
using Launchpad.Infrastructure.Kentico.CMS.Modules;
using Launchpad.Infrastructure.Kentico.CMS.Services;
using Launchpad.Infrastructure.Modules;

[assembly: RegisterModule(typeof(TabWidgetModule))]
namespace Launchpad.Infrastructure.Kentico.CMS.Modules
{
    public class TabWidgetModule : CustomCmsModule
    {
        private readonly TabWidgetModuleService tabWidgetModuleService;

        public TabWidgetModule()
            : base(nameof(TabWidgetModule))
        {
            tabWidgetModuleService = new TabWidgetModuleService();
            this.SettingDisableModuleCodeName = $"Disable{nameof(TabWidgetModule)}";
        }

        protected override void RegisterModuleEvents()
        {
            DocumentEvents.Update.Before += tabWidgetModuleService.UpdateBefore;
            DocumentEvents.Insert.Before += tabWidgetModuleService.InsertBefore;
        }
    }
}
