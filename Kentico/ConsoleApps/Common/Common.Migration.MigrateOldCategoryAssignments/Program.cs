using CMS.DocumentEngine;
using CMS.Helpers;
using Common.Migration.Infrastructure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Common.Migration.MigrateOldCategoryAssignments
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
				AssignOldCategories();

			}
			catch (Exception e)
			{
				Messages.Add($"Error: {e.Message}");
			}
		}

		private static void AssignOldCategories()
		{
			//var siteId = MigrationUtilities.GetSiteId();
			//List<string> ErrorMessages = new List<string>();

			//string classNames = ConfigurationManager.AppSettings["ClassNames"];
			//string nodeAliasPath = ConfigurationManager.AppSettings["NodeAliasPath"];


			//var treeNodesQuery = DocumentHelper.GetDocuments()
			//	.WithCoupledColumns()
			//	.OnSite(siteId, true)
			//	.Path((!string.IsNullOrWhiteSpace(nodeAliasPath) ? nodeAliasPath : "/"), PathTypeEnum.Children);

			//// Restrict to a smaller subset of assets with specified classname(s)
			//if (!string.IsNullOrWhiteSpace(classNames))
			//{
			//	var classNamesList = classNames.Split(',').Join("','");
			//	treeNodesQuery = treeNodesQuery.Where($"ClassName in ('{ classNamesList }')");
			//}

			//var references = ReferencesInfoProvider.GetReferences().Where(x => x.ReferenceType.Equals("Category", StringComparison.InvariantCultureIgnoreCase)).ToList();


			//var treeNodes = treeNodesQuery.ToList();

			//foreach (var node in treeNodes)
			//{
			//	try
			//	{
			//		var oldCategories = node["Categories"];
			//		if (oldCategories != null)
			//		{
			//			var categories = oldCategories.ToString().Split('|').Select(x => { return x.Replace("{", "").Replace("}", ""); }).ToList();
			//			var referencedCategories = references.Where(x => categories.Contains(x.OldReference)).ToList();
			//			foreach (var rc in referencedCategories)
			//			{
			//				if (int.TryParse(rc.NewReference, out int categoryId))
			//				{
			//					DocumentCategoryInfoProvider.AddDocumentToCategory(node.DocumentID, categoryId);
			//				}
			//			}

			//		}
			//	}
			//	catch (Exception e)
			//	{
			//		ErrorMessages.Add($"Issue Updating Node: {node.NodeID}, Error: {e.Message}");
			//	}

			//}

			//if (ErrorMessages != null && ErrorMessages.Any())
			//{
			//	Console.WriteLine($"There were {ErrorMessages.Count()} errors.");
			//	Console.WriteLine(ErrorMessages.Join("\n"));
			//}
		}
	}
}
