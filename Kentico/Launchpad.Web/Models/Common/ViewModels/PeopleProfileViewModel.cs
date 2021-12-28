/*
 * Built with Common Launchpad 2.0.2
 */

using CMS.DocumentEngine.Types.Common;
using Launchpad.Core.Abstractions.Providers;
using Launchpad.Infrastructure.Kentico.Web.Attributes;
using Launchpad.Infrastructure.Kentico.Web.Models.ViewModels;

namespace Launchpad.Web.Models.Common.ViewModels
{

	[RegisterForPageType(PeopleProfile.CLASS_NAME)]
	public class PeopleProfileViewModel : BaseViewModel
	{


		#region KenticoProperties
		public string LastName { get; protected set; }
		public string FirstName { get; protected set; }
		public string Photo { get; protected set; }
		public string PhotoAltText { get; protected set; }
		public string Title { get; protected set; }
		public string FacebookLink { get; protected set; }
		public string TwitterLink { get; protected set; }
		public string LinkedInLink { get; protected set; }
		public string InstagramLink { get; protected set; }
		public string Content { get; protected set; }
		#endregion


		#region Properties				
		public override string ViewPath { get; protected set; } = $"PeopleProfile/Index";
		public WysiwygViewModel WysiwygViewModel { get; protected set; }
		public PageBuilderViewModel PageBuilderViewModel { get; protected set; }
		public string FullName { get; protected set; }
		#endregion


		#region fields
		#endregion


		public PeopleProfileViewModel
		(
			ILayoutProvider layoutProvider
		)
			: base(layoutProvider)
		{

		}


		protected override void Populate()
		{
			base.Populate();

			PopulateFullName();
			PopulateWysiywg();
			PopulatePageBuilder();
		}


		protected virtual void PopulateFullName()
		{
			FullName = $"{FirstName} {LastName}".Trim();
		}


		protected virtual void PopulateWysiywg()
		{
			WysiwygViewModel = new WysiwygViewModel()
			{
				Content = Content
			};
		}


		protected virtual void PopulatePageBuilder()
		{
			PageBuilderViewModel = new PageBuilderViewModel(Node.PageBuilderWidgets);
		}


	}

}