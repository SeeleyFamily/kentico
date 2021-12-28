/*
 * Built with Common Launchpad 2.0.2
 */

using Kentico.PageBuilder.Web.Mvc;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Constants;
using Launchpad.Core.Specifications;
using Launchpad.Infrastructure.Kentico.Web.Models.ViewModels;
using Launchpad.Web.Models.Custom.Widgets;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Launchpad.Web.Models.Common.Widgets
{
	public class ChildSummaryWidgetProperties : WidgetProperties, IWidgetProperties
	{
		#region Properties				
		public IEnumerable<CardViewModel> Cards { get; set; } = Enumerable.Empty<CardViewModel>();
		#endregion

		#region fields
		private readonly IChildSummaryService childSummaryService;
		#endregion

		public ChildSummaryWidgetProperties()
		{
			childSummaryService = DependencyResolver.Current.GetService<IChildSummaryService>();
		}


		public void PopulateChildSummary(int nodeId)
		{
			var childSummarySpecification = new ChildSummarySpecification()
			{
				NodeLevels = 1,
				PageSize = int.MaxValue,
			};
			var childSummaryItems = childSummaryService.GetChildSummaryItems(nodeId, childSummarySpecification);
			Cards = childSummaryItems.Select(x =>
			{
				var cardViewModel = new CardViewModel()
				{
					Title = x.Title,
					Description = x.Summary,
					CtaUrl = x.Url,
					CtaText = LabelConstants.ReadMore,
					Image = x.Image,
				};
				return cardViewModel;
			});
		}
	}
}