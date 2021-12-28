using CMS.DocumentEngine;
using Common.Migration.Infrastructure;
using Launchpad.Core.Extensions;
using System;
using System.Collections.Generic;

namespace Common.Migration.TreeNodePublish
{
	public class TreeNodePublishProgram : BaseProgram
	{
		#region Properties
		IEnumerable<int> NodeIds { get; set; }
		#endregion

		static void Main(string[] args)
		{
			var consoleApp = new TreeNodePublishProgram();
			consoleApp.Main();
		}

		public TreeNodePublishProgram() : base()
		{
		}

		public override void RunConsoleApp()
		{
			try
			{
				// Console App Business Logic Here;
				PopulateTreeNodes();
				PublishTreeNodes();
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

		private void PublishTreeNodes()
		{
			if (!NodeIds.IsNullOrEmpty())
			{
				foreach (var nodeId in NodeIds)
				{
					try
					{
						var document = DocumentHelper.GetDocument(nodeId, DefaultCultureCode, Tree);
						document.Publish();
					}
					catch (Exception e)
					{
						Messages.Add($"Error: {nodeId} : Error Publishing : {e.Message}");
					}
				}
			}
		}
	}
}
