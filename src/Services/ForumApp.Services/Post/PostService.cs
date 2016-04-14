namespace ForumApp.Services.Post
{
    using System;
    using System.Linq;
    using ForumApp.Data.Models;
    using ForumApp.Data.Repositories;    

    public class PostService : IPostService
    {
        private readonly IRepository<Post> posts;

        public PostService(IRepository<Post> posts)
        {
            this.posts = posts;
        }

        public IQueryable<Post> GetAll()
        {
            return this.posts.All();
        }

        public IQueryable<Post> GetForumPostsOrderedByDate(int forumId, int page)
        {
            return this.posts
                .All()
                .OrderByDescending(x => x.CreatedDateTime)
                .Skip(Constants.Page.ItemsPerPage * (page - 1))
                .Take(Constants.Page.ItemsPerPage);
        }

        public int GetForumPostsAllPagesCount(int forumId)
        {
            return (int)Math.Ceiling(this.posts.All().Count(x => x.ForumId == forumId) / (decimal)Constants.Page.ItemsPerPage);
        }

        public void Add(Post post)
        {
            this.posts.Add(post);
            this.posts.SaveChanges();
        }
    }
}
