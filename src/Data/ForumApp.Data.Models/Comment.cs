﻿namespace ForumApp.Data.Models
{
    using System;

    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(1000)]
        public string Text { get; set; }

        [DefaultValue("anonymous")]
        [MaxLength(150)]
        public string Author { get; set; }

        [Required]
        public DateTime CreatedDateTime { get; set; }

        [ForeignKey("PostId")]
        public virtual Post Post { get; set; }

        public virtual int? PostId { get; set; }
    }
}
