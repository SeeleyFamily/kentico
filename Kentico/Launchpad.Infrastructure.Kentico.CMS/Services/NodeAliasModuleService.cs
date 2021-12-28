using CMS.DocumentEngine;
using Launchpad.Infrastructure.Services;

namespace Launchpad.Infrastructure.Kentico.CMS.Services
{
	public class NodeAliasModuleService
	{

		#region Fields		
		private readonly CustomCmsModuleLoggingService customCmsModuleLoggingService;
		#endregion

		public NodeAliasModuleService()
		{
			this.customCmsModuleLoggingService = new CustomCmsModuleLoggingService();
		}

		internal void UpdateBefore(object sender, DocumentEventArgs e)
		{
			customCmsModuleLoggingService.LogInformation("NodeAliasModuleService", "UpdateBefore");
			HandleNodeAliasPath(e.Node, e.TreeProvider);
		}

		internal void InsertBefore(object sender, DocumentEventArgs e)
		{
			customCmsModuleLoggingService.LogInformation("NodeAliasModuleService", "InsertBefore");
			HandleNodeAliasPath(e.Node, e.TreeProvider);
		}

		internal void PublishBefore(object sender, WorkflowEventArgs e)
		{
			customCmsModuleLoggingService.LogInformation("NodeAliasModuleService", "PublishBefore");
			HandleNodeAliasPath(e.Document, e.TreeProvider);
		}

		public void HandleNodeAliasPath(TreeNode node, TreeProvider tree)
		{
			node.NodeAlias = node.NodeAlias.ToLower();
		}
	}
}
