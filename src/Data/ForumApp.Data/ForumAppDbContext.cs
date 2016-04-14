namespace ForumApp.Data
{
    using System.Data.Entity;
    using ForumApp.Data.Models;

    public class ForumAppDbContext : DbContext, IForumAppDbContext
    {
        public ForumAppDbContext()
            : base("ForumAppDb")
        {
        }

        public IDbSet<Forum> Forums { get; set; }

        public IDbSet<Post> Posts { get; set; }

        public IDbSet<Comment> Comments { get; set; } 
    }
}