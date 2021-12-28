using CMS;
using CMS.DataEngine;
using CMS.DocumentEngine;
using Launchpad.Infrastructure.Kentico.CMS.Modules;
using Launchpad.Infrastructure.Kentico.CMS.Services;
using Launchpad.Infrastructure.Modules;

[assembly: RegisterModule(typeof(ExampleGlobalEventsModule))]
namespace Launchpad.Infrastructure.Kentico.CMS.Modules
{
	class ExampleGlobalEventsModule : CustomCmsModule
	{
		#region Fields
		private readonly ExampleGlobalEventsModuleService exampleGlobalEventsModuleService;
		#endregion

		public ExampleGlobalEventsModule()
			: base(nameof(ExampleGlobalEventsModule))
		{
			exampleGlobalEventsModuleService = new ExampleGlobalEventsModuleService();
			this.SettingDisableModuleCodeName = "DisableExampleGlobalEventsModule";

		}

		protected override void RegisterModuleEvents()
		{
			// Document Events
			DocumentEvents.Update.Before += exampleGlobalEventsModuleService.UpdateBefore;
			DocumentEvents.Update.After += exampleGlobalEventsModuleService.UpdateAfter;

			DocumentEvents.Insert.Before += exampleGlobalEventsModuleService.InsertBefore;
			DocumentEvents.Insert.After += exampleGlobalEventsModuleService.InsertAfter;

			DocumentEvents.Delete.Before += exampleGlobalEventsModuleService.DeleteBefore;
			DocumentEvents.Delete.After += exampleGlobalEventsModuleService.DeleteAfter;

			DocumentEvents.LogChange.Before += exampleGlobalEventsModuleService.LogChangeBefore;
			DocumentEvents.LogChange.After += exampleGlobalEventsModuleService.LogChangeAfter;

			// Workflow Events
			WorkflowEvents.Publish.Before += exampleGlobalEventsModuleService.PublishBefore;
			WorkflowEvents.Publish.After += exampleGlobalEventsModuleService.PublishAfter;
			WorkflowEvents.SaveVersion.After += exampleGlobalEventsModuleService.SaveVersionAfter;
			WorkflowEvents.CheckIn.After += exampleGlobalEventsModuleService.CheckInAfter;

			// Object Events
			ObjectEvents.Update.Before += exampleGlobalEventsModuleService.UpdateBefore;
			ObjectEvents.Update.After += exampleGlobalEventsModuleService.UpdateAfter;

			ObjectEvents.Insert.Before += exampleGlobalEventsModuleService.InsertBefore;
			ObjectEvents.Insert.After += exampleGlobalEventsModuleService.InsertAfter;

			ObjectEvents.Delete.Before += exampleGlobalEventsModuleService.DeleteBefore;
			ObjectEvents.Delete.After += exampleGlobalEventsModuleService.DeleteAfter;

			ObjectEvents.LogChange.Before += exampleGlobalEventsModuleService.LogChangeBefore;
			ObjectEvents.LogChange.After += exampleGlobalEventsModuleService.LogChangeAfter;
		}
	}
}
