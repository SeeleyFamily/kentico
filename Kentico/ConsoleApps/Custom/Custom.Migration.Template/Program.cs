using Common.Migration.Infrastructure;
using Launchpad.Core.Extensions;
using System;
using System.Configuration;

namespace Custom.Migration.Template
{
	public class Program : BaseProgram
	{
		static void Main(string[] args)
		{
			var consoleApp = new Program();
			consoleApp.Main();
		}

		public Program() : base()
		{
			Console.WriteLine(ConfigurationManager.AppSettings.GetStringValue("ExampleAppSetting"));
			Console.WriteLine(SiteId); // From BaseProgram App Settings
		}

		public override void RunConsoleApp()
		{
			try
			{
				// Console App Business Logic Here;
			}
			catch (Exception e)
			{
				Messages.Add($"Error: {e.Message}");
			}
		}
	}
}
