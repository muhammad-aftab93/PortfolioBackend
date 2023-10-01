using Microsoft.AspNetCore.Http;

namespace Services.Interfaces;

public interface IBlobService
{
    Task<string> UploadAsync(IFormFile file, string containerName);
    Task<bool> RemoveFileAsync(string fileName, string containerName);
}