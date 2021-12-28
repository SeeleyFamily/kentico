using CMS.DocumentEngine;
using Common.Migration.Infrastructure;
using Launchpad.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Migration.UpdateAllTreeNodesNodeOrder
{
	public class UpdateAllTreeNodesNodeOrderProgram : BaseProgram
	{
		#region Properties
		List<TreeNode> treeNodes { get; set; }
		#endregion

		public UpdateAllTreeNodesNodeOrderProgram() : base()
		{
		}
		static void Main(string[] args)
		{
			var consoleApp = new UpdateAllTreeNodesNodeOrderProgram();
			consoleApp.Main();
		}
		public override void RunConsoleApp()
		{
			try
			{
				// Console App Business Logic Here;
				PopulateTreeNodes();
				UpdateAllTreeNodesNodeOrder();
			}
			catch (Exception e)
			{
				Messages.Add($"Error: {e.Message}");
			}
		}

		private void PopulateTreeNodes()
		{
			treeNodes = DocumentHelper.GetDocuments().Where(d => d.NodeOrder == 1).ToList();
		}

		private void UpdateAllTreeNodesNodeOrder()
		{
			if (!treeNodes.IsNullOrEmpty())
			{
				foreach (var node in treeNodes)
				{
					Tree.SetNodeOrder(node.NodeID, DocumentOrderEnum.First);
					node.Publish();
				}
			}
		}
	}
}
