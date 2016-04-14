namespace ForumApp.Mvc.Controllers
{
    using System;
    using System.Web.Mvc;
    using ForumApp.Data.Models;
    using ForumApp.Mvc.Infrastructure.Mappings;
    using ForumApp.Mvc.Models.Forum;
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
            this.Session["forumId"] = id;

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