using CMS.Scheduler;
using Launchpad.Infrastructure.Kentico.CMS.Services;
using System;

namespace Launchpad.Infrastructure.Kentico.CMS.ScheduledTasks
{

    public class PublishScheduleModuleScheduledTask : ITask
	{
		#region Fields
		private readonly PublishScheduleModuleService publishScheduleModuleService;
        #endregion

        public PublishScheduleModuleScheduledTask()
        {
            publishScheduleModuleService = new PublishScheduleModuleService();
        }

        public string Execute(TaskInfo ti)
        {
            try
            {
                publishScheduleModuleService.CheckPublishedSchudule();
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
