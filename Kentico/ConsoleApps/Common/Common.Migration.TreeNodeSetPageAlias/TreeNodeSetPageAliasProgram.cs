using CMS.DocumentEngine;
using Common.Migration.Infrastructure;
using Launchpad.Core.Extensions;
using System;
using System.Collections.Generic;

namespace Common.Migration.TreeNodeSetPageAlias
{
	public class TreeNodeSetPageAliasProgram : BaseProgram
	{
		#region Properties
		IEnumerable<AliasNode> Nodes { get; set; }
		#endregion

		static void Main(string[] args)
		{
			var consoleApp = new TreeNodeSetPageAliasProgram();
			consoleApp.Main();
		}

		public TreeNodeSetPageAliasProgram() : base()
		{
		}

		public override void RunConsoleApp()
		{
			try
			{
				// Console App Business Logic Here;
				PopulateTreeNodes();
				SetAliasTreeNodes();
			}
			catch (Exception e)
			{
				Messages.Add($"Error: {e.Message}");
			}
		}

		private void PopulateTreeNodes()
		{
			Nodes = new List<AliasNode>()
			{
				//new AliasNode(){NodeId = 1, Alias="New Alias"},
			};
		}

		private void SetAliasTreeNodes()
		{
			if (!Nodes.IsNullOrEmpty())
			{
				foreach (var node in Nodes)
				{
					try
					{
						var setAliasNode = DocumentHelper.GetDocument(node.NodeId, DefaultCultureCode, Tree);
						setAliasNode.NodeAlias = node.Alias.ToLower();
						setAliasNode.Update(true);
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
