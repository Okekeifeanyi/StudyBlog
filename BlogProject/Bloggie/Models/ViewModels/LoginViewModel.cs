﻿using System.ComponentModel.DataAnnotations;

namespace Bloggie.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "Password has to be atleast minimum of 6 characters")]
        public string Password { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
