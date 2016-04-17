namespace ForumApp.Services.Comment
{
    using System;
    using System.Linq;
    using ForumApp.Data.Models;
    using ForumApp.Data.Repositories;
    using ForumApp.Services.Cache;

    using Ganss.XSS;

    public class CommentService : ICommentService
    {
        private readonly IRepository<Comment> comments;
        private readonly ICacheService cacheService;

        public CommentService(IRepository<Comment> comments, ICacheService cacheService)
        {
            this.comments = comments;
            this.cacheService = cacheService;
        }

        public IQueryable<Comment> GetPostCommentsOrderedByDate(int postId, int page)
        {
            return this.cacheService.Get(
            "AllCommentsCache" + postId + "_" + page, 
            () =>
            {
                return this.comments.All()
                    .Where(x => x.PostId == postId)
                    .OrderBy(x => x.CreatedDateTime)
                    .Skip(Constants.Page.ItemsPerPage * (page - 1))
                    .Take(Constants.Page.ItemsPerPage)
                    .ToList().AsQueryable();
            }, 
            ForumApp.Constants.Cache.Timeout);
        }

        public int GetPostCommentsAllPagesCount(int postId)
        {
            return this.cacheService.Get(
                "AllCommentsCacheCount" + postId,
                () => (int)Math.Ceiling(this.comments.All().Count(x => x.PostId == postId) / (decimal)Constants.Page.ItemsPerPage),
                ForumApp.Constants.Cache.Timeout);
        }

        public void Add(Comment comment)
        {
            var sanitizer = new HtmlSanitizer();

            if (comment.Text != null)
            {              
                comment.Text = sanitizer.Sanitize(comment.Text.Replace("\n", "<br />"));
            }

            if (comment.Author != null)
            {
                comment.Author = sanitizer.Sanitize(comment.Author.Replace("\n", "<br />"));
            }

            this.cacheService.Remove("AllCommentsCache" + comment.PostId);
            this.cacheService.Remove("AllCommentsCacheCount" + comment.PostId);
            this.comments.Add(comment);
            this.comments.SaveChanges();
        }
    }
}