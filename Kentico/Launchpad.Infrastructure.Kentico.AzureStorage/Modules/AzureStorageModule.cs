using CMS;
using CMS.IO;
using Launchpad.Infrastructure.Kentico.AzureStorage.Modules;
using Launchpad.Infrastructure.Modules;
using System.Configuration;

// Registers the custom module into the system
[assembly: RegisterModule(typeof(AzureStorageModule))]
namespace Launchpad.Infrastructure.Kentico.AzureStorage.Modules
{
	public class AzureStorageModule : CustomCmsModule
	{

		// Module class constructor, the system registers the module under the name "CustomInit"
		public AzureStorageModule()
			: base(nameof(AzureStorageModule))
		{
			this.SettingDisableModuleCodeName = "DisableAzureStorageModule";
		}

		protected override void RegisterModuleEvents()
		{
			bool.TryParse(ConfigurationManager.AppSettings["CMSAzureStorageEnabled"], out var isAzureStorageEnabled);
			if (isAzureStorageEnabled)
			{
				// Creates a new StorageProvider instance for Azure
				var mediaProvider = StorageProvider.CreateAzureStorageProvider();

				// Specifies the target container
				mediaProvider.CustomRootPath = "kenticomediacontainer";

				// Makes the container publicly accessible
				mediaProvider.PublicExternalFolderObject = true;
				
				// Maps a directory to the provider
				StorageHelper.MapStoragePath("~/SharedMedia", mediaProvider);
			}
		}
	}
}
