using CMS.MediaLibrary;
using CMS.SiteProvider;
using Launchpad.Core.Abstractions.Services;
using System;
using System.Web.Mvc;

namespace Launchpad.Infrastructure.Kentico.Web.Controllers
{
	public class AdminController : Controller
	{
		#region Fields
		private readonly IDocumentService documentService;
		#endregion

		public AdminController(
				IDocumentService documentService
			)
		{
			this.documentService = documentService;
		}

		public ActionResult Edit(string relativePath)
		{
			if (relativePath != null)
			{
				// Check Document
				var documentUrlPath = relativePath;
				if (!documentUrlPath.StartsWith("/"))
				{
					documentUrlPath = $"/{documentUrlPath}";
				}
				var currentSite = SiteContext.CurrentSite;
				var document = documentService.Get(documentUrlPath, true);
				if (document != null)
				{
					var documentEditUrl = $"https://{currentSite.DomainName}/Admin/CMSAdministration.aspx/default.aspx?action=edit&nodeid={document.NodeID}&culture=en-US#95a82f36-9c40-45f0-86f1-39aa44db9a77";

					return RedirectPermanent(documentEditUrl);
				}
			}
			return RedirectPermanent("/");
		}

		public ActionResult GetDirectPath(string guidString)
		{
			if (Guid.TryParse(guidString, out Guid guid))
			{
				var mediaFile = MediaFileInfo.Provider.Get(guid, SiteContext.CurrentSiteID);
				if (mediaFile != null)
				{
					return RedirectPermanent(MediaLibraryHelper.GetDirectUrl(mediaFile));
				}
			}
			return RedirectPermanent("/");
		}


	}
}
