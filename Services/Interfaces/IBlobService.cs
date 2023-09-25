using Microsoft.AspNetCore.Http;

namespace Services.Interfaces;

public interface IBlobService
{
    Task<string> UploadAsync(IFormFile file);
}