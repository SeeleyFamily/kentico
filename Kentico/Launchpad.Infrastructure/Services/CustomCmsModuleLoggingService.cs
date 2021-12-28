using CMS.Core;
using CMS.DataEngine;

namespace Launchpad.Infrastructure.Services
{
	public class CustomCmsModuleLoggingService
	{
		#region Properties
		public bool AllowLogging { get; set; }
		#endregion

		#region Fields
		private readonly string SettingEnableModuleLogging = "EnableModuleLogging";
		#endregion

		public CustomCmsModuleLoggingService()
		{
		}

		public void LogInformation(string source, string eventCode, string eventDescription = "", bool forceLog = false)
		{
			bool enableModuleLogging = SettingsKeyInfoProvider.GetBoolValue(SettingEnableModuleLogging);
			if (enableModuleLogging)
			{
				AllowLogging = enableModuleLogging;
			}

			if (AllowLogging || forceLog)
			{
				Service.Resolve<IEventLogService>().LogInformation(source, eventCode, eventDescription);
			}
		}
	}
}
