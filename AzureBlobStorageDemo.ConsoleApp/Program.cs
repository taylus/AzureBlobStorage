using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;

namespace AzureBlobStorageDemo
{
    public static class Program
    {
        private const string connectionString = "UseDevelopmentStorage=true";
        private const string containerName = "photos";
        private const string blobName = "blob.png";

        public static async Task Main()
        {
            Console.WriteLine("Azure Blob Storage demo");
            Console.WriteLine("=======================");

            Console.WriteLine($"Using connection string: {connectionString}");
            var account = CloudStorageAccount.Parse(connectionString);
            var client = account.CreateCloudBlobClient();
            var container = client.GetContainerReference(containerName);

            Console.WriteLine($"Creating container \"{containerName}\" if it doesn't already exist...");
            await container.CreateIfNotExistsAsync(BlobContainerPublicAccessType.Blob, null, null);
            Console.WriteLine();

            Console.WriteLine($"Uploading \"{blobName}\" to container \"{containerName}\"...");
            Console.WriteLine("Uploaded: " + await Upload(container, blobName, "image/png"));
            Console.WriteLine();

            const string downloadedFile = "download.png";
            Console.WriteLine($"Downloading \"{blobName}\" as \"{downloadedFile}\"...");
            Console.WriteLine("Downloaded: " + await Download(container, blobName, downloadedFile));
            Process.Start(new ProcessStartInfo(downloadedFile) { UseShellExecute = true });
        }

        private static async Task<Uri> Upload(CloudBlobContainer container, string blobName, string contentType)
        {
            var blob = container.GetBlockBlobReference(blobName);
            blob.Properties.ContentType = contentType;
            await blob.UploadFromFileAsync(blobName);
            return blob.Uri;
        }

        private static async Task<string> Download(CloudBlobContainer container, string blobName, string outputPath)
        {
            var blob = container.GetBlockBlobReference(blobName);
            await blob.DownloadToFileAsync(outputPath, FileMode.Create);
            return outputPath;
        }
    }
}
