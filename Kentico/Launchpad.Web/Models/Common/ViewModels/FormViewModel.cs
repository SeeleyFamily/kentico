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
	[EnablePageBuilder, RegisterForPageType(Form.CLASS_NAME)]

	public class FormViewModel : BaseViewModel
	{
		#region KenticoProperties
		public string Headline { get; protected set; }
		public string Description { get; protected set; }
		public string HeroBackgroundImage { get; protected set; }
		public string HeroBackgroundImageMobile { get; protected set; }
		public string Content { get; protected set; }
		#endregion


		#region Properties				
		public override string ViewPath { get; protected set; } = $"Form/Index";
		public HeroViewModel HeroViewModel { get; protected set; }
		public WysiwygViewModel WysiwygViewModel { get; protected set; }
		public PageBuilderViewModel PageBuilderViewModel { get; protected set; }
		public PageBuilderViewModel AboveFormPageBuilderViewModel { get; protected set; }
		public PageBuilderViewModel FormPageBuilderViewModel { get; protected set; }
		#endregion


		#region fields
		#endregion


		public FormViewModel
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

		protected virtual void PopulatePageBuilder()
		{
			PageBuilderViewModel = new PageBuilderViewModel(Node.PageBuilderWidgets);
			AboveFormPageBuilderViewModel = new PageBuilderViewModel(Node.PageBuilderWidgets)
			{
				AreaIdentifier = "AboveFormArea",
			};
			FormPageBuilderViewModel = new PageBuilderViewModel(Node.PageBuilderWidgets)
			{
				AreaIdentifier = "FormArea",
				AllowedWidgets = new string[] { SystemComponentIdentifiers.FORM_WIDGET_IDENTIFIER },
			};
		}
	}
}