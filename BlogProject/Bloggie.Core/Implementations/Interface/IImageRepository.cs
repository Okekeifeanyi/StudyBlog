using Microsoft.AspNetCore.Http;

namespace Bloggie.Core.Implementations.Interface
{
    public interface IImageRepository
    {
        Task<string> UploadAsync(IFormFile file);
    }
}
