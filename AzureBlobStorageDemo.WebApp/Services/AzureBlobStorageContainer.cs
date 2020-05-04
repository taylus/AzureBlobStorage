using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;

namespace AzureBlobStorageDemo.WebApp.Services
{
    public class AzureBlobStorageContainer : IAzureBlobStorageContainer
    {
        private readonly CloudStorageAccount account;
        private readonly CloudBlobClient client;
        private readonly CloudBlobContainer container;

        public AzureBlobStorageContainer(string connectionString, string containerName)
        {
            if (string.IsNullOrWhiteSpace(connectionString)) throw new ArgumentException("Azure Blob Storage connection string may not be null or empty.", nameof(connectionString));
            if (string.IsNullOrWhiteSpace(containerName)) throw new ArgumentException("Azure Blob Storage container name may not be null or empty.", nameof(containerName));
            account = CloudStorageAccount.Parse(connectionString);
            client = account.CreateCloudBlobClient();
            container = client.GetContainerReference(containerName);
        }

        public async Task<ICloudBlob> UploadFromStreamAsync(string name, Stream data, string contentType)
        {
            var blob = container.GetBlockBlobReference(name);
            blob.Properties.ContentType = contentType;
            await blob.UploadFromStreamAsync(data);
            return blob;
        }
    }
}
