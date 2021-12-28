using CMS.DocumentEngine;
using Common.Migration.Infrastructure;
using Common.Migration.TreeNodeUpdateFromDatabase;
using Launchpad.Core.Extensions;
using System;
using System.Linq;

namespace Common.Migration.UpdateAllWorkflowNodesFromDatabase
{
	public class UpdateAllWorkflowNodesFromDatabaseProgram : BaseProgram
	{
		static void Main(string[] args)
		{
			var consoleApp = new UpdateAllWorkflowNodesFromDatabaseProgram();
			consoleApp.Main();
		}

		public UpdateAllWorkflowNodesFromDatabaseProgram() : base()
		{
		}

		public override void RunConsoleApp()
		{
			try
			{
				// Console App Business Logic Here;
				UpdateAllWorkflowNodesFromDatabase();
			}
			catch (Exception e)
			{
				Messages.Add($"Error: {e.Message}");
			}
		}

		private void UpdateAllWorkflowNodesFromDatabase()
		{
			var nodeIds = DocumentHelper.GetDocuments().WhereNotNull(nameof(TreeNode.DocumentWorkflowStepID)).Columns(nameof(TreeNode.NodeID)).ToList().Select(x => x.NodeID);
			if (!nodeIds.IsNullOrEmpty())
			{
				// Re-use business logic from TreeNodeUpdateFromDatabaseProgram;
				var consoleApp = new TreeNodeUpdateFromDatabaseProgram();

				foreach (var nodeId in nodeIds)
				{
					try
					{
						consoleApp.UpdateTreeNodeFromDatabase(nodeId);
					}
					catch (Exception e)
					{
						Messages.Add($"Error: {nodeId} : Error Publishing : {e.Message}");
					}
				}
			}
		}
	}
}
