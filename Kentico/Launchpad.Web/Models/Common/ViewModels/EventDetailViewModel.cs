/*
 * Built with Common Launchpad 2.0.2
 */

using CMS.DocumentEngine.Types.Common;
using Launchpad.Core.Abstractions.Providers;
using Launchpad.Infrastructure.Kentico.Web.Attributes;
using Launchpad.Infrastructure.Kentico.Web.Models.ViewModels;
using System;

namespace Launchpad.Web.Models.Common.ViewModels
{

	[RegisterForPageType(EventDetail.CLASS_NAME)]
	public class EventDetailViewModel : BaseViewModel
	{


		#region KenticoProperties
		public string Title { get; protected set; }
		public string Url { get; protected set; }
		public bool IsExternal { get; protected set; }
		public DateTime? StartDate { get; protected set; }
		public DateTime? EndDate { get; protected set; }
		public string Address1 { get; protected set; }
		public string Address2 { get; protected set; }
		public string City { get; protected set; }
		public string State { get; protected set; }
		public string Zipcode { get; protected set; }
		public string Latitude { get; protected set; }
		public string Longitude { get; protected set; }
		public string PhoneNumber { get; protected set; }
		public string Content { get; protected set; }
		#endregion


		#region Properties				
		public override string ViewPath { get; protected set; } = $"EventDetail/Index";
		public WysiwygViewModel WysiwygViewModel { get; protected set; }
		public PageBuilderViewModel PageBuilderViewModel { get; protected set; }
		#endregion


		#region fields
		#endregion


		public EventDetailViewModel
		(
			ILayoutProvider layoutProvider
		)
			: base(layoutProvider)
		{

		}


		protected override void Populate()
		{
			base.Populate();

			PopulateWysiywg();
			PopulatePageBuilder();
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