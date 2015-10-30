using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ApiDemo.Infrastructure
{


    public class BlobHelper
    {
        public static CloudStorageAccount StorageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["AzureStorageAccountConnectionString"]);
        private CloudBlobContainer blobContainer;


        public BlobHelper(String container)
        {
            // get or create blobContainer to communicate with Azure storage service
            var blobClient = StorageAccount.CreateCloudBlobClient();
            blobContainer = blobClient.GetContainerReference(container);
            blobContainer.CreateIfNotExists();
            blobContainer.SetPermissions(new BlobContainerPermissions() { PublicAccess = BlobContainerPublicAccessType.Blob });
        }

        public void InitializeContainer(string container)
        {
            var blobClient = StorageAccount.CreateCloudBlobClient();
            blobContainer = blobClient.GetContainerReference(container);
            blobContainer.CreateIfNotExists();
            blobContainer.SetPermissions(new BlobContainerPermissions() { PublicAccess = BlobContainerPublicAccessType.Blob });
        }

        public async Task<bool> Upload(HttpPostedFileBase file, string fileName)
        {

            try
            {
                CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(fileName);

                await blockBlob.UploadFromStreamAsync(file.InputStream);
                blockBlob.Properties.ContentType = file.ContentType;
                await blockBlob.SetPropertiesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }






        public async Task<bool> Upload(System.IO.Stream steam, string filename)
        {
            try
            {
                CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(filename);

                await blockBlob.UploadFromStreamAsync(steam);
                // blockBlob.Properties.ContentType = System.IO.;
                await blockBlob.SetPropertiesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UploadFile(string file, string fileName, string ContentType = "image/jpeg")
        {

            try
            {
                CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(fileName);

                await blockBlob.UploadFromFileAsync(file, System.IO.FileMode.Open);
                blockBlob.Properties.ContentType = ContentType;
                await blockBlob.SetPropertiesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UploadFileNoAwait(string file, string fileName, string ContentType = "image/jpeg")
        {

            try
            {
                CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(fileName);

                blockBlob.UploadFromFile(file, System.IO.FileMode.Open);
                blockBlob.Properties.ContentType = ContentType;
                blockBlob.SetProperties();
                return true;
            }
            catch
            {
                return false;
            }
        }

        //DownloadBlobFromContainer(container, "Fuck.txt", @"E:\Fucked.txt");
        private async void DownloadBlobFromContainer(string blobName, string downloadFilePath)
        {
            CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(blobName);
            using (var fileStream = System.IO.File.OpenWrite(downloadFilePath))
            {
                await blockBlob.DownloadToStreamAsync(fileStream);
            }
        }

    }
}