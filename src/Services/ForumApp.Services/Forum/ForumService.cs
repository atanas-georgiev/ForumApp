namespace ForumApp.Services.Forum
{
    using System;
    using System.Linq;

    using ForumApp.Data.Models;
    using ForumApp.Data.Repositories;

    using ForumApp.Services.Cache;

    public class ForumService : IForumService
    {
        private readonly IRepository<Forum> forums;
        private readonly ICacheService cacheService;

        public ForumService(IRepository<Forum> forums, ICacheService cacheService)
        {
            this.forums = forums;
            this.cacheService = cacheService;
        }

        public IQueryable<Forum> GetByPage(int page)
        {
            return this.cacheService.Get("AllForumsCache" + page, () =>
            {
                return this.forums.All()
                    .OrderBy(x => x.Id)
                    .Skip(Constants.Page.ItemsPerPage * (page - 1))
                    .Take(Constants.Page.ItemsPerPage)
                    .ToList().AsQueryable();
            }, 15 * 60);
        }

        public int GetAllPagesCount()
        {
            return this.cacheService.Get("AllForumsCacheCount", () => (int)Math.Ceiling(this.forums.All().Count() / (decimal)Constants.Page.ItemsPerPage), 15 * 60);            
        }
    }
}
