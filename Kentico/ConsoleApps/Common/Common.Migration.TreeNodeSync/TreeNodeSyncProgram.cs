using CMS.DataEngine;
using CMS.DocumentEngine;
using Common.Migration.Infrastructure;
using Launchpad.Core.Extensions;
using System;
using System.Collections.Generic;

namespace Common.Migration.TreeNodeSync
{
	public class TreeNodeSyncProgram : BaseProgram
	{
		#region Properties
		IEnumerable<int> NodeIds { get; set; }
		#endregion

		static void Main(string[] args)
		{
			var consoleApp = new TreeNodeSyncProgram();
			consoleApp.Main();
		}

		public TreeNodeSyncProgram() : base()
		{
		}

		public override void RunConsoleApp()
		{
			try
			{
				// Console App Business Logic Here;
				PopulateTreeNodes();
				SyncTreeNodes();
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

		private void SyncTreeNodes()
		{
			if (!NodeIds.IsNullOrEmpty())
			{
				foreach (var nodeId in NodeIds)
				{
					try
					{
						var document = DocumentHelper.GetDocument(nodeId, DefaultCultureCode, Tree);
						// Ensures new version of the document is created
						document.CheckOut();
						document.Update(true);
						document.CheckIn();
						// Logs a chnange
						DocumentSynchronizationHelper.LogDocumentChange(document, TaskTypeEnum.UpdateDocument, Tree);
					}
					catch (Exception e)
					{
						Messages.Add($"Error: {nodeId} : Error Creating Sync Task : {e.Message}");
					}
				}
			}
		}
	}
}
