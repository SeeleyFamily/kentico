using CMS;
using CMS.DocumentEngine;
using Launchpad.Infrastructure.Kentico.CMS.Modules;
using Launchpad.Infrastructure.Kentico.CMS.Services;
using Launchpad.Infrastructure.Modules;

[assembly: RegisterModule(typeof(NodeAliasModule))]
namespace Launchpad.Infrastructure.Kentico.CMS.Modules
{
	class NodeAliasModule : CustomCmsModule
	{
		#region Fields
		private readonly NodeAliasModuleService nodeAliasModuleService;
		#endregion

		public NodeAliasModule()
			: base(nameof(NodeAliasModule))
		{
			nodeAliasModuleService = new NodeAliasModuleService();
			this.SettingDisableModuleCodeName = "DisableNodeAliasModule";

		}

		protected override void RegisterModuleEvents()
		{
			DocumentEvents.Update.Before += nodeAliasModuleService.UpdateBefore;
			DocumentEvents.Insert.Before += nodeAliasModuleService.InsertBefore;
			WorkflowEvents.Publish.After += nodeAliasModuleService.PublishBefore;
		}
	}
}
