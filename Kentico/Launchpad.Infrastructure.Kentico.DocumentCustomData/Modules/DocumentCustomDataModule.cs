using CMS;
using CMS.DocumentEngine;
using Launchpad.Infrastructure.Kentico.DocumentCustomData.Modules;
using Launchpad.Infrastructure.Kentico.DocumentCustomData.Services;
using Launchpad.Infrastructure.Modules;

[assembly: RegisterModule(typeof(DocumentCustomDataModule))]
namespace Launchpad.Infrastructure.Kentico.DocumentCustomData.Modules
{
	class DocumentCustomDataModule : CustomCmsModule
	{
		#region Fields
		private readonly DocumentCustomDataModuleService documentCustomDataModuleService;
		#endregion

		public DocumentCustomDataModule()
			: base(nameof(DocumentCustomDataModule))
		{
			documentCustomDataModuleService = new DocumentCustomDataModuleService();
			this.SettingDisableModuleCodeName = "DisableDocumentCustomDataModule";
		}

		protected override void RegisterModuleEvents()
		{			
			DocumentEvents.Update.After += documentCustomDataModuleService.UpdateAfterEventHandler;
			DocumentEvents.Insert.After += documentCustomDataModuleService.InsertAfterEventHandler;
			WorkflowEvents.CheckIn.After += documentCustomDataModuleService.CheckInAfterEventHandler;
			WorkflowEvents.Publish.Before += documentCustomDataModuleService.PublishBeforeEventHandler;
		}
	}
}
