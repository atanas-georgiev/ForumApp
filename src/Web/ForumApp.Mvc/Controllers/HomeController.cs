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

        public ActionResult Index()
        {
            var forums = this.forumService.GetAll().To<ForumListViewModel>();
            return View(forums);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}