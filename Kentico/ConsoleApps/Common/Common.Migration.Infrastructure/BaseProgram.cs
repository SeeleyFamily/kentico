using CMS.Base;
using CMS.DocumentEngine;
using CMS.IO;
using CMS.Membership;
using Launchpad.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Common.Migration.Infrastructure
{
	public abstract class BaseProgram
	{
		#region Properties
		public List<string> Messages { get; set; } = new List<string>();
		public int SiteId { get; set; }
		public string DefaultCultureCode { get; set; } = "en-US";
		public string DefaultAdministratorUser { get; set; } = "administrator";
		public TreeProvider Tree { get; set; }
		#endregion

		public BaseProgram()
		{
			SiteId = ConfigurationManager.AppSettings.GetIntValue("SiteID");
		}

		public virtual void Main()
		{
			StartUp();
			try
			{
				// Gets an object representing a specific Kentico user
				UserInfo user = UserInfo.Provider.Get("administrator");

				// Sets the context of the user
				using (new CMSActionContext(user))
				{
					RunConsoleApp();
				}
			}
			catch (Exception e)
			{
				Console.WriteLine($"Error: There was an error in the RunConsoleApp Block of this console application.");
				Console.WriteLine($"Error Messge: ${e.Message}");
				Console.WriteLine($"Error Stacktrace: ${e.StackTrace}");
			}
			CleanUp();
		}

		public virtual void StartUp()
		{
			try
			{
				Console.WriteLine("App Startup...");
				MigrationUtilities.InitKentico();
				Tree = new TreeProvider();

				InitAzureStorage();
			}
			catch (Exception e)
			{
				Console.WriteLine($"Error: There was an error in the Startup Block of this console application.");
				Console.WriteLine($"Error Messge: ${e.Message}");
				Console.WriteLine($"Error Stacktrace: ${e.StackTrace}");
			}

			MigrationUtilities.WriteSeparator();
		}

		private void InitAzureStorage()
		{
			bool.TryParse(ConfigurationManager.AppSettings["CMSAzureStorageEnabled"], out var isAzureStorageEnabled);
			if (isAzureStorageEnabled)
			{
				// Creates a new StorageProvider instance for Azure
				var mediaProvider = StorageProvider.CreateAzureStorageProvider();

				// Specifies the target container
				mediaProvider.CustomRootPath = "kenticomediacontainer";

				// Makes the container publicly accessible
				mediaProvider.PublicExternalFolderObject = true;

				// Maps a directory to the provider
				StorageHelper.MapStoragePath("~/SharedMedia", mediaProvider);
			}
		}

		public virtual void CleanUp()
		{
			WriteMessages();
			MigrationUtilities.WriteSeparator();
			Console.Write("Press any key to end");
			Console.ReadKey();
		}

		public virtual void WriteMessages()
		{
			MigrationUtilities.WriteSeparator();
			Console.WriteLine("Messages");
			MigrationUtilities.WriteSeparator();
			foreach (var message in Messages)
			{
				Console.WriteLine(message);
			}
		}


		public abstract void RunConsoleApp();
	}
}
