using CMS;
using Launchpad.Infrastructure.Kentico.CMS.Modules;
using Launchpad.Infrastructure.Kentico.CMS.Services;
using Launchpad.Infrastructure.Modules;

[assembly: RegisterModule(typeof(NodeOrderModule))]
namespace Launchpad.Infrastructure.Kentico.CMS.Modules
{
	class NodeOrderModule : CustomCmsModule
	{
		private readonly NodeOrderModuleService nodeOrderModuleService;

		public NodeOrderModule() : base(nameof(NodeOrderModule))
		{
			nodeOrderModuleService = new NodeOrderModuleService();
			this.SettingDisableModuleCodeName = "DisableNodeOrderModule";

		}

		protected override void RegisterModuleEvents()
		{
			// This implementation was used to clear the cache on Node Reorder
			// This currently causes a stack overflow issue
			// Commenting out
			//DocumentEvents.ChangeOrder.Before += nodeOrderModuleService.BeforeChangeOrder;
		}
	}
}
