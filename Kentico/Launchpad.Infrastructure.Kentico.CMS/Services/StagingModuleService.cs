using CMS.Core;
using CMS.MediaLibrary;
using CMS.Search;
using CMS.Synchronization;
using Launchpad.Core.Extensions;
using Launchpad.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Launchpad.Infrastructure.Kentico.CMS.Services
{
	public class StagingModuleService
	{
		#region Properties
		public string AppEnvironment { get; set; }
		public bool StagingModuleEnabled { get; set; }
		public bool StagingModuleStagingProdSyncEnabled { get; set; }
		public string ProdEnvironment { get; set; } = "PROD";
		public List<string> LowerEnvironments { get; set; } = new List<string>()
		{
			"LOCAL",
			"DEV",
			"QA",
			"TEST"
		};
		public List<string> AutomaticSyncEnvironments { get; set; } = new List<string>()
		{
		};
		#endregion

		#region Fields		
		private readonly CustomCmsModuleLoggingService customCmsModuleLoggingService;
		#endregion

		public StagingModuleService()
		{
			AppEnvironment = ConfigurationManager.AppSettings["AppEnvironment"];
			StagingModuleEnabled = ConfigurationManager.AppSettings.GetBoolValue("StagingModuleEnabled");
			StagingModuleStagingProdSyncEnabled = ConfigurationManager.AppSettings.GetBoolValue("StagingModuleStagingProdSyncEnabled");

			// Automatically Add Prod Environment
			AutomaticSyncEnvironments.Add(ProdEnvironment);

			if (StagingModuleStagingProdSyncEnabled)
			{
				AutomaticSyncEnvironments.Add("STAGING");
			}
			else
			{
				LowerEnvironments.Add("STAGING");
			}
			this.customCmsModuleLoggingService = new CustomCmsModuleLoggingService();

		}

		internal void LogStagingTask_Before(object sender, StagingLogTaskEventArgs e)
		{
			customCmsModuleLoggingService.LogInformation("StagingModuleService", "LogStagingTask_Before");

			try
			{
				// Prevent Smart Search Index info changes being staged
				if (e.Object is SearchIndexInfo searchIndexInfo && searchIndexInfo.IndexProvider == "Azure")
				{
					e.Cancel();
				}
			}
			catch (Exception)
			{

			}
		}

		bool SyncAllowed(StagingTaskInfo task)
		{
			if (!StagingModuleEnabled)
			{
				return false;
			}

			// Doesnt Matter What Environment

			// media files take some time to process which causes auto sync to fail especially when compressing the media file
			if (task.TaskObjectType.Equals("media.file", StringComparison.InvariantCultureIgnoreCase))
			{
				var mediaFileId = task.TaskObjectID;
				var mediaFileInfo = MediaFileInfo.Provider.Get(mediaFileId);
				if (mediaFileInfo == null)
				{
					customCmsModuleLoggingService.LogInformation("StagingModuleService", "SyncAllowed_False - media file is not ready", $"StagingTaskInfo - {task.TaskID}");
					return false;
				}
			}

			// do not auto sync page type updates in the event it was created in an upper environment
			if (task.TaskObjectType.Equals("cms.documenttype", StringComparison.InvariantCultureIgnoreCase))
			{
				customCmsModuleLoggingService.LogInformation("StagingModuleService", "SyncAllowed_False - do not auto sync page types", $"StagingTaskInfo - {task.TaskID}");
				return false;
			}

			// Environment Order Matters Below

			// Production Environment
			if (ProdEnvironment.Equals(AppEnvironment))
			{
				// Task has already been processed by the production environment and should be synced.
				customCmsModuleLoggingService.LogInformation("StagingModuleService", "SyncAllowed_TRUE - task has already been processed by the production environment", $"StagingTaskInfo - {task.TaskID}");
				return true;
			}

			// LOWER ENVIRONMENTS
			if (LowerEnvironments.Contains(AppEnvironment))
			{
				// Task was not started in Auto Sync Environment, so no automatic sync
				if (!(AutomaticSyncEnvironments.Any(x => task.WasProcessed(x))))
				{
					customCmsModuleLoggingService.LogInformation("StagingModuleService", "SyncAllowed_False - lower environment - task not processed by auto sync environment", $"StagingTaskInfo - {task.TaskID}");
					return false;
				}
			}

			// AUTO SYNC ENVIRONMENTS
			if (AutomaticSyncEnvironments.Contains(AppEnvironment))
			{
				// Task was started in the lower environments, so do not sync
				if (LowerEnvironments.Any(x => task.WasProcessed(x)))
				{
					customCmsModuleLoggingService.LogInformation("StagingModuleService", "SyncAllowed_False - auto sync environment - task originated from lower environment", $"StagingTaskInfo - {task.TaskID}");
					return false;
				}
			}

			// stop gap for auto sync 			
			if (AutomaticSyncEnvironments.Any(x => task.WasProcessed(x)))
			{
				if (StagingModuleStagingProdSyncEnabled)
				{
					if (AppEnvironment.Equals("QA", StringComparison.InvariantCultureIgnoreCase))
					{
						customCmsModuleLoggingService.LogInformation("StagingModuleService", "SyncAllowed_False - stop gap for QA environment", $"StagingTaskInfo - {task.TaskID}");
						return false;
					}
				}
				else
				{
					if (AppEnvironment.Equals("STAGING", StringComparison.InvariantCultureIgnoreCase))
					{
						customCmsModuleLoggingService.LogInformation("StagingModuleService", "SyncAllowed_False - stop gap for STAGING environment", $"StagingTaskInfo - {task.TaskID}");
						return false;
					}
				}
			}



			return true;
		}

		internal void SyncTask(StagingTaskInfo task)
		{
			var syncAllowed = SyncAllowed(task);
			if (syncAllowed)
			{
				customCmsModuleLoggingService.LogInformation("StagingModuleService", "SyncTask_Start", "SyncTask");

				// Gets the identifiers of the staging servers for which the task was created
				var taskServerIds = SynchronizationInfo.Provider.Get()
																		.Column("SynchronizationServerID")
																		.WhereEquals("SynchronizationTaskID", task.TaskID);

				// Gets the task's staging servers based on the retrieved identifiers
				var targetServers = ServerInfo.Provider.Get().WhereIn("ServerID", taskServerIds);

				// Processes the task for all relevant servers
				foreach (ServerInfo server in targetServers)
				{
					// Synchronizes the processed staging task to the target server
					new StagingTaskRunner(server.ServerID).RunSynchronization(task.TaskID);
				}

				customCmsModuleLoggingService.LogInformation("StagingModuleService", "SyncTask_END", "SyncTask");
			}
		}

		internal void SyncTasks()
		{
			StagingTaskInfoProvider.DeleteRedundantTasks();

			IStagingTaskInfoProvider stagingTaskInfoProvider = Service.Resolve<IStagingTaskInfoProvider>();
			var tasks = stagingTaskInfoProvider.Get().TopN(1000).ToList();

			foreach (var task in tasks)
			{
				SyncTask(task);
			}
		}

		internal void LogStagingTask_After(object sender, StagingTaskEventArgs e)
		{
			customCmsModuleLoggingService.LogInformation("StagingModuleService", "LogStagingTask_After_Start", "LogStagingTask_After");

			var task = e.Task;
			if (task != null)
			{
				SyncTask(task);
			}

			customCmsModuleLoggingService.LogInformation("StagingModuleService", "LogStagingTask_After_End", "LogStagingTask_After");
		}


	}
}
