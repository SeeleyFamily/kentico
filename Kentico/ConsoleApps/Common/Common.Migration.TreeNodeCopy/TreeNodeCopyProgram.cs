using CMS.DocumentEngine;
using Common.Migration.Infrastructure;
using Launchpad.Core.Extensions;
using System;
using System.Collections.Generic;

namespace Common.Migration.TreeNodeCopy
{
	public class TreeNodeCopyProgram : BaseProgram
	{
		#region Properties
		IEnumerable<CopyNode> Nodes { get; set; }
		#endregion

		static void Main(string[] args)
		{
			var consoleApp = new TreeNodeCopyProgram();
			consoleApp.Main();
		}

		public TreeNodeCopyProgram() : base()
		{
		}

		public override void RunConsoleApp()
		{
			try
			{
				// Console App Business Logic Here;
				PopulateTreeNodes();
				CopyTreeNodes();
			}
			catch (Exception e)
			{
				Messages.Add($"Error: {e.Message}");
			}
		}

		private void PopulateTreeNodes()
		{
			Nodes = new List<CopyNode>()
			{
				//new CopyNode(){NodeId = 1, TargetNodeId = 1 },

			};
		}

		private void CopyTreeNodes()
		{
			if (!Nodes.IsNullOrEmpty())
			{
				foreach (var node in Nodes)
				{
					try
					{
						var copyNode = DocumentHelper.GetDocument(node.NodeId, DefaultCultureCode, Tree);
						var targetNode = DocumentHelper.GetDocument(node.TargetNodeId, DefaultCultureCode, Tree);
						var newNode = DocumentHelper.CopyDocument(copyNode, targetNode, true, Tree);
					}
					catch (Exception e)
					{
						Messages.Add($"Error: {node.NodeId} : Error Copying : {e.Message}");
					}
				}
			}
		}
	}
}
