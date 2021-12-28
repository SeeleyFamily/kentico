using CMS;
using CMS.Search.Azure;
using Launchpad.Infrastructure.Kentico.CMS.Modules;
using Launchpad.Infrastructure.Kentico.CMS.Services;
using Launchpad.Infrastructure.Modules;

[assembly: RegisterModule(typeof(AzureSearchModule))]
namespace Launchpad.Infrastructure.Kentico.CMS.Modules
{

	public class AzureSearchModule : CustomCmsModule
	{
		#region Fields
		private readonly AzureSearchModuleService azureSearchModuleService;
		#endregion

		public AzureSearchModule()
			: base(nameof(AzureSearchModule))
		{
			azureSearchModuleService = new AzureSearchModuleService();
			this.SettingDisableModuleCodeName = "DisableAzureSearchModule";
		}

		protected override void RegisterModuleEvents()
		{
			SearchServiceManager.CreatingOrUpdatingIndex.Execute += azureSearchModuleService.OnCreatingOrUpdatingIndex;
			//DocumentCreator.Instance.CreatingDocument.Before += azureSearchModuleService.OnCreatingDocumentBefore;
			DocumentCreator.Instance.CreatingDocument.After += azureSearchModuleService.OnCreatingDocumentAfter;
			//DocumentFieldCreator.Instance.CreatingField.Before += azureSearchModuleService.OnCreatingFieldBefore;
			//DocumentFieldCreator.Instance.CreatingFields.Before += azureSearchModuleService.OnCreatingFieldsBefore;
			//DocumentEvents.GetContent.Execute += _AzureSearchModuleService.OnGetContent;	// Necessary if we want to compile child content into a document's searchable content field, etc			
			DocumentFieldCreator.Instance.CreatingFields.After += azureSearchModuleService.OnCreatingFieldsAfter;
		}
	}

}
