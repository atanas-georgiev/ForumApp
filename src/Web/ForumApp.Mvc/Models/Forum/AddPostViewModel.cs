﻿namespace ForumApp.Mvc.Models.Forum
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using ForumApp.Mvc.Infrastructure.Mappings;

    public class AddPostViewModel : IMapFrom<Data.Models.Post>
    {
        public AddPostViewModel()
        {
            this.CreatedDateTime = DateTime.UtcNow;
        }

        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(1000)]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Text { get; set; }

        [AllowHtml]
        [MaxLength(150)]
        public string Author { get; set; }
        
        public DateTime CreatedDateTime { get; set; }
    }
}