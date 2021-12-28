/*
 * Built with Common Launchpad 2.0.2
 */

using CMS.DocumentEngine.Types.Common;
using Kentico.Content.Web.Mvc;
using Launchpad.Core.Abstractions.Providers;
using Launchpad.Infrastructure.Kentico.Web.Attributes;
using Launchpad.Infrastructure.Kentico.Web.Models.ViewModels;


namespace Launchpad.Web.Models.Common.ViewModels
{
	[EnablePageBuilder, RegisterForPageType(Contact.CLASS_NAME)]

	public class ContactViewModel : BaseViewModel
	{
		#region KenticoProperties
		public string Headline { get; protected set; }
		public string Description { get; protected set; }
		public string HeroBackgroundImage { get; protected set; }
		public string HeroBackgroundImageMobile { get; protected set; }
		public string Content { get; protected set; }
		#endregion


		#region Properties				
		public override string ViewPath { get; protected set; } = $"Contact/Index";
		public HeroViewModel HeroViewModel { get; protected set; }
		public WysiwygViewModel WysiwygViewModel { get; protected set; }
		public PageBuilderViewModel PageBuilderViewModel { get; protected set; }
		public PageBuilderViewModel AboveFormPageBuilderViewModel { get; protected set; }
		public PageBuilderViewModel ContactFormPageBuilderViewModel { get; protected set; }
		#endregion


		#region fields
		#endregion


		public ContactViewModel
		(

			ILayoutProvider layoutProvider
		)
			: base(layoutProvider)
		{

		}



		protected override void Populate()
		{
			base.Populate();
			PopulateHero();
			PopulateWysiywg();
			PopulatePageBuilder();
		}

		protected virtual void PopulateHero()
		{
			HeroViewModel = new HeroViewModel()
			{
				Headline = Headline,
				Description = Description,
				Breadcrumbs = Breadcrumbs,
				Image = HeroBackgroundImage,
				ImageMobile = HeroBackgroundImageMobile,
			};
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
			AboveFormPageBuilderViewModel = new PageBuilderViewModel(Node.PageBuilderWidgets)
			{
				AreaIdentifier = "AboveFormArea",
			};
			ContactFormPageBuilderViewModel = new PageBuilderViewModel(Node.PageBuilderWidgets)
			{
				AreaIdentifier = "ContactUsFormArea",
				AllowedWidgets = new string[] { SystemComponentIdentifiers.FORM_WIDGET_IDENTIFIER },
			};
		}
	}
}