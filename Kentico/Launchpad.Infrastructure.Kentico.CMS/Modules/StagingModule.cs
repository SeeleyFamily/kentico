using CMS;
using CMS.Synchronization;
using Launchpad.Infrastructure.Kentico.CMS.Modules;
using Launchpad.Infrastructure.Kentico.CMS.Services;
using Launchpad.Infrastructure.Modules;

[assembly: RegisterModule(typeof(StagingModule))]
namespace Launchpad.Infrastructure.Kentico.CMS.Modules
{
	public class StagingModule : CustomCmsModule
	{
		#region Fields
		private readonly StagingModuleService stagingModuleService;
		#endregion

		public StagingModule()
			: base(nameof(StagingModule))
		{
			stagingModuleService = new StagingModuleService();
			this.SettingDisableModuleCodeName = "DisableStagingModule";

		}

		protected override void RegisterModuleEvents()
		{
			StagingEvents.LogTask.Before += stagingModuleService.LogStagingTask_Before;

			// Assigns a handler to the StagingEvents.LogTask.After event
			// This event occurs after the system creates content staging synchronization tasks (separately for each task)
			StagingEvents.LogTask.After += stagingModuleService.LogStagingTask_After;

			// Assigns a handler to the IntegrationEvents.LogInternalTask.After event
			// This event occurs after the system creates outgoing integration tasks (separately for each task)
			//IntegrationEvents.LogInternalTask.After += stagingModuleService.LogIntegrationTask_After;
		}
	}

}
