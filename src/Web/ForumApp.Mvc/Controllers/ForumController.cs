namespace ForumApp.Mvc.Controllers
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using ForumApp.Data.Models;
    using ForumApp.Mvc.Infrastructure.Mappings;
    using ForumApp.Mvc.Models.Forum;
    using ForumApp.Mvc.Models.Post;
    using ForumApp.Mvc.Models.Shared;
    using ForumApp.Services.Cache;
    using ForumApp.Services.Forum;
    using ForumApp.Services.Post;
    
    public class ForumController : Controller
    {
        private readonly IForumService forumService;
        private readonly IPostService postService;
        private readonly ICacheService cacheService;

        public ForumController(IForumService forumService, IPostService postService, ICacheService cacheService)
        {
            this.postService = postService;
            this.forumService = forumService;
            this.cacheService = cacheService;
        }

        public ActionResult Index(int page = 1, int id = 1)
        {
            var allPages = this.postService.GetForumPostsAllPagesCount(id);
            var posts = this.postService.GetForumPostsOrderedByDate(id, page).To<PostViewModel>();            

            this.Session["forumId"] = id;

            if (page > allPages)
            {
                return this.HttpNotFound();
            }

            var model = new PagableListViewModel<PostViewModel>()
            {
                Data = posts,
                Pages = allPages,
                Page = page,
                ParentId = id
            };

            return this.View(model);
        }

        public ActionResult AddPost()
        {
            return this.PartialView("_AddPost", new AddPostViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPost(AddPostViewModel model)
        {
            var forumId = int.Parse(this.Session["forumId"].ToString());

            if (this.ModelState.IsValid)
            {                
                var postDb = new Post()
                {
                    CreatedDateTime = DateTime.UtcNow,
                    Author = model.Author ?? "anonymous",
                    Text = model.Text,
                    ForumId = forumId
                };

                this.postService.Add(postDb);

                return this.RedirectToAction("AddPost");
            }

            return this.PartialView("_AddPost", model);            
        }
    }
}