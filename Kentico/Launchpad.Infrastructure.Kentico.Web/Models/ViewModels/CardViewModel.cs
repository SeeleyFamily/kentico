namespace Launchpad.Infrastructure.Kentico.Web.Models.ViewModels
{
	public class CardViewModel
	{		
		public string Image { get; set; }
		public string ImageAltText { get; set; }
		public string CtaUrl { get; set; }
		public string CtaText { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }

		// Widget View Model Properties
		public bool PageBuilderEnabled { get; set; }
		public string TitleTargetId { get; set; }
		public string WidgetClass { get; set; }
	}
}
