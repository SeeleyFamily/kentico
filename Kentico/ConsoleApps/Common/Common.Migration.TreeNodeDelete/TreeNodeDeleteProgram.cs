using CMS.DocumentEngine;
using CMS.SiteProvider;
using Common.Migration.Infrastructure;
using Launchpad.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Common.Migration.TreeNodeDelete
{
	public class TreeNodeDeleteProgram : BaseProgram
	{
		#region Properties
		IEnumerable<int> NodeIds { get; set; }
		#endregion

		static void Main(string[] args)
		{
			var consoleApp = new TreeNodeDeleteProgram();
			consoleApp.Main();
		}

		public TreeNodeDeleteProgram() : base()
		{
		}

		public override void RunConsoleApp()
		{
			try
			{
				// Console App Business Logic Here;
				PopulateTreeNodes();
				DeleteTreeNodes();
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

		private void DeleteTreeNodes()
		{
			if (!NodeIds.IsNullOrEmpty())
			{
				foreach (var nodeId in NodeIds)
				{
					try
					{
						var document = DocumentHelper.GetDocument(nodeId, DefaultCultureCode, Tree);
						document.Delete(true, true);
					}
					catch (Exception e)
					{
						Messages.Add($"Error: {nodeId} : Error Deleting : {e.Message}");
					}
				}
			}
		}
	}
}
