using CMS.Base;
using CMS.Core;
using Launchpad.Infrastructure.Kentico.ImageOptimization.Services;
using System;

namespace Launchpad.Infrastructure.Kentico.ImageOptimization.WebFarmTasks
{
	public class ClearAzureCacheWebFarmTask : WebFarmTaskBase
	{
		#region field
		private readonly AzureCacheCleanerService cacheCleanerService;
		#endregion
		public ClearAzureCacheWebFarmTask()
		{
			cacheCleanerService = new AzureCacheCleanerService();
		}

		// Contains the name of the web farm server that created the task
		public string CreatorName { get; set; }


		// Holds arbitrary data for processing on target servers
		public object Data { get; set; }


		// Contains custom logic to determine whether a new instance of this task should be created whenever WebFarmHelper.CreateTask is invoked
		public override bool ConditionMethod()
		{
			// TODO: Include custom logic that determines whether the task should be created

			return true;
		}


		// Contains logic that is executed when a target server processes the web farm task
		public override void ExecuteTask()
		{
			// Logs a record into the system's event log, with 'Execute' as the event code
			string message = $"Server {SystemContext.ServerName} is processing a task from creator {CreatorName}";
			Service.Resolve<IEventLogService>().LogInformation("CustomTask", "Execute", message);

			// TODO: Include custom logic to be processed by target servers here
			if (Guid.TryParse(Data.ToString(), out var mediaFileGuid))
			{
				cacheCleanerService.ClearCacheFile(mediaFileGuid);
			}
		}
	}
}
