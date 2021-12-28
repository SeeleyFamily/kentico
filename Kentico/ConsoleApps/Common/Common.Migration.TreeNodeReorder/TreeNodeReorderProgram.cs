using CMS.DocumentEngine;
using Common.Migration.Infrastructure;
using Launchpad.Core.Extensions;
using System;
using System.Collections.Generic;

namespace Common.Migration.TreeNodeReorder
{
	public class TreeNodeReorderProgram : BaseProgram
	{
		#region Properties
		IEnumerable<ReorderNode> Nodes { get; set; }
		#endregion

		static void Main(string[] args)
		{
			var consoleApp = new TreeNodeReorderProgram();
			consoleApp.Main();
		}

		public TreeNodeReorderProgram() : base()
		{
		}

		public override void RunConsoleApp()
		{
			try
			{
				// Console App Business Logic Here;
				PopulateTreeNodes();
				ReorderTreeNodes();
			}
			catch (Exception e)
			{
				Messages.Add($"Error: {e.Message}");
			}
		}

		private void PopulateTreeNodes()
		{
			Nodes = new List<ReorderNode>()
			{
				// new ReorderNode(){NodeId = 1, NodeOrder=1, TargetNodeId = 1 ,  ReorderType = ReorderType.Exact },

			};
		}

		private void ReorderTreeNodes()
		{
			if (!Nodes.IsNullOrEmpty())
			{
				foreach (var node in Nodes)
				{
					try
					{
						if (node.ReorderType == ReorderType.Exact)
						{
							Tree.SetNodeOrder(node.NodeId, node.NodeOrder);
						}
						else if (node.ReorderType == ReorderType.BeforeTarget || node.ReorderType == ReorderType.AfterTarget)
						{
							if (node.TargetNodeId == 0)
							{
								continue;
							}
							var targetNode = DocumentHelper.GetDocument(node.TargetNodeId, DefaultCultureCode, Tree);

							var targetNodeOrder = targetNode.NodeOrder;
							if (node.ReorderType == ReorderType.AfterTarget)
							{
								targetNodeOrder++;
							}
							Tree.SetNodeOrder(node.NodeId, targetNodeOrder);
						}
						else if (node.ReorderType == ReorderType.First)
						{
							Tree.SetNodeOrder(node.NodeId, DocumentOrderEnum.First);
						}
						else if (node.ReorderType == ReorderType.Last)
						{
							Tree.SetNodeOrder(node.NodeId, DocumentOrderEnum.Last);
						}
					}
					catch (Exception e)
					{
						Messages.Add($"Error: {node.NodeId} : Error Reordering : {e.Message}");
					}
				}
			}
		}
	}
}
