using Bloggie.Core.Implementations.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Bloggie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }
        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {//call a repository 
            var imageUrl = await _imageRepository.UploadAsync(file);

            if (imageUrl == null)
            {
                return Problem("Image Upload Not Successful", null, (int)HttpStatusCode.InternalServerError);
            }

            return new JsonResult(new { link = imageUrl });

        }
    }
}
