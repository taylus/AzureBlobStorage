using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.Storage.Blob;

namespace AzureBlobStorageDemo.WebApp.Services
{
    public interface IAzureBlobStorageContainer
    {
        Task<ICloudBlob> UploadFromStreamAsync(string name, Stream data, string contentType);
    }
}
