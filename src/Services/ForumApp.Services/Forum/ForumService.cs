namespace ForumApp.Services.Forum
{
    using System.Linq;

    using ForumApp.Data.Models;
    using ForumApp.Data.Repositories;

    public class ForumService : IForumService
    {
        private IRepository<Forum> forums;

        public ForumService(IRepository<Forum> forums)
        {
            this.forums = forums;
        }

        public IQueryable<Forum> GetAll()
        {
            return this.forums.All();
        }
    }
}
