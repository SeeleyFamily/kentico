using CMS.MediaLibrary;
using Common.Migration.Infrastructure;
using Launchpad.Core.Extensions;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.File;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Common.Migration.MediaFileDeleteNotInDb
{
    public class Program : BaseProgram
    {

        #region Properties

        public string DestinationBlob { get; set; }
        #endregion

        static void Main(string[] args)
        {
            var consoleApp = new Program();

            consoleApp.Main();
        }

        public Program() : base()
        {

        }

        public override void RunConsoleApp()
        {
            try
            {
                MoveFilesNotInDB();
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Messages.Add($"Error: {e.Message}");
            }
        }

        /// <summary>
        /// This method will create a folder in Kentico Website Media Library as "notinkentico" and move all the
        /// files which are not in Kentico to that folder, All this files shows up exclanation mark in Kentico
        /// </summary>
        private void MoveFilesNotInDB()
        {

            var accountName = ConfigurationManager.AppSettings.GetStringValue("CMSAzureAccountName");
            var accountKey = ConfigurationManager.AppSettings.GetStringValue("CMSAzureSharedKey");
            var destBlob = string.Empty;

            try
            {
                MediaLibraryInfo library = MediaLibraryInfo.Provider.Get(SiteId);

                if (library != null)
                {
                    var mediaFiles = MediaFileInfo.Provider.Get().ToList();


                    //Connect to blob and Itreate thorugh blob containers
                    Console.WriteLine($"Connecting to Azure Storage account {accountName} and creating client.");
                    var account = CloudStorageAccount.Parse($"DefaultEndpointsProtocol=https;AccountName={accountName};AccountKey={accountKey}");
                    var client = account.CreateCloudBlobClient();

                    //Delete the folder if exists then create notinkentico media library in Kentico website library folder
                    CreateMediaLibraryFolder();

                    ////Fill desinationblob property
                    GetDestinationBlob(client);

                    Console.WriteLine($"-------------------------------Start - {DateTime.Now}-------------------------------");

                    Console.WriteLine($"Iterating through all containers in account {accountName}");
                    foreach (var container in client.ListContainers(null, ContainerListingDetails.All))
                    {
                        Console.WriteLine($"Now inspecting container {container.Name}");
                        var permissions = container.GetPermissions();
                        if (container.Name == "kenticomediacontainer" && (permissions.PublicAccess == BlobContainerPublicAccessType.Blob || permissions.PublicAccess == BlobContainerPublicAccessType.Container))
                        {
                            Console.WriteLine($"Container {container.Name} is a public container or has public blobs, proceeding.");

                            var list = container.ListBlobs(null, true);
                            List<CloudBlockBlob> blobs = list.OfType<CloudBlockBlob>().Where(h => !h.Name.Contains("thumbnail") && !h.Name.Contains("cmsfolder")).ToList();

                            foreach (var blobItem in blobs)
                            {
                                if (blobItem is CloudBlockBlob blob)
                                {

                                    if (blob.Exists())
                                    {

                                        string[] split1 = blob.Name.Split('/').ToArray();
                                        var sp2 = split1.Skip(3);
                                        var blobFilePath = string.Join("/", sp2);

                                        var mediaFile = mediaFiles.Where(x => x.FilePath.ToLower().Contains(blobFilePath.ToLower())).FirstOrDefault();

                                        if (mediaFile == null)
                                        {
                                            Console.WriteLine($"File present in blob but not in DB = {blob.Name}");

                                            if (!string.IsNullOrEmpty(DestinationBlob) && DestinationBlob.Contains("notinkentico"))
                                            {
                                                CloudBlobDirectory rootDir = container.GetDirectoryReference(DestinationBlob);
                                                var dest = rootDir.GetBlockBlobReference(blob.Uri.Segments.Last());
                                                Move(blob, dest);
                                            }
                                        }

                                    }
                                }
                            }

                        }
                    }
                    Console.WriteLine($"-------------------------------Finish - {DateTime.Now}-------------------------------");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error encountered: {ex.Message}");
            }

            Console.Read();
            Console.ReadLine();
        }

        private void CreateMediaLibraryFolder()
        {
            MediaLibraryInfo mediaFolder = MediaLibraryInfo.Provider.Get("WebsiteMediaLibrary", SiteId);

            if (mediaFolder != null)
            {
                //MediaLibraryInfoProvider.DeleteMediaLibraryFolder(ConfigurationManager.AppSettings.GetStringValue("SiteName"), mediaFolder.LibraryID, "notinkentico", false);
                MediaLibraryInfoProvider.CreateMediaLibraryFolder(ConfigurationManager.AppSettings.GetStringValue("SiteName"), mediaFolder.LibraryID, "notinkentico", false);
            }

        }

        private void GetDestinationBlob(CloudBlobClient client)
        {
            foreach (var container in client.ListContainers(null, ContainerListingDetails.All))
            {
                var permissions = container.GetPermissions();
                if (container.Name == "kenticomediacontainer" && (permissions.PublicAccess == BlobContainerPublicAccessType.Blob || permissions.PublicAccess == BlobContainerPublicAccessType.Container))
                {

                    var list = container.ListBlobs(null, true);
                    CloudBlockBlob destBlob = list.OfType<CloudBlockBlob>().Where(h => h.Name.Contains("notinkentico")).FirstOrDefault();
                    if (destBlob != null)
                    {
                        DestinationBlob = $"{destBlob.Name.Replace("/$cmsfolder$", "")}";
                    }
                }
            }

        }

        private void Move(CloudBlockBlob source, CloudBlockBlob destination)
        {
            destination.StartCopy(source);
            source.Delete();
        }
    }
}
