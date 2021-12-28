/*
 * Built with Common Launchpad 2.0.2
 */

using CMS.DocumentEngine.Types.Common;
using Launchpad.Core.Abstractions.Providers;
using Launchpad.Infrastructure.Kentico.Web.Attributes;

namespace Launchpad.Web.Models.Common.ViewModels
{
	[RegisterForPageType(ErrorPage.CLASS_NAME)]
	public class ErrorPageViewModel : BaseViewModel
	{
		#region KenticoProperties

		#endregion

		#region Properties	
		public string Headline { get; set; }
		public string Description { get; set; }
		public string BackgroundImage { get; set; }
		public override string ViewPath { get; protected set; } = $"Error/Error";
		#endregion

		public ErrorPageViewModel
		(
			ILayoutProvider layoutProvider
		)
			: base(layoutProvider)
		{

		}


	}
}