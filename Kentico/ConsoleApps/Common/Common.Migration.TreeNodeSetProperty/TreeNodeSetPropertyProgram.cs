using CMS.DocumentEngine;
using Common.Migration.Infrastructure;
using Launchpad.Core.Extensions;
using System;
using System.Collections.Generic;

namespace Common.Migration.TreeNodeSetProperty
{
	public class TreeNodeSetPropertyProgram : BaseProgram
	{
		#region Properties
		IEnumerable<SetPropertyNode> Nodes { get; set; }
		#endregion

		static void Main(string[] args)
		{
			var consoleApp = new TreeNodeSetPropertyProgram();
			consoleApp.Main();
		}

		public TreeNodeSetPropertyProgram() : base()
		{
		}

		public override void RunConsoleApp()
		{
			try
			{
				// Console App Business Logic Here;
				PopulateTreeNodes();
				SetPropertyTreeNodes();
			}
			catch (Exception e)
			{
				Messages.Add($"Error: {e.Message}");
			}
		}

		private void PopulateTreeNodes()
		{
			Nodes = new List<SetPropertyNode>()
			{
				//NodeId,
				//new SetPropertyNode(){ NodeId = 1, Key = "key", Value=0 },
				//new SetPropertyNode(){ NodeId = 1, Key = "key", Value="value" },
				//new SetPropertyNode(){ NodeId = 1, Key = "key", Value=true },
			};
		}

		private void SetPropertyTreeNodes()
		{
			if (!Nodes.IsNullOrEmpty())
			{
				foreach (var node in Nodes)
				{
					try
					{
						var document = DocumentHelper.GetDocument(node.NodeId, DefaultCultureCode, Tree);
						// May want to consider using the following instead...
						// var document = DocumentHelper.GetDocuments().LatestVersion(false).WhereEquals("NodeId", nodeId).WithCoupledColumns().FirstOrDefault();

						bool isArchived = document.IsArchived;
						bool isPublished = document.IsPublished;

						document.SetValue(node.Key, node.Value);
						document.CheckOut();
						document.Update(true);
						document.CheckIn();

						if (isArchived)
						{
							document.Archive();
						}
						if (isPublished)
						{
							document.Publish();
						}
					}
					catch (Exception e)
					{
						Messages.Add($"Error: {node.NodeId} : Error Setting Property : {e.Message}");
					}
				}
			}
		}
	}
}
