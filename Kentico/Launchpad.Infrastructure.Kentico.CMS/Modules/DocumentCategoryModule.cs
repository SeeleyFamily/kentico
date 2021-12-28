using CMS;
using CMS.DocumentEngine;
using Launchpad.Infrastructure.Kentico.CMS.Modules;
using Launchpad.Infrastructure.Kentico.CMS.Services;
using Launchpad.Infrastructure.Modules;

[assembly: RegisterModule(typeof(DocumentCategoryModule))]
namespace Launchpad.Infrastructure.Kentico.CMS.Modules
{
	class DocumentCategoryModule : CustomCmsModule
	{
		#region Fields
		private readonly DocumentCategoryModuleService documentCategoryModuleService;
		#endregion

		public DocumentCategoryModule()
			: base(nameof(DocumentCategoryModule))
		{
			documentCategoryModuleService = new DocumentCategoryModuleService();
			this.SettingDisableModuleCodeName = "DisableDocumentCategoryModule";
		}

		protected override void RegisterModuleEvents()
		{
			DocumentCategoryInfo.TYPEINFO.Events.Insert.After += documentCategoryModuleService.TriggerPageUpdate;
			DocumentCategoryInfo.TYPEINFO.Events.Delete.After += documentCategoryModuleService.TriggerPageUpdate;
		}
	}
}
