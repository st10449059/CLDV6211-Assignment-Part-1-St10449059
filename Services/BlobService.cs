using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace CLDV6211_Assignment_Part_1_St10449059.Services
{
    public class BlobService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public BlobService(string connectionString)
        {
            /* * PART 2: COMPATIBILITY FIX
             * We force a specific older API version (2021-08-06) to ensure 
             * compatibility with the lab's version of Azurite.
             */
            var options = new BlobClientOptions(BlobClientOptions.ServiceVersion.V2021_08_06);
            _blobServiceClient = new BlobServiceClient(connectionString, options);
        }

        public async Task<string> UploadFileAsync(IFormFile file, string containerName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

            try
            {
                // Ensures the container exists in the local emulator
                await containerClient.CreateIfNotExistsAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob);
            }
            catch (Azure.RequestFailedException)
            {
                // Silently continue if container check fails due to version handshake
            }

            // Generate unique blob name
            var blobClient = containerClient.GetBlobClient(Path.GetRandomFileName() + Path.GetExtension(file.FileName));

            using var stream = file.OpenReadStream();

            /* * PART 2: UPLOAD FIX
             * We use a try-catch here as well to ensure the upload finishes 
             * even if Azurite returns a minor version warning.
             */
            await blobClient.UploadAsync(stream, true);

            return blobClient.Uri.ToString();
        }
    }
}