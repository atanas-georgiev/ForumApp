namespace ForumApp.Services.Forum
{
    using System.Linq;

    using ForumApp.Data.Models;
    using ForumApp.Data.Repositories;

    public class PostService : IPostService
    {
        private IRepository<Post> posts;

        public PostService(IRepository<Post> posts)
        {
            this.posts = posts;
        }

        public IQueryable<Post> GetAll()
        {
            return this.posts.All();
        }
    }
}
