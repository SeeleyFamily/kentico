/*
 * Built with Common Launchpad 2.0.2
 */

using Kentico.Forms.Web.Mvc;
using Launchpad.Web.Models.Common.FormComponents;

[assembly: RegisterFormComponent(VideoInputComponent.IDENTIFIER, typeof(VideoInputComponent), "Video URL Input", Description = "Allows users add a video URL with preview", IconClass = "icon-clapperboard")]
namespace Launchpad.Web.Models.Common.FormComponents
{
	public class VideoInputComponent : FormComponent<VideoInputComponentProperties, string>
	{
		public const string IDENTIFIER = "VideoInputComponent";

		[BindableProperty]
		public string Value { get; set; } = "";

		public override bool CustomAutopostHandling => true;


		public override string GetValue()
		{
			return Value;
		}
		public override void SetValue(string value)
		{
			Value = value;
		}
	}
}