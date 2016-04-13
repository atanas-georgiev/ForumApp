using System.Data.Entity;
using ForumApp.Data.Models;

namespace ForumApp.Data
{
    public class ForumAppDbContext : DbContext, IForumAppDbContext
    {
        public IDbSet<Forum> Forums { get; set; }

        //        public IDbSet<Article> Articles { get; set; }
        //
        //        public IDbSet<Like> Likes { get; set; }
        //
        //        public IDbSet<Comment> Comments { get; set; }
        //
        //        public IDbSet<Category> Categories { get; set; }
        //
        //        public IDbSet<Tag> Tags { get; set; }
        //
        //        public IDbSet<Alert> Alerts { get; set; }

        public ForumAppDbContext()
            : base("ForumAppDb")
        {
        }    
    }
}