using CMS.DocumentEngine;
using CMS.Helpers;
using Common.Migration.Infrastructure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Common.Migration.MassAssignCategories
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
				AssignCategory();

			}
			catch (Exception e)
			{
				Messages.Add($"Error: {e.Message}");
			}
		}

		private static void AssignCategory()
		{
			var siteId = MigrationUtilities.GetSiteId();
			List<string> ErrorMessages = new List<string>();

			string classNames = ConfigurationManager.AppSettings["ClassNames"];
			string nodeAliasPath = ConfigurationManager.AppSettings["NodeAliasPath"];
			string categoryIdsSettings = ConfigurationManager.AppSettings["CategoryIds"];
			if (categoryIdsSettings == null)
			{
				throw new Exception("Category Ids can not be null or empty");
			}
			var categoryIds = categoryIdsSettings.ToString().Split(',').ToList();
			if (!categoryIds.Any())
			{
				throw new Exception("Category Ids can not be null or empty");
			}

			var treeNodesQuery = DocumentHelper.GetDocuments()
				.WithCoupledColumns()
				.OnSite(siteId, true)
				.Path((!string.IsNullOrWhiteSpace(nodeAliasPath) ? nodeAliasPath : "/"), PathTypeEnum.Children);

			// Restrict to a smaller subset of assets with specified classname(s)
			if (!string.IsNullOrWhiteSpace(classNames))
			{
				var classNamesList = classNames.Split(',').Join("','");
				treeNodesQuery = treeNodesQuery.Where($"ClassName in ('{ classNamesList }')");
			}

			var treeNodes = treeNodesQuery.ToList();

			foreach (var node in treeNodes)
			{
				try
				{
					foreach (var cat in categoryIds)
					{
						if (int.TryParse(cat, out int categoryId))
						{
							DocumentCategoryInfo.Provider.Add(node.DocumentID, categoryId);
						}
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
