using System.ComponentModel.DataAnnotations;

namespace Bloggie.Models.ViewModels
{
    public class AdminTagRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string DisplayName { get; set; }
    }
}
