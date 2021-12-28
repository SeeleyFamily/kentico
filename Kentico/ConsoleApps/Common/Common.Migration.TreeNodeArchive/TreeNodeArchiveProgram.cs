using CMS.DocumentEngine;
using Common.Migration.Infrastructure;
using Launchpad.Core.Extensions;
using System;
using System.Collections.Generic;

namespace Common.Migration.TreeNodeArchive
{
	public class TreeNodeArchiveProgram : BaseProgram
	{
		#region Properties
		IEnumerable<int> NodeIds { get; set; }
		#endregion

		static void Main(string[] args)
		{
			var consoleApp = new TreeNodeArchiveProgram();
			consoleApp.Main();
		}

		public TreeNodeArchiveProgram() : base()
		{
		}

		public override void RunConsoleApp()
		{
			try
			{
				// Console App Business Logic Here;
				PopulateTreeNodes();
				ArchiveTreeNodes();
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

		private void ArchiveTreeNodes()
		{
			if (!NodeIds.IsNullOrEmpty())
			{
				foreach (var nodeId in NodeIds)
				{
					try
					{
						var document = DocumentHelper.GetDocument(nodeId, DefaultCultureCode, Tree);
						document.Archive();
					}
					catch (Exception e)
					{
						Messages.Add($"Error: {nodeId} : Error Archiving : {e.Message}");
					}
				}
			}
		}
	}
}
