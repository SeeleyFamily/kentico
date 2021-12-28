using CMS.DocumentEngine.Types.Common;
using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Core.Abstractions.Services;
using Launchpad.Core.Models;
using Launchpad.Core.Models.Summary;

namespace Launchpad.Infrastructure.Services
{
	public class CalloutCardService : GlobalContentDocumentService<CalloutCard, CalloutCardSummaryItem>, ICalloutCardService, IPerScopeService
	{
		public CalloutCardService(IGlobalContentDocumentService<CalloutCard> documentService) : base(documentService)
		{
		}

		protected override CalloutCardSummaryItem Convert(CalloutCard node)
		{
			var model = new CalloutCardSummaryItem()
			{
				Title = node.Headline,
				Summary = node.Description,
				Image = node.Image,
				Cta = new Cta()
				{
					Text = node.CtaLabel,
					Url = node.CtaUrl
				},
				Url = node.CtaUrl
			};

			return model;
		}
	}
}
