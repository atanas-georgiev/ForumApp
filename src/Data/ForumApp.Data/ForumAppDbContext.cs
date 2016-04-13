using System.Data.Entity;
using ForumApp.Data.Models;

namespace ForumApp.Data
{
    public class ForumAppDbContext : DbContext, IForumAppDbContext
    {
        public IDbSet<Forum> Forums { get; set; }

        public IDbSet<Post> Posts { get; set; }

        public IDbSet<Comment> Comments { get; set; }

        public ForumAppDbContext()
            : base("ForumAppDb")
        {
        }    
    }
}