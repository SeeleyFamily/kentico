using CMS.Scheduler;
using Launchpad.Infrastructure.Kentico.CMS.Services;
using System;

namespace Launchpad.Infrastructure.Kentico.CMS.ScheduledTasks
{
	public class ClearRedirectsCacheScheduledTask : ITask
    {
		#region Fields
		private readonly RedirectsModuleService redirectsModuleService;
		#endregion

		public ClearRedirectsCacheScheduledTask()
        {
            redirectsModuleService = new RedirectsModuleService();
        }    

        public string Execute(TaskInfo ti)
        {            
            try
            {
                redirectsModuleService.ClearCache();
                redirectsModuleService.GetRedirects();
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
