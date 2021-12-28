/*
 * Built with Common Launchpad 2.0.2
 */

using CMS.DocumentEngine.Types.Common;
using Launchpad.Core.Abstractions.Providers;
using Launchpad.Infrastructure.Kentico.Web.Attributes;

namespace Launchpad.Web.Models.Common.ViewModels
{

	[RegisterForPageType(Blank.CLASS_NAME)]
	public class BlankViewModel : BaseViewModel
	{

		#region Kentico Properties		
		public string Head { get; set; }
		public string Body { get; set; }
		#endregion


		#region Properties		
		public override string ViewPath { get; protected set; } = "Blank/Index";
		#endregion


		public BlankViewModel
		(
			ILayoutProvider layoutProvider
		)
			: base(layoutProvider)
		{

		}

	}

}