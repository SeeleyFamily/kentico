using CMS;
using CMS.Helpers;
using CMS.MediaLibrary;
using Launchpad.Infrastructure.ImageMagick.Services;
using Launchpad.Infrastructure.Kentico.ImageOptimization;
using Launchpad.Infrastructure.Kentico.ImageOptimization.WebFarmTasks;
using Launchpad.Infrastructure.Modules;

[assembly: RegisterModule(typeof(MediaFileCompressionModule))]
namespace Launchpad.Infrastructure.Kentico.ImageOptimization
{
	public class MediaFileCompressionModule : CustomCmsModule
	{
		#region Fields
		private readonly MediaFileCompressionModuleService mediaFileCompressionModuleService;
		#endregion

		public MediaFileCompressionModule()
			: base(nameof(MediaFileCompressionModule))
		{
			mediaFileCompressionModuleService = new MediaFileCompressionModuleService();
			this.SettingDisableModuleCodeName = "DisableMediaFileCompressionModule";
		}

		protected override void RegisterModuleEvents()
		{
			MediaFileInfo.TYPEINFO.Events.Insert.After += mediaFileCompressionModuleService.OnInsertAfter;
			MediaFileInfo.TYPEINFO.Events.Update.After += mediaFileCompressionModuleService.OnUpdateAfter;
			MediaFileInfo.TYPEINFO.Events.Delete.Before += mediaFileCompressionModuleService.OnDeleteBefore;

			WebFarmHelper.RegisterTask<ClearAzureCacheWebFarmTask>();
		}
	}
}
