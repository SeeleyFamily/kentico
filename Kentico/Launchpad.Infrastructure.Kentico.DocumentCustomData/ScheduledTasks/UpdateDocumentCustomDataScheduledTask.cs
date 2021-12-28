using CMS.Scheduler;
using Launchpad.Infrastructure.Kentico.DocumentCustomData.Services;
using System;

namespace Launchpad.Infrastructure.Kentico.DocumentCustomData.ScheduledTasks
{
	public class UpdateDocumentCustomDataScheduledTask : ITask
	{
		#region Fields
		private readonly DocumentCustomDataModuleService documentCustomDataModuleService;
		#endregion

		public UpdateDocumentCustomDataScheduledTask()
		{
			documentCustomDataModuleService = new DocumentCustomDataModuleService();
		}

		public string Execute(TaskInfo ti)
		{
			try
			{
				documentCustomDataModuleService.UpdateDocumentCustomData();
			}
			catch (Exception e)
			{
				// Return an error message string with details in cases where the execution fails
				return e.Message;
			}
			// Returns a null value to indicate that the task executed successfully            
			return null;
		}
	}
}
