namespace ForumApp.Services.Post
{
    using System.Linq;

    using ForumApp.Data.Models;

    public interface IPostService
    {
        IQueryable<Post> GetForumPostsOrderedByDate(int forumId, int page);

        int GetForumPostsAllPagesCount(int forumId);

        string GetTitleById(int id);

        void Add(Post post);
    }
}
