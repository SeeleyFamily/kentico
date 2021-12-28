/*
 * Built with Common Launchpad 2.0.2
 */

using CMS.DocumentEngine.Types.Common;
using Launchpad.Core.Abstractions.Providers;
using Launchpad.Infrastructure.Kentico.Web.Attributes;

namespace Launchpad.Web.Models.Common.ViewModels
{

	[RegisterForPageType(Home.CLASS_NAME)]
	public class HomeViewModel : BaseViewModel
	{
		#region Properties				
		public override string ViewPath { get; protected set; } = $"Home/Index";
		#endregion

		#region fields
		#endregion


		public HomeViewModel
		(
			ILayoutProvider layoutProvider
		)
			: base(layoutProvider)
		{
		}



		protected override void Populate()
		{
			base.Populate();
		}
	}

}