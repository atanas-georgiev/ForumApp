using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using ForumApp.Data.Models;

namespace ForumApp.Data
{
    public interface IForumAppDbContext
    {
        IDbSet<Forum> Forums { get; set; }

        IDbSet<Post> Posts { get; set; }

        IDbSet<Comment> Comments { get; set; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        int SaveChanges();

        void Dispose();
    }
}