namespace ForumApp.Services.Comment
{
    using System.Linq;

    using ForumApp.Data.Models;

    public interface ICommentService
    {
        IQueryable<Comment> GetPostCommentsOrderedByDate(int postId, int page);

        int GetPostCommentsAllPagesCount(int postId);

        void Add(Comment comment);
    }
}
