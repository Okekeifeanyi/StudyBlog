using Bloggie.Model.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bloggie.Models.ViewModels
{
    public class AddBlogPostRequest
    {
     
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string PageContent { get; set; }
        public string ShortDescription { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishedDtae { get; set; }
        public string Author { get; set; }
        public bool Visible { get; set; }

        //add tags
        public IEnumerable<SelectListItem> Tags { get; set; }
        public string[] SelectedTag{ get; set; } = Array.Empty<string>();

    }
}
