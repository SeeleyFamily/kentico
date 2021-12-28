using CMS.DocumentEngine;
using CMS.Helpers;
using CMS.MediaLibrary;
using CMS.SiteProvider;
using Common.Migration.Infrastructure;
using Launchpad.Core.Constants;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;

namespace Common.Migration.ReferenceReport
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
				ReferenceReport();

			}
			catch (Exception e)
			{
				Messages.Add($"Error: {e.Message}");
			}
		}

		private static void ReferenceReport()
		{
			var siteId = MigrationUtilities.GetSiteId();
			string classNames = ConfigurationManager.AppSettings["ClassNames"];
			string nodeAliasPath = ConfigurationManager.AppSettings["NodeAliasPath"];
			string additionalDomains = ConfigurationManager.AppSettings["AdditionalDomains"];

			List<string> ErrorMessages = new List<string>();

			var allMediaFiles = MediaFileInfo.Provider.Get().OnSite(siteId, true).ToList();
			var allPermanentUrls = allMediaFiles.Select(x => MediaLibraryHelper.GetPermanentUrl(x).ToLower());
			var allContentTreeNodesQuery = DocumentHelper.GetDocuments().OnSite(siteId, true).WithCoupledColumns()
								.Path((!string.IsNullOrWhiteSpace(nodeAliasPath) ? nodeAliasPath : "/"), PathTypeEnum.Section)
								.ExcludePath("/Content-Migration", PathTypeEnum.Section);

			// Restrict to a smaller subset of assets with specified classname(s)
			if (!string.IsNullOrWhiteSpace(classNames))
			{
				var classNamesQuery = classNames.Split(',').Join("','");
				allContentTreeNodesQuery = allContentTreeNodesQuery.Where($"ClassName in ('{classNamesQuery}')");
			}

			var allContentTreeNodes = allContentTreeNodesQuery.ToList();
			var allDocumentUrlPaths = allContentTreeNodes.Select(x =>
			{
				if (x["DocumentUrlPath"] != null)
				{
					return x["DocumentUrlPath"].ToString().ToLower();
				}
				else
				{
					return null;
				}
			});
			var domains = SiteDomainAliasInfoProvider.GetDomainAliases(siteId).ToList();
			var domainAliases = domains.Select(x => x.SiteDomainAliasName.Trim('/')).ToList();
			var currentSite = SiteInfo.Provider.Get().Where(x => x.SiteID == siteId).FirstOrDefault();


			var allSiteDomains = new List<string>();

			allSiteDomains.Add(currentSite.SitePresentationURL.Trim('/'));
			allSiteDomains.Add(currentSite.DomainName.Trim('/'));
			allSiteDomains.AddRange(domainAliases);

			if (!string.IsNullOrWhiteSpace(additionalDomains))
			{
				allSiteDomains.AddRange(additionalDomains.Split(',').ToList());
			}

			var totalCount = allContentTreeNodes.Count();
			var currentCount = 0;

			foreach (var node in allContentTreeNodes)
			{
				currentCount++;
				var defaultFields = TreeNode.New().Properties.Union(new string[] { "Fields" }).ToList();
				var contentFields = node.Properties.Except(defaultFields);
				contentFields = contentFields.Except(new string[] { "OldPath", "NewNodeAliasPath", "SlideshowNewUrl" });
				contentFields = contentFields.Append("DocumentPageBuilderWidgets");
				foreach (string field in contentFields)
				{
					var fieldValue = node[field];
					if (fieldValue != null)
					{
						List<string> allReferences = new List<string>();

						var fieldStringValue = fieldValue.ToString().Trim();

						// Get all src matches
						var srcMatches = Regex.Matches(fieldStringValue, "src=[\"'](.+?)[\"'].*?", RegexOptions.IgnoreCase);
						foreach (Match srcMatch in srcMatches)
						{
							var value = srcMatch.Value.ToString();
							if (value.Contains("."))
							{
								allReferences.Add(value.Replace("src=\"", "").Trim('"'));
							}
						}

						// Get all href matches
						var hrefMatches = Regex.Matches(fieldStringValue, "href=[\"'](.+?)[\"'].*?", RegexOptions.IgnoreCase);
						foreach (Match hrefMatch in hrefMatches)
						{
							var value = hrefMatch.Value.ToString();
							if (value.Contains("."))
							{
								allReferences.Add(value.Replace("href=\"", "").Trim('"'));
							}
						}

						// If the field value is a relative url
						if (fieldStringValue.Trim().StartsWith("/"))
						{
							allReferences.Add(fieldStringValue);
						}

						// Starts With Kentico's ~/ Reference
						if (fieldStringValue.StartsWith("~/"))
						{
							allReferences.Add(fieldStringValue);
						}

						// Check if the Field Value is a Url
						try
						{
							Uri uri = new Uri(fieldStringValue);
							allReferences.Add(fieldStringValue);
						}
						catch (Exception)
						{
							// Reference is not a valid Url
						}

						if (field.Equals("DocumentPageBuilderWidgets"))
						{
							// Get all src matches
							var pbSrcMatches = Regex.Matches(fieldStringValue, "src=(.+?)\"(.+?)(.+?)\"", RegexOptions.IgnoreCase);
							foreach (Match srcMatch in pbSrcMatches)
							{
								var value = srcMatch.Value.ToString();
								if (value.Contains("."))
								{
									var replacedValue = value.Replace("\\\"", "").Replace("src=", "").Trim('"');
									allReferences.Add(replacedValue);
								}
							}

							// Get all href matches
							var pbHrefMatches = Regex.Matches(fieldStringValue, "href=(.+?)\"(.+?)(.+?)\"", RegexOptions.IgnoreCase);
							foreach (Match hrefMatch in pbHrefMatches)
							{
								var value = hrefMatch.Value.ToString();
								if (value.Contains("."))
								{
									var replacedValue = value.Replace("\\\"", "").Replace("href=", "").Trim('"');
									allReferences.Add(replacedValue);
								}
							}


							var pagebuilderMatches = Regex.Matches(fieldStringValue, "\".+?\":\"(.+?)\"", RegexOptions.IgnoreCase);
							foreach (Match match in pagebuilderMatches)
							{
								var pbValue = match.Value.ToString();
								pbValue = match.Groups[1].Value.ToString();
								// If the field value is a relative url
								if (pbValue.Trim().StartsWith("/"))
								{
									allReferences.Add(pbValue);
								}

								// Starts With Kentico's ~/ Reference
								if (pbValue.StartsWith("~/"))
								{
									allReferences.Add(pbValue);
								}

								// Check if the Field Value is a Url
								try
								{
									Uri uri = new Uri(pbValue);
									allReferences.Add(pbValue);
								}
								catch (Exception)
								{
									// Reference is not a valid Url
								}
							}
						}


						if (allReferences.Any())
						{
							foreach (var reference in allReferences)
							{
								var currentReference = reference;
								if (currentReference.IndexOf('?') >= 0)
								{
									currentReference = currentReference.Substring(0, currentReference.IndexOf('?'));
								}
								if (allPermanentUrls.Any(x => x.Contains(currentReference.ToLower().TrimStart('~'))))
								{
									continue;
								}
								if (allDocumentUrlPaths.Contains(currentReference.ToLower().TrimStart('~')))
								{
									continue;
								}
								try
								{
									Uri uri = new Uri(currentReference);
									if (!allSiteDomains.Contains(uri.Host.Trim('/')))
									{
										continue;
									}
								}
								catch (Exception)
								{
									// Reference is not a valid Url
								}
								// IGNORE MAILTO 
								if (currentReference.StartsWith("mailto:"))
								{
									continue;
								}
								Console.WriteLine($"DocumentUrlPath,{node.DocumentCustomData["DocumentUrlPath"]?.ToString()},DocumentID,{node.DocumentID},NodeID,{node.NodeID},DocumentForeignKeyValue,{node[Constants.DocumentForeignKeyValueColumnName].ToString()},ClassName,{node.ClassName},Field,{field},Reference,{reference}");
							}
						}
					}
				}
			}

			if (ErrorMessages != null && ErrorMessages.Any())
			{
				ErrorMessages.Prepend($"Proccessed {currentCount}:{totalCount}");
				Console.WriteLine($"There were {ErrorMessages.Count()} errors.");
				Console.WriteLine(ErrorMessages.Join("\n"));
			}

		}
	}
}
