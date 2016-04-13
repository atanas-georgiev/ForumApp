namespace ForumApp.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Forum
    {
        private ICollection<Post> posts;

        public Forum()
        {
            this.posts = new HashSet<Post>();
        }

        [Key]
        public int Id { get; set; }

        [MinLength(5)]
        [MaxLength(150)]
        public string Title { get; set; }
        
        public virtual ICollection<Post> Posts
        {
            get
            {
                return this.posts;
            }

            set
            {
                this.posts = value;
            }
        }
    }
}
