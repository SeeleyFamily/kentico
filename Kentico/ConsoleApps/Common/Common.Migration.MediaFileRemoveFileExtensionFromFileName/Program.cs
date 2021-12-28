using CMS.MediaLibrary;
using Common.Migration.Infrastructure;
using Launchpad.Core.Extensions;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace Common.Migration.MediaFileRemoveFileExtensionFromFileName
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
                RemoveFileExtensions();
            }
            catch (Exception e)
            {
                Messages.Add($"Error: {e.Message}");
            }
        }

        private void RemoveFileExtensions()
        {
            try
            {
                Logger.Out($"-------------------------------{ConfigurationManager.AppSettings["SiteName"]} - {ConfigurationManager.AppSettings["CMSStagingServerName"]}-------------------------------");
                Logger.Out($"-------------------------------Start - {DateTime.Now}-------------------------------");
                MediaLibraryInfo library = MediaLibraryInfo.Provider.Get(SiteId);


                if (library != null)
                {

                    var mediaFiles = MediaFileInfo.Provider.Get().ToList();

                    var mediaFileWithExtensions = mediaFiles.Where(x => x.FileName.ToLower().EndsWith(x.FileExtension.ToLower())).OrderBy(x=>x.FileID).ToList();
                   
                    int i = 1;
                    foreach (var item in mediaFileWithExtensions)
                    {
                        if (item.FileName.ToLower().EndsWith(item.FileExtension.ToLower()))
                        {
                            
                            Logger.Out($"{i}.Library Name = {item.FileLibraryID} File name = {item.FileName}");

                            //// Updates the media library file properties
                            //item.FileName = item.FileName.ToLower().Replace(item.FileExtension.ToLower(), "");

                            //// Saves the media library file
                            //MediaFileInfo.Provider.Set(item);
                            i++;
                        }
                    }
                    Logger.Out($"-------------------------------Finish - {DateTime.Now}-------------------------------");
                    Logger.Out($"-------------------------------{ConfigurationManager.AppSettings["SiteName"]} - {ConfigurationManager.AppSettings["CMSStagingServerName"]}-------------------------------");
                    WriteToFile(Logger.LogString.ToString());
                }
                Console.Read();
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Logger.Out($"Error encountered: {ex.Message}");
            }
        }

        public static class Logger
        {
            public static StringBuilder LogString = new StringBuilder();
            public static void Out(string str)
            {
                Console.WriteLine(str);
                LogString.Append(str).Append(Environment.NewLine);
            }
        }

        private void WriteToFile(string s)
        {
            string path = @"C:\MediaFileRemoveFileExtensionFromFileName.txt";

            if (!File.Exists(path))
            {
                File.Create(path).Dispose();

                using (TextWriter tw = new StreamWriter(path))
                {
                    tw.WriteLine(s);
                }

            }
            else if (File.Exists(path))
            {
                using (TextWriter tw = new StreamWriter(path,false))
                {
                    tw.WriteLine(s);
                }
            }
        }


    }

}
