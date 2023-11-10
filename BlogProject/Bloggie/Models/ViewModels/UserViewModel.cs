using System.ComponentModel.DataAnnotations;

namespace Bloggie.Models.ViewModels
{
    public class UserViewModel
    {
        public List<User> Users { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        public bool AdminRoleCheckBox { get; set; }
    }
}
