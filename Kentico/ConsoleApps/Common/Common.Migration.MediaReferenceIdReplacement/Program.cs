using CMS.DocumentEngine;
using CMS.Helpers;
using CMS.MediaLibrary;
using CMS.SiteProvider;
using Common.Migration.Infrastructure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;

namespace Common.Migration.MediaReferenceIdReplacement
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
				MovePageNodes();

			}
			catch (Exception e)
			{
				Messages.Add($"Error: {e.Message}");
			}
		}

		private static void MovePageNodes()
		{
			//var siteId = MigrationUtilities.GetSiteId();
			//List<string> ErrorMessages = new List<string>();

			//string mediaLibraryFileReferenceDomains = ConfigurationManager.AppSettings["MediaLibraryFileReferenceDomains"];
			//string mediaLibraryFileNodeClassNames = ConfigurationManager.AppSettings["MediaLibraryFileNodeClassNames"];
			//string mediaLibraryIdRegexPattern = ConfigurationManager.AppSettings["MediaLibraryIdRegexPattern"];
			//string mediaLibraryOldReferenceRegexPattern = ConfigurationManager.AppSettings["MediaLibraryOldReferenceRegexPattern"];
			//string mediaLibraryReferencesNodeAliasPath = ConfigurationManager.AppSettings["MediaLibraryReferencesNodeAliasPath"];

			//var allMediaFiles = MediaFileInfoProvider.GetMediaFiles().OnSite(siteId, true).ToList();
			//var allMediaReferences = ReferencesInfoProvider.GetReferences().Where(x => x.ReferenceType.Equals("Media", StringComparison.InvariantCultureIgnoreCase)).ToList();

			//var allContentTreeNodesQuery = DocumentHelper.GetDocuments().OnSite(siteId, true).WithCoupledColumns()
			//					.Path((!string.IsNullOrWhiteSpace(mediaLibraryReferencesNodeAliasPath) ? mediaLibraryReferencesNodeAliasPath : "/"), PathTypeEnum.Children);

			//// Restrict to a smaller subset of assets with specified classname(s)
			//if (!string.IsNullOrWhiteSpace(mediaLibraryFileNodeClassNames))
			//{
			//	var classNames = mediaLibraryFileNodeClassNames.Split(',').Join("','");
			//	allContentTreeNodesQuery = allContentTreeNodesQuery.Where($"ClassName in ('{classNames}')");
			//}

			//var allContentTreeNodes = allContentTreeNodesQuery.ToList();


			//var domains = SiteDomainAliasInfoProvider.GetDomainAliases(siteId).ToList();
			//var domainAliases = domains.Select(x => x.SiteDomainAliasName.Trim('/')).ToList();
			//var currentSite = SiteInfoProvider.GetSites().Where(x => x.SiteID == siteId).FirstOrDefault();

			//var allSiteDomains = new List<string>();

			//allSiteDomains.Add(currentSite.SitePresentationURL.Trim('/'));
			//allSiteDomains.Add(currentSite.DomainName.Trim('/'));
			//allSiteDomains.AddRange(domainAliases);

			//if (!string.IsNullOrWhiteSpace(mediaLibraryFileReferenceDomains))
			//{
			//	allSiteDomains = mediaLibraryFileReferenceDomains.Split(',').ToList();
			//}

			//var totalCount = allContentTreeNodes.Count();
			//var currentCount = 0;
			//var updatedCount = 0;

			//foreach (var node in allContentTreeNodes)
			//{
			//	currentCount++;
			//	var defaultFields = TreeNode.New().Properties.Union(new string[] { "Fields" }).ToList();
			//	var contentFields = node.Properties.Except(defaultFields);
			//	foreach (string field in contentFields)
			//	{
			//		var fieldValue = node[field];
			//		if (fieldValue != null)
			//		{
			//			List<string> fieldMediaFileReferences = new List<string>();

			//			var fieldStringValue = fieldValue.ToString().Trim();

			//			var didRegexMatch = false;
			//			if (!string.IsNullOrWhiteSpace(mediaLibraryOldReferenceRegexPattern))
			//			{
			//				// Custom Regex Matches
			//				var regexMatches = Regex.Matches(fieldStringValue, mediaLibraryOldReferenceRegexPattern, RegexOptions.IgnoreCase);
			//				foreach (Match regexMatch in regexMatches)
			//				{
			//					var value = regexMatch.Value.ToString();
			//					fieldMediaFileReferences.Add(value);
			//					didRegexMatch = true;
			//				}

			//			}
			//			if (!didRegexMatch)
			//			{
			//				// Get all src matches
			//				var srcMatches = Regex.Matches(fieldStringValue, "src=[\"'](.+?)[\"'].*?", RegexOptions.IgnoreCase);
			//				foreach (Match srcMatch in srcMatches)
			//				{
			//					var value = srcMatch.Value.ToString();
			//					if (value.Contains("."))
			//					{
			//						fieldMediaFileReferences.Add(value.Replace("src=\"", "").Trim('"'));
			//					}
			//				}
			//				var hrefMatches = Regex.Matches(fieldStringValue, "href=[\"'](.+?)[\"'].*?", RegexOptions.IgnoreCase);
			//				foreach (Match hrefMatch in hrefMatches)
			//				{
			//					var value = hrefMatch.Value.ToString();
			//					if (value.Contains("."))
			//					{
			//						fieldMediaFileReferences.Add(value.Replace("href=\"", "").Trim('"'));
			//					}
			//				}
			//			}


			//			// If the field value is a relative url referencing a file with an extension
			//			if (fieldStringValue.Trim().StartsWith("/") && fieldStringValue.Contains("."))
			//			{
			//				fieldMediaFileReferences.Add(fieldStringValue);
			//			}

			//			// Starts With Kentico's ~/ Reference
			//			if (fieldStringValue.StartsWith("~/") && fieldStringValue.Contains("."))
			//			{
			//				// Verify that it isn't the permanent url
			//				// If it isn't, convert to permanent url
			//				if (!fieldStringValue.StartsWith("~/getmedia/") && !fieldStringValue.StartsWith("~/getattachment/"))
			//				{
			//					fieldMediaFileReferences.Add(fieldStringValue);
			//				}
			//			}

			//			// Check if the Field Value is a Url
			//			try
			//			{
			//				Uri uri = new Uri(fieldStringValue);
			//				if (allSiteDomains.Contains(uri.Host.Trim('/')))
			//				{
			//					fieldMediaFileReferences.Add(fieldStringValue);
			//				}
			//			}
			//			catch (Exception)
			//			{
			//				// Reference is not a valid Url
			//			}

			//			if (fieldMediaFileReferences.Any())
			//			{
			//				bool updateNode = false;

			//				foreach (var filePathReference in fieldMediaFileReferences)
			//				{
			//					// DO REGEX MATCH EXTRACTION
			//					var matches = Regex.Match(filePathReference, $"{mediaLibraryIdRegexPattern}");
			//					if (matches.Success && matches.Groups.Count >= 2)
			//					{
			//						var guid = matches.Groups[1].Value;

			//						// match the old URL to the new url
			//						var mediaReference = allMediaReferences.Where(x => x.OldReference.Equals(guid, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
			//						if (mediaReference != null)
			//						{
			//							if (Guid.TryParse(mediaReference.NewReference, out Guid result))
			//							{
			//								var mediaFile = allMediaFiles.Where(x => x.FileGUID.Equals(result)).FirstOrDefault();
			//								if (mediaFile != null)
			//								{
			//									var permanentUrl = MediaLibraryHelper.GetPermanentUrl(mediaFile);
			//									if (!string.IsNullOrWhiteSpace(permanentUrl))
			//									{
			//										// SOMETIMES PERMA URL WILL HAVE THE FULL DOMAIN IF ITS NOT ON A SITE (E.G. CONSOLE APPS)
			//										try
			//										{
			//											Uri uri = new Uri(permanentUrl);
			//											if (allSiteDomains.Contains(uri.Host.Trim('/')))
			//											{
			//												permanentUrl = uri.AbsolutePath;
			//												if (permanentUrl.StartsWith("/getmedia/"))
			//												{
			//													permanentUrl = "~" + permanentUrl;
			//												}
			//											}

			//											// DO THE REPLACEMENT
			//											fieldStringValue = fieldStringValue.Replace(filePathReference, permanentUrl);
			//											updateNode = true;
			//										}
			//										catch (Exception e)
			//										{
			//											ErrorMessages.Add($"Issue Updating Node: {node.NodeID}, Error: {e.Message}");
			//										}
			//									}
			//								}
			//							}

			//						}
			//					}
			//				}

			//				if (updateNode)
			//				{
			//					try
			//					{
			//						node[field] = fieldStringValue;
			//						node.Update();
			//						updatedCount++;
			//					}
			//					catch (Exception e)
			//					{
			//						ErrorMessages.Add($"Issue Updating Node: {node.NodeID}, Error: {e.Message}");
			//					}
			//				}
			//			}
			//		}
			//	}
			//}

			//if (ErrorMessages != null && ErrorMessages.Any())
			//{
			//	ErrorMessages.Prepend($"Proccessed {currentCount}:{totalCount} - Updated {updatedCount}");
			//	Console.WriteLine($"There were {ErrorMessages.Count()} errors.");
			//	Console.WriteLine(ErrorMessages.Join("\n"));
			//}

		}
	}
}
