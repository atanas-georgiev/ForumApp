namespace ForumApp.Mvc.Controllers
{
    using System.Web.Mvc;

    using ForumApp.Mvc.Infrastructure.Mappings;
    using ForumApp.Mvc.Models.Forum;
    using ForumApp.Mvc.Models.Shared;
    using ForumApp.Services.Forum;

    public class HomeController : Controller
    {
        private readonly IForumService forumService;

        public HomeController(IForumService forumService)
        {
            this.forumService = forumService;
        }

        public ActionResult Index(int page = 1, int id = 0)
        {
            var allPages = this.forumService.GetAllPagesCount();
            var forums = this.forumService.GetByPage(page).To<ForumViewModel>();
            
            if (page > allPages)
            {
                return this.HttpNotFound();
            }
            
            var model = new PagableListViewModel<ForumViewModel>()
            {
                Data = forums,
                Pages = allPages,
                Page = page,
                ParentId = 0
            };

            return this.View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return this.View();
        }
    }
}