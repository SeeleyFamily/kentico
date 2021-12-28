using CMS;
using CMS.DocumentEngine;
using Launchpad.Infrastructure.Kentico.CMS.Modules;
using Launchpad.Infrastructure.Kentico.CMS.Services;
using Launchpad.Infrastructure.Modules;

[assembly: RegisterModule(typeof(PublishScheduleModule))]
namespace Launchpad.Infrastructure.Kentico.CMS.Modules
{
	class PublishScheduleModule : CustomCmsModule
	{
		#region Fields
		private readonly PublishScheduleModuleService publishScheduleModuleService;
		#endregion

		public PublishScheduleModule()
			: base(nameof(PublishScheduleModule))
		{
			publishScheduleModuleService = new PublishScheduleModuleService();
			this.SettingDisableModuleCodeName = "DisablePublishScheduleModule";
		}

		protected override void RegisterModuleEvents()
		{
			WorkflowEvents.Publish.After += publishScheduleModuleService.PublishAfterEventHandler;
		}
	}
}
