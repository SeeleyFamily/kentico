/*
 * Built with Common Launchpad 2.0.2
 */

using Launchpad.Core.Abstractions.Providers;


namespace Launchpad.Web.Models.Common.ViewModels
{

	/// <remarks>
	/// This class is mostly a partial placeholder for the customized version of it.
	/// Most common, core changes should be placed in <see cref="AbstractBaseViewModel"/>
	/// instead. Place custom changes in the Custom/ViewModels/BaseViewModel.cs partial file.
	/// </remarks>
	public partial class BaseViewModel : AbstractBaseViewModel
	{

		public BaseViewModel
		(
			ILayoutProvider layoutProvider
		)
			: base(layoutProvider)
		{

		}

	}

}