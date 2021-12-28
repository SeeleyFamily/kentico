using System.Collections.Generic;
using Launchpad.Core.Models;


namespace Launchpad.Core.Abstractions.Services
{

	public interface IBannerService
	{
		Banner GetCookieBanner( );

		IEnumerable<Banner> GetGlobalNotificationBanners( );


		/// <summary>
		/// Returns notification banners for a given page. Includes global notifications.
		/// </summary>
		IEnumerable<Banner> GetNotificationBanners( string path );
	}

}
