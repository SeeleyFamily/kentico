using System.Collections.Generic;
using Launchpad.Core.Enums;
using Launchpad.Core.Models;


namespace Launchpad.Infrastructure.Kentico.Web.Models.ViewModels
{

	public class HeroViewModel : SectionViewModel
	{
		public BackgroundType BackgroundType { get; set; }
		public Breadcrumbs Breadcrumbs { get; set; }
		public string Image { get; set; }
		public string ImageMobile { get; set; }
		public string ImageAltText { get; set; }
		public string Headline { get; set; }
		public string Description { get; set; }
		public string Date { get; set; }
		public IEnumerable<string> Tags { get; set; }		

		// Widget View Model Properties
		public bool PageBuilderEnabled { get; set; }
		public string TitleTargetId { get; set; }
	}

}