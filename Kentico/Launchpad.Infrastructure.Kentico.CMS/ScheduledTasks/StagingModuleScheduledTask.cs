using CMS.Scheduler;
using Launchpad.Infrastructure.Kentico.CMS.Services;
using System;

namespace Launchpad.Infrastructure.Kentico.CMS.ScheduledTasks
{
    public class StagingModuleScheduledTask : ITask
    {
        #region Fields
        private readonly StagingModuleService stagingModuleService;
        #endregion

        public StagingModuleScheduledTask()
        {
            stagingModuleService = new StagingModuleService();
        }

        public string Execute(TaskInfo ti)
        {
            try
            {
                stagingModuleService.SyncTasks();
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
