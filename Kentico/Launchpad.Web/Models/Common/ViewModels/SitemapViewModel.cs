/*
 * Built with Common Launchpad 2.0.2
 */

using CMS.DocumentEngine.Types.Common;
using Launchpad.Core.Abstractions.Providers;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Infrastructure.Kentico.Web.Attributes;
using System;
using System.Collections.Generic;


namespace Launchpad.Web.Models.Common.ViewModels
{
	[RegisterForPageType(Sitemap.CLASS_NAME)]
	public class SitemapViewModel : BaseViewModel
	{
		#region Properties	
		public string Headline { get; set; }
		public Guid SitemapMenu { get; set; }
		public override string ViewPath { get; protected set; } = $"Sitemap/Index";
		public IEnumerable<Core.Models.MenuItem> Menu { get; private set; }
		#endregion

		#region fields
		private readonly IMenuService menuService;
		#endregion


		public SitemapViewModel
		(
			ILayoutProvider layoutProvider,
			IMenuService menuService
		)
			: base(layoutProvider)
		{
			this.menuService = menuService;
		}

		protected override void Populate()
		{
			base.Populate();
			PopulateSitemapMenu();
		}

		protected virtual void PopulateSitemapMenu()
		{
			Menu = menuService.GetMenu(SitemapMenu);
		}
	}

}