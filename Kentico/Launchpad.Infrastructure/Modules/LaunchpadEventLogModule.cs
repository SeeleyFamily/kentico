using CMS;
using CMS.CustomTables;
using CMS.CustomTables.Types.Common;
using CMS.EventLog;
using Launchpad.Infrastructure.Modules;
using System;
using System.Collections.Generic;
using System.Linq;


[assembly: RegisterModule(typeof(LaunchpadEventLogModule))]


namespace Launchpad.Infrastructure.Modules
{

	public class LaunchpadEventLogModule : CustomCmsModule
	{
		#region fields
		private static string[] descriptionMatches;
		#endregion

		#region Properties

		#endregion

		public LaunchpadEventLogModule()
			: base(nameof(LaunchpadEventLogModule))
		{
			this.SettingDisableModuleCodeName = "DisableEventLogModule";
		}



		protected override void RegisterModuleEvents()
		{
			EventLogEvents.LogEvent.Before += OnBeforeLogEvent;
		}



		private void InitDictionary()
		{
			try
			{
				var supressionList = new List<string>()
				{
					"A potentially dangerous Request.Path value was detected from the client (?)",
					"A potentially dangerous Request.Path value was detected from the client (:)",
					"A potentially dangerous Request.Path value was detected from the client (&)"
				};

				// Get Custom Table Values
				// This comes first as it may fail due to IOC on initialization of the application
				var logSupressionItems = CustomTableItemProvider.GetItems<LogSuppressionItem>().ToList();

				var cmsSupressionList = logSupressionItems.Select(x => x.Log).ToList();
				supressionList.AddRange(cmsSupressionList);

				descriptionMatches = supressionList.ToArray();
			}
			catch (Exception)
			{
				// catch errors in the event start up causes an error
			}
		}


		private void OnBeforeLogEvent(object sender, LogEventArgs e)
		{
			if (descriptionMatches == null)
			{
				InitDictionary();
			}

			EventLogInfo eventInfo = e.Event;


			if (descriptionMatches.Any(d => eventInfo.EventDescription.Contains(d)))
			{
				e.Cancel();
			}
		}
	}

}