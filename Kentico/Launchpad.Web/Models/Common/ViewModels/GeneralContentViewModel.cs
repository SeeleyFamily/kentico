/*
 * Built with Common Launchpad 2.0.2
 */

using CMS.DocumentEngine.Types.Common;
using Launchpad.Core.Abstractions.Providers;
using Launchpad.Infrastructure.Kentico.Web.Attributes;
using Launchpad.Infrastructure.Kentico.Web.Models.ViewModels;
using System.Linq;
using MenuItem = Launchpad.Core.Models.MenuItem;

namespace Launchpad.Web.Models.Common.ViewModels
{
	[EnablePageBuilder, RegisterForPageType(GeneralContent.CLASS_NAME)]
	public class GeneralContentViewModel : BaseViewModel
	{
		#region KenticoProperties
		public string Headline { get; protected set; }
		public string Content { get; protected set; }
		public bool ShowSidebarNavigation { get; protected set; }
		#endregion


		#region Properties				
		public override string ViewPath { get; protected set; } = $"GeneralContent/Index";
		public HeroViewModel HeroViewModel { get; protected set; }
		public WysiwygViewModel WysiwygViewModel { get; protected set; }
		public PageBuilderViewModel PageBuilderViewModel { get; protected set; }
		public PageBuilderViewModel AbovePageBuilderViewModel { get; protected set; }
		public PageBuilderViewModel BelowPageBuilderViewModel { get; protected set; }
		public MenuItem SideNavMenu { get; protected set; }
		#endregion


		#region fields
		#endregion


		public GeneralContentViewModel
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
			PopulateSideNavMenu();
		}

		protected virtual void PopulateHero()
		{
			HeroViewModel = new HeroViewModel()
			{
				Headline = Headline,
				Breadcrumbs = Breadcrumbs,
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
			AbovePageBuilderViewModel = new PageBuilderViewModel(Node.PageBuilderWidgets) { AreaIdentifier = "above_pagecontent" };
			BelowPageBuilderViewModel = new PageBuilderViewModel(Node.PageBuilderWidgets) { AreaIdentifier = "below_pagecontent" };
		}

		protected virtual void PopulateSideNavMenu()
		{
			if (ShowSidebarNavigation)
			{
				SideNavMenu = LayoutProvider.GetMenuService().GetSideNavMenu(Node, 2, rootMenuClassName: CategoryOverview.CLASS_NAME).FirstOrDefault();
				if (!SideNavMenu.HasChildren)
				{
					ShowSidebarNavigation = false;
				}
			}
		}
	}
}