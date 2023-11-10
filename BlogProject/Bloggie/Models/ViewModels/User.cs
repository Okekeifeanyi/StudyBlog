using System.ComponentModel.DataAnnotations;

namespace Bloggie.Models.ViewModels
{
    public class User
    {
        public Guid Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
