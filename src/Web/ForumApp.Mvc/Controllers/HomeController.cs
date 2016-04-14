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

        public ActionResult Index(int id = 1)
        {
            var allPages = this.forumService.GetAllPagesCount();

            if (id > allPages)
            {
                return this.HttpNotFound();
            }

            var forums = this.forumService.GetByPage(id).To<ForumViewModel>();
            var model = new PagableListViewModel<ForumViewModel>()
            {
                Data = forums,
                Pages = allPages,
                Page = id
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