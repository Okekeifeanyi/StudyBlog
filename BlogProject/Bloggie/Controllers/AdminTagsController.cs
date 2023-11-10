using Bloggie.Core.Implementations.Interface;
using Bloggie.Model.Models;
using Bloggie.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminTagsController : Controller
    {
        private readonly ITagRepository _tagRepository;

        public AdminTagsController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }
        [HttpGet]
        public IActionResult Add() 
        {
            // Display the add tag form (a static HTML page)
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AdminTagRequest addTagRequest)
        {
            // Validate the incoming data
            if (!ModelState.IsValid)
            {
                // If there are validation errors, return to the add tag form with error messages
                return View(addTagRequest);
            }

            // Mapping the add tag request to the tag domain model
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName,
            };

            try
            {
                // Attempt to add the tag to the database
                await _tagRepository.AddAsync(tag);

                // If the addition is successful, redirect to the "List" action
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
               
                ModelState.AddModelError(string.Empty, "An error occurred while adding the tag.");
                return View(addTagRequest);
            }
        }

       
        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            //Use dbcontest to read the tags
            var tags = await _tagRepository.GetAllAsync();

            return View(tags);
        }

        
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var tag = await  _tagRepository.GetByIdAsync(id);
            if (tag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName,
                };
                return View(editTagRequest);
            }
            return View(null);
        }

        
        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName,
            };

          var updatedTag = await _tagRepository.UpdateAsync(tag);
            if (updatedTag != null)
            { //Show success notification

            }
            else
            {

            }

            return RedirectToAction("Edit", new { id = editTagRequest.Id });

        }

        
        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
        {
          var deletedTag   = await _tagRepository.DeleteAsync(editTagRequest.Id);
            if (deletedTag != null)
            {
                //Show Success Notification
                return RedirectToAction("List");
            }
            //Show an error notification
            return RedirectToAction("Edit", new {id = editTagRequest.Id});
        }
    
        private void ValidateAddTagRequest(AdminTagRequest request)
        {
            if (request.Name is not null && request.DisplayName is not null)
            {
                if(request.Name == request.DisplayName)
                {
                    ModelState.AddModelError("DisplayName", "The name cannot be the same as display name");
                }
            }

        }
        
    }
}

