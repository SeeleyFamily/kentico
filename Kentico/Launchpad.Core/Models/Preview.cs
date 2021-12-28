using System;

namespace Launchpad.Core.Models
{
	public class Preview
	{
		public string PreviewTitle { get; set; }
		public DateTime? PreviewDate { get; set; }
		public string PreviewImage { get; set; }
		public string PreviewUrl { get; set; }
		public string PreviewText { get; set; }
		public string PreviewCtaLabel { get; set; }
		public bool HidePreviewText { get; set; }
		public string PreviewNavigationLabel { get; set; }

	}
}
