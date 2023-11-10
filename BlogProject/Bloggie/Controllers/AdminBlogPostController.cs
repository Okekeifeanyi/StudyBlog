using Bloggie.Core.Implementations.Interface;
using Bloggie.Model.Models;
using Bloggie.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bloggie.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminBlogPostController : Controller
    {
        private readonly ITagRepository _tagRepository;
        private readonly IBlogPostRepository _blogPostRepository;

        public AdminBlogPostController(ITagRepository tagRepository, IBlogPostRepository blogPostRepository)
        {
            _tagRepository = tagRepository;
            _blogPostRepository = blogPostRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var tags = await _tagRepository.GetAllAsync();
            var model = new AddBlogPostRequest
            {
                Tags = tags.Select(x => new SelectListItem { Text = x.DisplayName, Value = x.Id.ToString() }) 
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostRequest addBlogPostRequest)
        {
            //Map view model to domain model

            var blogPost = new BlogPost
            {
                Heading = addBlogPostRequest.Heading,
                PageTitle = addBlogPostRequest.PageTitle,
                PageContent = addBlogPostRequest.PageContent,
                ShortDescription = addBlogPostRequest.ShortDescription,
                FeaturedImageUrl = addBlogPostRequest.FeaturedImageUrl,
                UrlHandle = addBlogPostRequest.UrlHandle,
                PublishedDtae = addBlogPostRequest.PublishedDtae,
                Author = addBlogPostRequest.Author,
                Visible = addBlogPostRequest.Visible,
            };

            //MAp tags from the selected tags 
            var selectedTag = new List<Tag>();
            foreach (var selectedTagId in addBlogPostRequest.SelectedTag)
            {
                var selectedTagIsGuid = Guid.Parse(selectedTagId);
                var existingTag = await _tagRepository.GetByIdAsync(selectedTagIsGuid);

                if (existingTag != null)
                {
                    selectedTag.Add(existingTag);
                }
            }
            blogPost.Tags = selectedTag;

            await _blogPostRepository.AddPostAsync(blogPost);
            return RedirectToAction("Add");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            //call the repository 
            var blogPost = await _blogPostRepository.GetAllAsync();

            return View(blogPost);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            //Retrieve the result from the repository
            var blogPost = await _blogPostRepository.GetByIdAsync(id);
            var tagsDomainModel = await _tagRepository.GetAllAsync();

            if (blogPost != null)
            {
                //map the domain model into the view model 
                var model = new EditBlogPostRequest
                {
                    Id = blogPost.Id,
                    Heading = blogPost.Heading,
                    PageTitle = blogPost.PageTitle,
                    PageContent = blogPost.PageContent,
                    Author = blogPost.Author,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    UrlHandle = blogPost.UrlHandle,
                    ShortDescription = blogPost.ShortDescription,
                    PublishedDtae = blogPost.PublishedDtae,
                    Visible = blogPost.Visible,
                    Tags = tagsDomainModel.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }),
                    SelectedTag = blogPost.Tags.Select(x => x.Id.ToString()).ToArray()
                };
                return View(model);

            }

           //pass data to the view
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditBlogPostRequest editBlogPostRequest)
        {
            //map view model back to dormain model
            var blogPostDomainModel = new BlogPost
            {
                Id = editBlogPostRequest.Id,
                Heading = editBlogPostRequest.Heading,
                PageTitle = editBlogPostRequest.PageTitle,
                PageContent = editBlogPostRequest.PageContent,
                Author = editBlogPostRequest.Author,
                ShortDescription = editBlogPostRequest.ShortDescription,
                FeaturedImageUrl = editBlogPostRequest.FeaturedImageUrl,
                PublishedDtae = editBlogPostRequest.PublishedDtae,
                UrlHandle = editBlogPostRequest.UrlHandle,
                Visible = editBlogPostRequest.Visible
            };

           //Map tags into domain model
           var selectedTag = new List<Tag>();
           foreach(var item in editBlogPostRequest.SelectedTag)
            {
                if (Guid.TryParse(item, out var tag))
                {
                    var foundTag = await _tagRepository.GetByIdAsync(tag);
                    if (foundTag != null)
                    {
                        selectedTag.Add(foundTag);
                    }
                }
            }

           blogPostDomainModel.Tags = selectedTag;
           
            //sub mit information to the repositroy to update
            var updatedBlog = await _blogPostRepository.UpdatePostAsync(blogPostDomainModel);

            if (updatedBlog != null)
            {
                //Show Success Notification
                return RedirectToAction("Edit");
            }
            //redirect to the get method 
            return View(blogPostDomainModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditBlogPostRequest editBlogPostRequest)
        {
            //Talk to repository to delete this blog post and tags
            var DeletedBlogPost = await _blogPostRepository.DeletePostAsync(editBlogPostRequest.Id);
            if (DeletedBlogPost != null)
            {
                //SHow success notification 
                return RedirectToAction("List");
            }
            //Display the response
            return RedirectToAction("Edit", new {id = editBlogPostRequest.Id});
        }
        
    }
}
