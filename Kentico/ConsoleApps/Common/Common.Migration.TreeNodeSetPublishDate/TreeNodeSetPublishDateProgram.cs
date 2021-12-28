using CMS.DocumentEngine;
using Common.Migration.Infrastructure;
using Launchpad.Core.Extensions;
using System;
using System.Collections.Generic;

namespace Common.Migration.TreeNodeSetPublishDate
{
	public class TreeNodeSetPublishDateProgram : BaseProgram
	{
		#region Properties
		IEnumerable<PublishDateNode> Nodes { get; set; }
		#endregion

		static void Main(string[] args)
		{
			var consoleApp = new TreeNodeSetPublishDateProgram();
			consoleApp.Main();
		}

		public TreeNodeSetPublishDateProgram() : base()
		{
		}

		public override void RunConsoleApp()
		{
			try
			{
				// Console App Business Logic Here;
				PopulateTreeNodes();
				SetPublishDateTreeNodes();
			}
			catch (Exception e)
			{
				Messages.Add($"Error: {e.Message}");
			}
		}

		private void PopulateTreeNodes()
		{
			Nodes = new List<PublishDateNode>()
			{
				//new PublishDateNode() { NodeId = 1, PublishFrom = DateTime.Now.AddDays(1), PublishTo = DateTime.Now.AddDays(10) },
				//new PublishDateNode() { NodeId = 1, PublishFrom = DateTime.Now.AddDays(20) },
				//new PublishDateNode() { NodeId = 1, PublishTo = DateTime.Now.AddDays(30) },
			};
		}

		private void SetPublishDateTreeNodes()
		{
			if (!Nodes.IsNullOrEmpty())
			{
				foreach (var node in Nodes)
				{
					try
					{
						var publishDateNode = DocumentHelper.GetDocument(node.NodeId, DefaultCultureCode, Tree);
						bool doUpdate = false;
						if(node.PublishFrom != null)
						{
							publishDateNode.DocumentPublishFrom = node.PublishFrom.GetValueOrDefault();
							doUpdate = true;
						}
						if(node.PublishTo != null)
						{
							publishDateNode.DocumentPublishTo = node.PublishTo.GetValueOrDefault();
							doUpdate = true;
						}
						if (doUpdate)
						{
							publishDateNode.Update(true);
						}
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
