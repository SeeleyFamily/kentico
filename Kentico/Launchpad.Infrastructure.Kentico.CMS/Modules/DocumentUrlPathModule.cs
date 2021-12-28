using CMS;
using CMS.DocumentEngine;
using Launchpad.Infrastructure.Kentico.CMS.Modules;
using Launchpad.Infrastructure.Kentico.CMS.Services;
using Launchpad.Infrastructure.Modules;

[assembly: RegisterModule(typeof(DocumentUrlPathModule))]
namespace Launchpad.Infrastructure.Kentico.CMS.Modules
{
	class DocumentUrlPathModule : CustomCmsModule
	{
		#region Fields
		private readonly DocumentUrlPathModuleService documentUrlPathModuleService;
		#endregion

		public DocumentUrlPathModule()
			: base(nameof(DocumentUrlPathModule))
		{
			documentUrlPathModuleService = new DocumentUrlPathModuleService();
			this.SettingDisableModuleCodeName = "DisableDocumentUrlPathModule";
		}

		protected override void RegisterModuleEvents()
		{
			DocumentEvents.Update.Before += documentUrlPathModuleService.UpdateBefore;
			DocumentEvents.Update.After += documentUrlPathModuleService.UpdateAfter;

			DocumentEvents.Insert.Before += documentUrlPathModuleService.InsertBefore;
			DocumentEvents.Insert.After += documentUrlPathModuleService.InsertAfter;

			DocumentEvents.InsertNewCulture.Before += documentUrlPathModuleService.InsertBefore;
			DocumentEvents.InsertNewCulture.After += documentUrlPathModuleService.InsertAfter;

			WorkflowEvents.Publish.After += documentUrlPathModuleService.PublishBefore;
			WorkflowEvents.Publish.Before += documentUrlPathModuleService.PublishAfter;
		}
	}
}
