using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using CMS.Base;
using CMS.DataEngine;
using CMS.DocumentEngine;
using CMS.Helpers;
using CMS.Synchronization;
using Common.Migration.Infrastructure;


namespace Common.Migration.DocumentUrlPath
{
	public class Program : BaseProgram
	{
		static void Main(string[] args)
		{
			var consoleApp = new Program();
			consoleApp.Main();
		}

		public Program() : base()
		{
		}

		public override void RunConsoleApp()
		{
			try
			{
				// Console App Business Logic Here;
				UpdateDocumentUrlPath();
			}
			catch (Exception e)
			{
				Messages.Add($"Error: {e.Message}");
			}
		}

		private static void UpdateDocumentUrlPath()
		{
			var siteId = MigrationUtilities.GetSiteId();
			List<string> ErrorMessages = new List<string>();

			string classNames = ConfigurationManager.AppSettings["ClassNames"];
			string nodeAliasPath = ConfigurationManager.AppSettings["NodeAliasPath"];


			var treeNodesQuery = DocumentHelper.GetDocuments()
				.OnSite( siteId, true )
				.Path( ( !string.IsNullOrWhiteSpace( nodeAliasPath ) ? nodeAliasPath : "/" ), PathTypeEnum.Children )
				.WhereNotEquals( "ClassName", "CMS.Root" );
				
			// Restrict to a smaller subset of assets with specified classname(s)
			if (!string.IsNullOrWhiteSpace(classNames))
			{
				var classNamesList = classNames.Split(',').Join("','");
				treeNodesQuery = treeNodesQuery.Where($"ClassName in ('{ classNamesList }')");
			}

			var treeNodes = treeNodesQuery.ToList();

			TreeProvider tree = new TreeProvider( CMSActionContext.CurrentUser )
			{
				LogSynchronization = true
			};


			foreach (var node in treeNodes)
			{
				if( !node.HasUrl() )
				{
					continue;
				}


				try
				{
					string originalPath = node.DocumentCustomData["DocumentUrlPath"]?.ToString();

					string documentUrlPath = "/" + node.DocumentsOnPath
												 .Where( n => n.HasUrl() )
												 .Select( n => n.NodeAlias.ToLower() )
												 .ToArray()
												 .Join( "/" );


					if( originalPath != documentUrlPath )
					{
						node.DocumentCustomData.SetValue( "DocumentUrlPath", documentUrlPath );
						node.Update();						


						// Log a staging task for the update
						DocumentSynchronizationHelper.LogDocumentChange( node, TaskTypeEnum.UpdateDocument, tree, -1, ( TaskParameters ) null, false );
					}
				}
				catch (Exception e)
				{
					ErrorMessages.Add($"Issue Updating Node: {node.NodeID}, Error: {e.Message}");
				}
			}

			if (ErrorMessages != null && ErrorMessages.Any())
			{
				Console.WriteLine($"There were {ErrorMessages.Count()} errors.");
				Console.WriteLine(ErrorMessages.Join("\n"));
			}
		}
	}
}
