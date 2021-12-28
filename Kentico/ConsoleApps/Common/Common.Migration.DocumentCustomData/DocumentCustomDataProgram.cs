using CMS.DocumentEngine;
using Common.Migration.Infrastructure;
using Launchpad.Core.Extensions;
using Launchpad.Infrastructure.Kentico.DocumentCustomData.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Common.Migration.TreeNodeMove
{
	public class DocumentCustomDataProgram : BaseProgram
	{
		#region Properties
		IEnumerable<int> NodeIds { get; set; }
		#endregion

		#region Fields
		private readonly DocumentCustomDataModuleService documentCustomDataModuleService;
		#endregion

		static void Main(string[] args)
		{
			var consoleApp = new DocumentCustomDataProgram();
			consoleApp.Main();
		}

		public DocumentCustomDataProgram() : base()
		{
			documentCustomDataModuleService = new DocumentCustomDataModuleService();
		}

		public override void RunConsoleApp()
		{
			try
			{
				// Console App Business Logic Here;
				PopulateTreeNodes();
				DocumentCustomData();
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
		private void DocumentCustomData()
		{
			bool allTreeNodes = ConfigurationManager.AppSettings.GetBoolValue("AllTreeNodes");
			if (allTreeNodes)
			{
				NodeIds = DocumentHelper.GetDocuments().Columns("NodeID").ToList().Select(x=>x.NodeID);
			}

			if (!NodeIds.IsNullOrEmpty())
			{
				foreach (var nodeId in NodeIds)
				{
					try
					{
						var document = DocumentHelper.GetDocument(nodeId, DefaultCultureCode, Tree);
						documentCustomDataModuleService.UpdateDocumentCustomDataEvent(document);
					}
					catch (Exception e)
					{
						Messages.Add($"Error: {nodeId} : Error Updating DocumentCustomData : {e.Message}");
					}
				}
			}
		}
	}
}
