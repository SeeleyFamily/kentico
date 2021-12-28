using CMS.Module.Redirects;
using Common.Migration.Infrastructure;
using Launchpad.Core.Extensions;
using System;
using System.Collections.Generic;

namespace Common.Migration.Import.Redirects
{
	public class ImportRedirectsProgram : BaseProgram
	{
		#region Properties
		IEnumerable<ImportRedirect> ImportRedirects { get; set; }
		#endregion

		static void Main(string[] args)
		{
			var consoleApp = new ImportRedirectsProgram();
			consoleApp.Main();
		}

		public ImportRedirectsProgram() : base()
		{
		}

		public override void RunConsoleApp()
		{
			try
			{
				// Console App Business Logic Here;
				PopulateImportRedirects();
				Import();

			}
			catch (Exception e)
			{
				Messages.Add($"Error: {e.Message}");
			}
		}

		private void PopulateImportRedirects()
		{
			ImportRedirects = new List<ImportRedirect>()
			{
				//new ImportRedirect(){SiteId = 1, MatchUrl="", RedirectUrl = "" ,  ImportRedirectType = ImportRedirectType.Permanent },
				//new ImportRedirect(){SiteId = 1, MatchUrl="", RedirectUrl = "" ,  ImportRedirectType = ImportRedirectType.Temporary },
				//new ImportRedirect(){SiteId = 1, MatchUrl="", RedirectUrl = "" ,  ImportRedirectType = ImportRedirectType.Regex, RegexReplace = true },
			};
		}

		private void Import()
		{
			if (ImportRedirects.IsNullOrEmpty())
			{
				return;
			}

			foreach (var importRedirect in ImportRedirects)
			{
				try
				{
					switch (importRedirect.ImportRedirectType)
					{
						case ImportRedirectType.Permanent:
							var newPermanentRedirect = new PermanentRedirectsInfo()
							{
								MatchUrl = importRedirect.MatchUrl.TrimStart('/'),
								RedirectUrl = importRedirect.RedirectUrl,
								SiteID = importRedirect.SiteId
							};
							PermanentRedirectsInfoProvider.SetPermanentRedirectsInfo(newPermanentRedirect);
							break;
						case ImportRedirectType.Temporary:
							var newTemporaryRedirect = new TemporaryRedirectsInfo()
							{
								MatchUrl = importRedirect.MatchUrl.TrimStart('/'),
								RedirectUrl = importRedirect.RedirectUrl,
								SiteID = importRedirect.SiteId
							};
							TemporaryRedirectsInfoProvider.SetTemporaryRedirectsInfo(newTemporaryRedirect);
							break;
						case ImportRedirectType.Regex:
							var newRegexRedirect = new RegexRedirectsInfo()
							{
								MatchUrl = importRedirect.MatchUrl.TrimStart('/'),
								RedirectUrl = importRedirect.RedirectUrl,
								RegexReplace = importRedirect.RegexReplace,
								SiteID = importRedirect.SiteId
							};
							RegexRedirectsInfoProvider.SetRegexRedirectsInfo(newRegexRedirect);
							break;
						default:
							break;
					}
				}
				catch (Exception e)
				{
					Messages.Add($"Error: {importRedirect.SiteId} : {importRedirect.MatchUrl} : {importRedirect.RedirectUrl} : Error Importing Redirect : {e.Message}");
				}
			}
		}
	}
}
