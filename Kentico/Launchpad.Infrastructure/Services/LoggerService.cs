using CMS.Core;
using Launchpad.Core.Abstractions.Configuration;
using Launchpad.Core.Abstractions.Services;
using Newtonsoft.Json;
using System;
using System.Runtime.CompilerServices;


namespace Launchpad.Infrastructure.Services
{

	public class LoggerService : ILoggerService, IPerApplicationService
	{

		public void LogError(string message, string eventCode, object data = null, Exception exception = null, [CallerMemberName] string source = "")
		{
			if (exception != null)
			{
				message += $"<p>Exception:<br/>{exception.Message}</p><p>Stack Trace:<br/>{exception.StackTrace}</p>";
			}


			LogInternal(EventTypeEnum.Error, source, eventCode, message, data);
		}


		public void LogInformation(string message, string eventCode, object data = null, [CallerMemberName] string source = "")
		{
			LogInternal(EventTypeEnum.Information, source, eventCode, message, data);
		}


		public void LogWarning(string message, string eventCode, object data = null, [CallerMemberName] string source = "")
		{
			LogInternal(EventTypeEnum.Warning, source, eventCode, message, data);
		}



		private void LogInternal(EventTypeEnum type, string source, string eventCode, string message, object data = null)
		{
			// If there's an object, parse it and add it to the message
			if (data != null)
			{
				message += $"<p>Data:<br/>{JsonConvert.SerializeObject(data)}</p>";
			}

			try
			{
				var loggerService = Service.Resolve<IEventLogService>();

				if (loggerService != null)
				{
					loggerService.LogEvent(type, source, eventCode, eventDescription: message);
				}
			}
			catch (Exception)
			{
				// Sometimes the logger is not available during start up
			}
		}

	}

}
