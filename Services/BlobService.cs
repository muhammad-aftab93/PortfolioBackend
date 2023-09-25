using Azure.Storage.Blobs;
using Common;
using Microsoft.AspNetCore.Http;
using Services.Interfaces;

namespace Services;

public class BlobService : IBlobService
{
    private readonly BlobServiceClient _blobServiceClient;
    
    public BlobService()
    {
        _blobServiceClient = new BlobServiceClient(BlobSettings.ConnectionString);
    }
    
    public async Task<string> UploadAsync(IFormFile file)
    {
        var blobContainerClient = _blobServiceClient.GetBlobContainerClient(BlobSettings.ContainerName);
        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        var blobClient = blobContainerClient.GetBlobClient(fileName);

        await using (var stream = file.OpenReadStream())
        {
            await blobClient.UploadAsync(stream, true);
        }

        return blobClient.Uri.ToString();
    }
}