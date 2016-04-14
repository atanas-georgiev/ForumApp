namespace ForumApp.Mvc.Controllers
{
    using System.Web.Mvc;
    using ForumApp.Mvc.Infrastructure.Mappings;
    using ForumApp.Mvc.Models.Post;
    using ForumApp.Mvc.Models.Shared;
    using ForumApp.Services.Forum;
    using ForumApp.Services.Post;
    
    public class ForumController : Controller
    {
        private readonly IForumService forumService;
        private readonly IPostService postService;

        public ForumController(IForumService forumService, IPostService postService)
        {
            this.postService = postService;
            this.forumService = forumService;
        }

        public ActionResult Index(int id = 1, int page = 1)
        {
            var allPages = this.postService.GetForumPostsAllPagesCount(id);

            if (page > allPages)
            {
                return this.HttpNotFound();
            }

            var posts = this.postService.GetForumPostsOrderedByDate(id, page).To<PostViewModel>();
            var model = new PagableListViewModel<PostViewModel>()
            {
                Data = posts,
                Pages = allPages,
                Page = id
            };

            return this.View(model);
        }
    }
}