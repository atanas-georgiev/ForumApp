namespace ForumApp.Services.Forum
{
    using System;
    using System.Linq;

    using ForumApp.Data.Models;
    using ForumApp.Data.Repositories;

    public class ForumService : IForumService
    {
        private readonly IRepository<Forum> forums;

        public ForumService(IRepository<Forum> forums)
        {
            this.forums = forums;
        }

        public IQueryable<Forum> GetAll()
        {
            return this.forums.All();
        }

        public IQueryable<Forum> GetByPage(int page)
        {
            return this.forums
                .All() 
                .OrderBy(x => x.Id)                                               
                .Skip(Constants.Page.ItemsPerPage * (page - 1))
                .Take(Constants.Page.ItemsPerPage);
        }

        public int GetAllPagesCount()
        {
            return (int)Math.Ceiling(this.forums.All().Count() / (decimal)Constants.Page.ItemsPerPage);
        }
    }
}
