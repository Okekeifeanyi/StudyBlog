using Bloggie.Core.Implementations.Interface;
using Bloggie.Models;
using Bloggie.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Bloggie.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITagRepository _tagRepository;
        private readonly IBlogPostRepository _blogPostRepository;

        public HomeController(ILogger<HomeController> logger, IBlogPostRepository blogPostRepository, ITagRepository tagRepository)
        {
            _logger = logger;
            _tagRepository = tagRepository;
            _blogPostRepository = blogPostRepository;
        }

        public async Task<IActionResult> Index()
        {
            //Getting all blog
            var blogPost = await _blogPostRepository.GetAllAsync();

            //Getting all tags
            var tags = await _tagRepository.GetAllAsync();

            var model = new HomeViewModel
            {
                BlogPosts = blogPost,
                Tags = tags
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}