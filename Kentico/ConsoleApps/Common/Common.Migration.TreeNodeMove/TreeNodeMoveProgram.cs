using CMS.DocumentEngine;
using Common.Migration.Infrastructure;
using Launchpad.Core.Extensions;
using System;
using System.Collections.Generic;

namespace Common.Migration.TreeNodeMove
{
	public class TreeNodeMoveProgram : BaseProgram
	{
		#region Properties
		IEnumerable<MoveNode> Nodes { get; set; }
		#endregion

		static void Main(string[] args)
		{
			var consoleApp = new TreeNodeMoveProgram();
			consoleApp.Main();
		}

		public TreeNodeMoveProgram() : base()
		{
		}

		public override void RunConsoleApp()
		{
			try
			{
				// Console App Business Logic Here;
				PopulateTreeNodes();
				MoveTreeNodes();
			}
			catch (Exception e)
			{
				Messages.Add($"Error: {e.Message}");
			}
		}

		private void PopulateTreeNodes()
		{
			Nodes = new List<MoveNode>()
			{
				//new MoveNode(){NodeId = 1, TargetNodeId = 1 },

			};
		}

		private void MoveTreeNodes()
		{
			if (!Nodes.IsNullOrEmpty())
			{
				foreach (var node in Nodes)
				{
					try
					{
						var moveNode = DocumentHelper.GetDocument(node.NodeId, DefaultCultureCode, Tree);
						var targetNode = DocumentHelper.GetDocument(node.TargetNodeId, DefaultCultureCode, Tree);
						DocumentHelper.MoveDocument(moveNode, targetNode, Tree);
					}
					catch (Exception e)
					{
						Messages.Add($"Error: {node.NodeId} : Error Moving : {e.Message}");
					}
				}
			}
		}
	}
}
