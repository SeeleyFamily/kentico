using CMS.Core;
using CMS.DataEngine;

namespace Launchpad.Infrastructure.Modules
{
	public class CustomCmsModule : Module
	{
		#region Properties
		public string SettingDisableModuleCodeName { get; set; }
		private string ModuleName { get; set; }
		#endregion

		#region Fields
		private readonly SettingsService settingsService;
		private readonly string SettingDisableAllCustomModules = "DisableAllCustomModules";
		#endregion

		public CustomCmsModule(string moduleName)
			: base(moduleName)
		{
			this.ModuleName = moduleName;
			settingsService = new SettingsService();
		}

		protected override void OnInit()
		{
			base.OnInit();

			var eventLogService = Service.Resolve<IEventLogService>();

			if (CheckModuleRegistration())
			{
				eventLogService.LogInformation($"CMS - {ModuleName} ", "OnInit Start - Register Module", $"{ModuleName} is initializing.");
				RegisterModuleEvents();
				eventLogService.LogInformation($"CMS - {ModuleName} ", "OnInit End - Register Module Complete", $"{ModuleName} has been initialized.");
			}
			else
			{
				eventLogService.LogInformation($"CMS - {ModuleName} ", "OnInit Start - Module Disabled", $"{ModuleName} is disabled.");
			}
		}

		protected virtual bool CheckModuleRegistration()
		{
			var allowRegistration = true;

			bool disableAllCustomModules = SettingsKeyInfoProvider.GetBoolValue(SettingDisableAllCustomModules);
			bool disableCurrentCustomModule = SettingsKeyInfoProvider.GetBoolValue(SettingDisableModuleCodeName);
			
			allowRegistration = allowRegistration && !disableAllCustomModules;
			allowRegistration = allowRegistration && !disableCurrentCustomModule;

			return allowRegistration;
		}

		protected virtual void RegisterModuleEvents()
		{

		}
	}
}
