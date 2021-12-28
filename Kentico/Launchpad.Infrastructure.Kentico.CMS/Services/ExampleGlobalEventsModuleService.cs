using CMS.DataEngine;
using CMS.DocumentEngine;
using Launchpad.Infrastructure.Services;
using System;
using System.Configuration;

namespace Launchpad.Infrastructure.Kentico.CMS.Services
{
	public class ExampleGlobalEventsModuleService
	{
		#region Properties
		private string AppEnvironment { get; set; }
		#endregion

		#region fields
		private readonly CustomCmsModuleLoggingService customCmsModuleLoggingService;
		#endregion

		public ExampleGlobalEventsModuleService()
		{
			AppEnvironment = ConfigurationManager.AppSettings["AppEnvironment"];
			this.customCmsModuleLoggingService = new CustomCmsModuleLoggingService();
			// this will overload the event log
			// turn this off by default
			// use for debugging purposes only
			this.customCmsModuleLoggingService.AllowLogging = false;
		}

		public void LogChangeBefore(object sender, LogDocumentChangeEventArgs e)
		{
			customCmsModuleLoggingService.LogInformation("ExampleGlobalEventsModuleService", "LogChangeBefore");
		}

		public void LogChangeAfter(object sender, LogDocumentChangeEventArgs e)
		{
			customCmsModuleLoggingService.LogInformation("ExampleGlobalEventsModuleService", "LogChangeAfter");
		}

		public void InsertBefore(object sender, DocumentEventArgs e)
		{
			customCmsModuleLoggingService.LogInformation("ExampleGlobalEventsModuleService", "InsertBefore");
		}

		public void InsertAfter(object sender, DocumentEventArgs e)
		{
			customCmsModuleLoggingService.LogInformation("ExampleGlobalEventsModuleService", "InsertAfter");
		}

		public void UpdateBefore(object sender, DocumentEventArgs e)
		{
			customCmsModuleLoggingService.LogInformation("ExampleGlobalEventsModuleService", "UpdateBefore");
		}

		public void UpdateAfter(object sender, DocumentEventArgs e)
		{
			customCmsModuleLoggingService.LogInformation("ExampleGlobalEventsModuleService", "UpdateAfter");
		}
		
		public void DeleteBefore(object sender, DocumentEventArgs e)
		{
			customCmsModuleLoggingService.LogInformation("ExampleGlobalEventsModuleService", "DeleteBefore");
		}

		public void DeleteAfter(object sender, DocumentEventArgs e)
		{
			customCmsModuleLoggingService.LogInformation("ExampleGlobalEventsModuleService", "DeleteAfter");
		}

		public void PublishBefore(object sender, WorkflowEventArgs e)
		{
			customCmsModuleLoggingService.LogInformation("ExampleGlobalEventsModuleService", "PublishBefore");
		}

		public void PublishAfter(object sender, WorkflowEventArgs e)
		{
			customCmsModuleLoggingService.LogInformation("ExampleGlobalEventsModuleService", "PublishAfter");
		}
		public void SaveVersionAfter(object sender, WorkflowEventArgs e)
		{
			customCmsModuleLoggingService.LogInformation("ExampleGlobalEventsModuleService", "SaveVersionAfter");
		}

		public void CheckInAfter(object sender, WorkflowEventArgs e)
		{
			customCmsModuleLoggingService.LogInformation("ExampleGlobalEventsModuleService", "CheckInAfter");
		}

		public void LogChangeBefore(object sender, LogObjectChangeEventArgs e)
		{
			customCmsModuleLoggingService.LogInformation("ExampleGlobalEventsModuleService", "LogChangeBefore");
		}

		public void LogChangeAfter(object sender, LogObjectChangeEventArgs e)
		{
			customCmsModuleLoggingService.LogInformation("ExampleGlobalEventsModuleService", "LogChangeAfter");
		}

		public void InsertBefore(object sender, ObjectEventArgs e)
		{
			customCmsModuleLoggingService.LogInformation("ExampleGlobalEventsModuleService", "InsertBefore");
		}

		public void InsertAfter(object sender, ObjectEventArgs e)
		{
			customCmsModuleLoggingService.LogInformation("ExampleGlobalEventsModuleService", "InsertAfter");
		}

		public void UpdateBefore(object sender, ObjectEventArgs e)
		{
			customCmsModuleLoggingService.LogInformation("ExampleGlobalEventsModuleService", "UpdateBefore");
		}

		public void UpdateAfter(object sender, ObjectEventArgs e)
		{
			customCmsModuleLoggingService.LogInformation("ExampleGlobalEventsModuleService", "UpdateAfter");
		}

		public void DeleteBefore(object sender, ObjectEventArgs e)
		{
			customCmsModuleLoggingService.LogInformation("ExampleGlobalEventsModuleService", "DeleteBefore");
		}

		public void DeleteAfter(object sender, ObjectEventArgs e)
		{
			customCmsModuleLoggingService.LogInformation("ExampleGlobalEventsModuleService", "DeleteAfter");
		}
	}
}
