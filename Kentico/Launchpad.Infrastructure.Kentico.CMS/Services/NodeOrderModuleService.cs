using CMS.DocumentEngine;
using Launchpad.Infrastructure.Services;

namespace Launchpad.Infrastructure.Kentico.CMS.Services
{

	public class NodeOrderModuleService
	{
		#region Fields		
		private readonly CustomCmsModuleLoggingService customCmsModuleLoggingService;
		#endregion

		public NodeOrderModuleService()
		{
			this.customCmsModuleLoggingService = new CustomCmsModuleLoggingService();
		}

		public void BeforeChangeOrder(object sender, DocumentChangeOrderEventArgs e)
        {
			customCmsModuleLoggingService.LogInformation("NodeOrderModuleService", "BeforeChangeOrder");
			TouchCacheKeys(e.Node);
        }

		private void TouchCacheKeys(TreeNode treeNode)
        {
			treeNode.Update(true);
        }
	}

}
