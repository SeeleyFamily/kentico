using Launchpad.Core.Extensions;
using System;
using System.Web.Mvc;


namespace Launchpad.Infrastructure.Kentico.Web.HtmlHelpers
{

	public static class ResponsiveImageHelper
	{
		#region Fields & Constants
		private const int DesktopBreakpoint = 750;
		#endregion


		public static MvcHtmlString ResponsiveImageCss(this HtmlHelper helper, string desktopImage, string mobileImage, string id, string prefix = null)
		{
			if (string.IsNullOrWhiteSpace(desktopImage))
			{
				return new MvcHtmlString("");
			}

			prefix = prefix ?? "responsive-image";

			TagBuilder tag = new TagBuilder("style");


			// Output the CSS 
			tag.InnerHtml = String.Format(
				@".{0}_{1} {{ background-image: url( '{2}' ); }}
				@media screen and (min-width: {4}px), (orientation: landscape) {{
					.{0}_{1} {{ background-image: url( '{3}' ); }}
				}}",

				prefix,
				id,
				string.IsNullOrEmpty(mobileImage) ? desktopImage.SanitizeMediaUrl() : mobileImage.SanitizeMediaUrl(),
				desktopImage.SanitizeMediaUrl(),
				DesktopBreakpoint
			);


			return new MvcHtmlString(tag.ToString());
		}

	}

}