using Azure.Storage.Blobs;
using Common;
using Microsoft.AspNetCore.Http;
using Services.Interfaces;

namespace Services;

public class BlobService : IBlobService
{
    private readonly BlobServiceClient _blobServiceClient;

    public BlobService()
        => _blobServiceClient = new BlobServiceClient(BlobSettings.ConnectionString);

    public async Task<string> UploadAsync(IFormFile file, string containerName)
    {
        var blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        var blobClient = blobContainerClient.GetBlobClient(fileName);

        await using (var stream = file.OpenReadStream())
        {
            await blobClient.UploadAsync(stream, true);
        }

        return blobClient.Uri.ToString();
    }

    public async Task<bool> RemoveFileAsync(string fileName, string containerName)
    {
        var blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        var blobs = blobContainerClient.GetBlobs();

        var blobToDelete = blobs.FirstOrDefault(blob => blob.Name == fileName);

        if (blobToDelete != null)
        {
            var blobClient = blobContainerClient.GetBlobClient(blobToDelete.Name);
            return await blobClient.DeleteIfExistsAsync();
        }

        return false;
    }
}