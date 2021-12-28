using CMS.DocumentEngine;
using Common.Migration.Infrastructure;
using Launchpad.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Migration.TreeNodeUpdateFromDatabase
{
	public class TreeNodeUpdateFromDatabaseProgram : BaseProgram
	{
		#region Properties
		IEnumerable<int> NodeIds { get; set; }
		#endregion

		static void Main(string[] args)
		{
			var consoleApp = new TreeNodeUpdateFromDatabaseProgram();
			consoleApp.Main();
		}

		public TreeNodeUpdateFromDatabaseProgram() : base()
		{
		}

		public override void RunConsoleApp()
		{
			try
			{
				// Console App Business Logic Here;
				PopulateTreeNodes();
				UpdateTreeNodesFromDatabase();
			}
			catch (Exception e)
			{
				Messages.Add($"Error: {e.Message}");
			}
		}

		private void PopulateTreeNodes()
		{
			NodeIds = new List<int>()
			{
				//NodeId,
			};
		}

		private void UpdateTreeNodesFromDatabase()
		{
			if (!NodeIds.IsNullOrEmpty())
			{
				foreach (var nodeId in NodeIds)
				{
					try
					{
						UpdateTreeNodeFromDatabase(nodeId);
					}
					catch (Exception e)
					{
						Messages.Add($"Error: {nodeId} : Error Publishing : {e.Message}");
					}
				}
			}
		}

		public void UpdateTreeNodeFromDatabase(int nodeId)
		{
			var document = DocumentHelper.GetDocument(nodeId, DefaultCultureCode, Tree);
			var databaseNode = DocumentHelper.GetDocuments().LatestVersion(false).WhereEquals("NodeId", nodeId).WithCoupledColumns().FirstOrDefault();

			var columns = document.ColumnNames.Except(TreeNode.New().ColumnNames.Union(new string[] { document.CoupledClassIDColumn }));
			columns = columns.Concat(new string[] {
				nameof(TreeNode.DocumentPageTitle),
				nameof(TreeNode.DocumentPageDescription),
				"DocumentPageBuilderWidgets",
			});
			bool doUpdate = false;
			foreach (var column in columns)
			{
				var currentCmsValue = document.GetStringValue(column, "");
				var currentDatabaseValue = databaseNode.GetStringValue(column, "");
				if (currentCmsValue != currentDatabaseValue)
				{
					document.SetValue(column, databaseNode[column]);
					doUpdate = true;
				}
			}
			if (doUpdate)
			{
				document.Update(true);
			}
		}
	}
}
