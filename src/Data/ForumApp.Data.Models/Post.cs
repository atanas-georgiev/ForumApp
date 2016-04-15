namespace ForumApp.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Post
    {
        private ICollection<Comment> comments;

        public Post()
        {
            this.comments = new HashSet<Comment>();
        }

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

        [ForeignKey("ForumId")]
        public virtual Forum Forum { get; set; }

        public virtual int? ForumId { get; set; }

        public virtual ICollection<Comment> Comments
        {
            get
            {
                return this.comments;
            }

            set
            {
                this.comments = value;
            }
        }
    }
}
