namespace ForumApp.Mvc.Controllers
{
    using System.Web.Mvc;

    using ForumApp.Mvc.Infrastructure.Mappings;
    using ForumApp.Mvc.Models.Forum;
    using ForumApp.Services.Forum;

    public class HomeController : Controller
    {
        private readonly IForumService forumService;

        public HomeController(IForumService forumService)
        {
            this.forumService = forumService;
        }

        public ActionResult Index(int page = 1)
        {
            var forums = this.forumService.GetByPage(page).To<ForumViewModel>();
            var model = new ForumListViewModel()
            {
                Forums = forums,
                Pages = this.forumService.GetAllPagesCount(),
                Page = page
            };

            return this.View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return this.View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return this.View();
        }
    }
}