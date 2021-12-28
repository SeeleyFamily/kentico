using CMS.DataEngine;
using System;
using System.Configuration;

namespace Common.Migration.Infrastructure
{
	public static class MigrationUtilities
	{

		public static void InitKentico()
		{
			Console.Write("Initializing Kentico DataEngine...");

			CMSApplication.PreInit();			

			CMSApplication.Init();


			WriteSuccessToConsole();
		}

		public static int GetSiteId()
		{
			string siteIdString = ConfigurationManager.AppSettings["SiteId"];
			if(int.TryParse(siteIdString,out int result))
			{
				return result;
			}
			throw new Exception("Please enter a valid SiteId in App Settings.");
		}

		public static void WriteSeparator()
		{
			Console.WriteLine("---------------------------------------------------------------");
		}

		public static void WriteSuccessToConsole(string message = "Success")
		{
			Console.Write($" {message}");
			Console.WriteLine();
		}
	}
}
