using CMS.SiteProvider;
using Launchpad.Core.Models;


namespace Launchpad.Infrastructure.Extensions
{

	public static class SiteInfoExtensions
	{

		public static Site ToSite( this SiteInfo siteInfo )
		{
			if( siteInfo == null )
			{
				return null;
			}


			return new Site
			{
				CodeName = siteInfo.SiteName,
				Name = siteInfo.DisplayName,
				PresentationUrl = siteInfo.SitePresentationURL,
				SiteGuid = siteInfo.SiteGUID,
				SiteID = siteInfo.SiteID
			};
		}

	}


}
