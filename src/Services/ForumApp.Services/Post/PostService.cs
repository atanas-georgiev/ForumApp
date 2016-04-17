namespace ForumApp.Services.Post
{
    using System;
    using System.Linq;
    using ForumApp.Data.Models;
    using ForumApp.Data.Repositories;

    using ForumApp.Services.Cache;

    using Ganss.XSS;

    public class PostService : IPostService
    {
        private readonly IRepository<Post> posts;
        private readonly ICacheService cacheService;

        public PostService(IRepository<Post> posts, ICacheService cacheService)
        {
            this.posts = posts;
            this.cacheService = cacheService;
        }
        
        public IQueryable<Post> GetForumPostsOrderedByDate(int forumId, int page)
        {
            return this.cacheService.Get(
            "AllPostsCache" + forumId + "_" + page, 
            () =>
            {
                return this.posts.All()
                    .Where(x => x.ForumId == forumId)
                    .OrderBy(x => x.CreatedDateTime)
                    .Skip(Constants.Page.ItemsPerPage * (page - 1))
                    .Take(Constants.Page.ItemsPerPage)
                    .ToList().AsQueryable();
            }, 
            ForumApp.Constants.Cache.Timeout);
        }

        public int GetForumPostsAllPagesCount(int forumId)
        {
            return this.cacheService.Get("AllPostsCacheCount" + forumId, () => (int)Math.Ceiling(this.posts.All().Count(x => x.ForumId == forumId) / (decimal)Constants.Page.ItemsPerPage), ForumApp.Constants.Cache.Timeout);
        }

        public string GetTitleById(int id)
        {
            return this.cacheService.Get(
                "PostTitle" + id,
                () =>
                {
                    var title = this.posts.All().FirstOrDefault(x => x.Id == id);
                    return title != null ? title.Text : string.Empty;
                },
                ForumApp.Constants.Cache.Timeout);
        }

        public void Add(Post post)
        {
            var sanitizer = new HtmlSanitizer();

            if (post.Text != null)
            {                
                post.Text = sanitizer.Sanitize(post.Text.Replace("\n", "<br />"));
            }

            if (post.Author != null)
            {
                post.Author = sanitizer.Sanitize(post.Author.Replace("\n", "<br />"));
            }

            this.cacheService.Remove("AllPostsCache" + post.ForumId);
            this.cacheService.Remove("AllPostsCacheCount" + post.ForumId);
            this.posts.Add(post);
            this.posts.SaveChanges();
        }
    }
}
