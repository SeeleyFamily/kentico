using CMS.SiteProvider;
using Launchpad.Core.Abstractions.Providers;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Models;
using Launchpad.Infrastructure.Extensions;
using System;
using System.Configuration;

namespace Launchpad.Infrastructure.Kentico.Web.Providers
{

	/// <summary>
	/// Provides the current <see cref="Site"/> from session.
	/// </summary>
	public class CurrentSiteProvider : ICurrentSiteProvider
	{		
		#region Fields
		private readonly Lazy<ISiteService> siteService;
		#endregion


		#region Properties
		protected Site Site { get; set; }
		#endregion

		public CurrentSiteProvider
		(
			Lazy<ISiteService> siteService
		)
		{
			this.siteService = siteService;

		}

		/// <summary>
		/// Retrieves the current <see cref="Site"/>
		/// </summary>
		public virtual Site GetCurrentSite()
		{
			if (Site == null)
			{
				Site = GetSiteInternal();
			}

			return Site;
		}

		/// <summary>
		/// Determines an appropriate <see cref="Site"/> for the current context and request.
		/// </summary>
		protected virtual Site GetSiteInternal()
		{			
			try
			{
				return SiteContext.CurrentSite.ToSite();
			}
			catch (Exception)
			{
				_ = int.TryParse(ConfigurationManager.AppSettings["SiteId"], out var siteId) ? siteId : 0;
				return siteService.Value.GetSite(siteId);
			}
		}
	}

}