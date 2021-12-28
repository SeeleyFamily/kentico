using CMS.DataEngine;
using CMS.DocumentEngine;
using CMS.Membership;
using CMS.SiteProvider;
using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Configuration;
using Launchpad.Infrastructure.Services;
using System;

namespace Launchpad.Infrastructure.Kentico.CMS.Services
{

	public class DocumentCategoryModuleService
	{
		#region Fields		
		private readonly CustomCmsModuleLoggingService customModuleLoggingService;
		#endregion

		public DocumentCategoryModuleService()
		{
			this.customModuleLoggingService = new CustomCmsModuleLoggingService();
		}

		private ICategoryService _CategoryService
		{
			get
			{				
				var siteContextConfiguration = new SiteContextConfiguration()
				{
					SiteId = SiteContext.CurrentSiteID,
					SiteName = SiteContext.CurrentSiteName
				};

				var cacheConfiguration = new Lazy<ICacheConfiguration>(() => new CacheConfiguration()
				{
					IsCached = false
				});				
				var cacheService = new CacheService(cacheConfiguration);

				return new CategoryService(cacheService, siteContextConfiguration);
			}
		}


		internal void TriggerPageUpdate(object sender, ObjectEventArgs e)
		{
			customModuleLoggingService.LogInformation("DocumentCategoryModuleService", "TriggerPageUpdate");

			TreeProvider tree = new TreeProvider(MembershipContext.AuthenticatedUser);
			var documentIdString = e.Object.GetValue(nameof(DocumentCategoryInfo.DocumentID)).ToString();
			if (!string.IsNullOrWhiteSpace(documentIdString) && int.TryParse(documentIdString, out int documentId))
			{
				var document = tree.SelectSingleDocument(documentId);
				document.Update(true);
			}
		}	


	}

}
