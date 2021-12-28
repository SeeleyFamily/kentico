using CMS.DocumentEngine;
using Common.Migration.Infrastructure;
using Launchpad.Core.Extensions;
using System;
using System.Collections.Generic;

namespace Common.Migration.TreeNodeRename
{
	public class TreeNodeRenameProgram : BaseProgram
	{
		#region Properties
		IEnumerable<RenameNode> Nodes { get; set; }
		#endregion

		static void Main(string[] args)
		{
			var consoleApp = new TreeNodeRenameProgram();
			consoleApp.Main();
		}

		public TreeNodeRenameProgram() : base()
		{
		}

		public override void RunConsoleApp()
		{
			try
			{
				// Console App Business Logic Here;
				PopulateTreeNodes();
				RenameTreeNodes();
			}
			catch (Exception e)
			{
				Messages.Add($"Error: {e.Message}");
			}
		}

		private void PopulateTreeNodes()
		{
			Nodes = new List<RenameNode>()
			{
				//new RenameNode(){NodeId = 1, Name="New Name"},
			};
		}

		private void RenameTreeNodes()
		{
			if (!Nodes.IsNullOrEmpty())
			{
				foreach (var node in Nodes)
				{
					try
					{
						var renameNode = DocumentHelper.GetDocument(node.NodeId, DefaultCultureCode, Tree);
						renameNode.DocumentName = node.Name;
						renameNode.Update(true);
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
