﻿using CMS.Core;
using CMS.Scheduler;
using Launchpad.Infrastructure.Kentico.CMS.Services;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Launchpad.Infrastructure.Kentico.CMS.ScheduledTasks
{
	public class MovePageNodesScheduledTask : ITask
	{
		#region Fields
		private readonly PageNodesModuleService pageNodeModuleService;
		private readonly string appEnvironment;
		#endregion

		public MovePageNodesScheduledTask()
		{
			pageNodeModuleService = new PageNodesModuleService();
			appEnvironment = ConfigurationManager.AppSettings["AppEnvironment"];
		}


		public string Execute(TaskInfo ti)
		{
			string details = "Custom scheduled task executed. Task data: " + ti.TaskData;

			// Logs the execution of the task in the event log
			Service.Resolve<IEventLogService>().LogInformation("MovePageNodesScheduledTask", "Execute", details);

			var result = "";
			if (new List<string>() { "LOCAL", "DEV" }.Contains(appEnvironment))
			{
				try
				{
					pageNodeModuleService.MovePageNodes();
				}
				catch (Exception e)
				{
					result = e.Message;
				}
			}
			else
			{
				result = "This process only runs on Local or Development Environments.";
			}

			// Returns a null value to indicate that the task executed successfully
			// Return an error message string with details in cases where the execution fails
			return result;
		}
	}
}
