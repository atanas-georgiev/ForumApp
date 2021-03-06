﻿namespace ForumApp.Mvc.Models.Comment
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using ForumApp.Data.Models;
    using ForumApp.Mvc.Infrastructure.Mappings;

    public class CommentViewModel : IMapFrom<Comment>
    {
        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(1000)]
        public string Text { get; set; }

        [MaxLength(150)]
        public string Author { get; set; }

        [Required]
        public DateTime CreatedDateTime { get; set; }        
    }
}