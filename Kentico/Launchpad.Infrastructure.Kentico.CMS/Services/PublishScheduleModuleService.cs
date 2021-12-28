using CMS.Core;
using CMS.DataEngine;
using CMS.DocumentEngine;
using Launchpad.Infrastructure.Services;
using System;

namespace Launchpad.Infrastructure.Kentico.CMS.Services
{
	class PublishScheduleModuleService
	{
		#region Fields		
		private readonly CustomCmsModuleLoggingService customCmsModuleLoggingService;
		#endregion

		public PublishScheduleModuleService()
		{
			this.customCmsModuleLoggingService = new CustomCmsModuleLoggingService();
		}


		internal void PublishAfterEventHandler(object sender, WorkflowEventArgs e)
		{
			customCmsModuleLoggingService.LogInformation("PublishScheduleModuleService", "PublishAfterEventHandler");


			// see https://www.bluemodus.com/articles/kentico-tip-how-to-manage-scheduled-publishing-of-cached-content
			var publishedPage = e.PublishedDocument;
			ClearPublishScheduleNode(publishedPage);
		}

		internal void CheckPublishedSchudule()
		{
			var publishFromWhereCondition = new WhereCondition(new WhereCondition[]
				 {
						new WhereCondition().WhereNotNull(nameof(TreeNode.DocumentPublishFrom)),
						new WhereCondition().WhereLessOrEquals(nameof(TreeNode.DocumentPublishFrom),DateTime.Now.AddMinutes(5)),
						new WhereCondition().WhereGreaterOrEquals(nameof(TreeNode.DocumentPublishFrom),DateTime.Now.AddMinutes(-5)),
				 });
			var publishToWhereCondition = new WhereCondition(new WhereCondition[]
				 {
						new WhereCondition().WhereNotNull(nameof(TreeNode.DocumentPublishTo)),
						new WhereCondition().WhereGreaterOrEquals(nameof(TreeNode.DocumentPublishTo),DateTime.Now.AddMinutes(-5)),
						new WhereCondition().WhereLessOrEquals(nameof(TreeNode.DocumentPublishTo),DateTime.Now.AddMinutes(5)),
				 });
			var scheduleWhereCondition = publishFromWhereCondition.Or(publishToWhereCondition);

			var publishScheduleNodes = DocumentHelper.GetDocuments()
				.Published(false)
					 .LatestVersion()
				.Where(scheduleWhereCondition);

			foreach (var publishedPage in publishScheduleNodes)
			{
				ClearPublishScheduleNode(publishedPage);
			}
		}

		internal void ClearPublishScheduleNode(TreeNode publishedPage)
		{
			if (publishedPage != null)
			{
				var publishedFromDate = publishedPage.DocumentPublishFrom;
				var publishedToDate = publishedPage.DocumentPublishTo;
				var now = DateTime.Now;

				if ((publishedFromDate != DateTime.MinValue) && (now >= publishedFromDate) && (now < publishedToDate))
				{
					if (publishedPage.IsPublished)
					{
						// Once the content is published
						publishedPage.Update(); // We have to update to trigger DocumentCustomData Updates
					}
					else
					{
						publishedPage.ClearCache();
						publishedPage.TouchKeys();
					}
					var successMessage = $"A page has been published via scheduling. Cache cleared.\r\n{publishedPage.NodeAliasPath}";
					Service.Resolve<IEventLogService>().LogInformation("PublishScheduleModuleService", "PublishedPageDetected", eventDescription: successMessage);
				}
			}
		}
	}
}
