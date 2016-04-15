namespace ForumApp.Mvc.Controllers
{
    using System;
    using System.Web.Mvc;
    using ForumApp.Data.Models;
    using ForumApp.Mvc.Infrastructure.Mappings;
    using ForumApp.Mvc.Models.Comment;
    using ForumApp.Mvc.Models.Shared;    
    using ForumApp.Services.Comment;
    using ForumApp.Services.Post;

    public class PostController : Controller
    {
        private readonly ICommentService commentService;
        private readonly IPostService postService;

        public PostController(ICommentService commentService, IPostService postService)
        {
            this.commentService = commentService;
            this.postService = postService;
        }

        public ActionResult Index(int page = 1, int id = 1)
        {
            var allPages = this.commentService.GetPostCommentsAllPagesCount(id);
            var posts = this.commentService.GetPostCommentsOrderedByDate(id, page).To<CommentViewModel>();
            var title = this.postService.GetTitleById(id);

            this.Session["postId"] = id;

            if ((page > allPages && allPages != 0) || title == string.Empty)
            {
                return this.HttpNotFound();
            }

            var model = new PagableListViewModel<CommentViewModel>()
            {
                Data = posts,
                Pages = allPages,
                Page = page,
                ParentId = id,
                Title = title
            };

            return this.View(model);
        }

        public ActionResult AddComment()
        {
            return this.PartialView("_AddComment", new AddCommentViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddComment(AddCommentViewModel model)
        {
            var postId = int.Parse(this.Session["postId"].ToString());

            if (this.ModelState.IsValid)
            {
                var commentDb = new Comment()
                {
                    CreatedDateTime = DateTime.UtcNow,
                    Author = model.Author ?? "anonymous",
                    Text = model.Text,
                    PostId = postId
                };

                this.commentService.Add(commentDb);

                return this.RedirectToAction("AddComment");
            }

            return this.PartialView("_AddComment", model);
        }
    }
}