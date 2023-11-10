﻿using System.ComponentModel.DataAnnotations;

namespace Bloggie.Model.Models
{
    public class BlogPostComment
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public Guid BlogPostId { get; set; }
        public Guid UserId { get; set; }
        public DateTime DateAdded { get; set; }

    }
}
