namespace ForumApp.Tests.Controllers
{
    using System.Reflection;
    using System.Web;
    using System.Web.Mvc;

    using ForumApp.Data.Models;
    using ForumApp.Mvc;
    using ForumApp.Mvc.Controllers;
    using ForumApp.Mvc.Models.Comment;
    using ForumApp.Mvc.Models.Shared;
    using ForumApp.Services.Comment;
    using ForumApp.Services.Post;
    using ForumApp.Tests.Mocks;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using TestStack.FluentMVCTesting;

    [TestClass]
    public class PostControllerTest
    {
        private const int DataCount = 100;

        private AutoMapperConfig autoMapperConfig;

        private PostController controller;

        [TestMethod]
        public void AddCommentGetShouldReturnCorrectPartialView()
        {
            this.controller.WithCallTo(c => c.AddComment()).ShouldRenderPartialView("_AddComment");
        }

        [TestMethod]
        public void AddCommentGetShouldReturnCorrectPartialViewIfErrorInModelState()
        {
            this.controller.ModelState.AddModelError("test", "error");
            this.controller.WithCallTo(c => c.AddComment(new AddCommentViewModel()))
                .ShouldRenderPartialView("_AddComment")
                .WithModel<AddCommentViewModel>();
        }

        [TestMethod]
        public void AddCommentPostShouldRedirectIfNoErrorInModelState()
        {
            this.controller.WithCallTo(c => c.AddComment(new AddCommentViewModel())).ShouldRedirectToRoute(string.Empty);
        }

        [TestMethod]
        public void IndexShouldReturnCorrectViewAndModelWithValidPage()
        {
            this.controller.WithCallTo(c => c.Index(1, 1))
                .ShouldRenderDefaultView()
                .WithModel<PagableListViewModel<CommentViewModel>>();
        }

        [TestMethod]
        public void IndexShouldReturnValidStatusCodeWithInvalidId()
        {
            this.controller.WithCallTo(c => c.Index(0, DataCount + 1)).ShouldGiveHttpStatus(404);
        }

        [TestMethod]
        public void IndexShouldReturnValidStatusCodeWithInvalidPage()
        {
            this.controller.WithCallTo(c => c.Index(DataCount, 1)).ShouldGiveHttpStatus(404);
        }

        [TestInitialize]
        public void Init()
        {
            this.autoMapperConfig = new AutoMapperConfig();
            this.autoMapperConfig.Execute(Assembly.Load("ForumApp.Mvc"));

            var repo = new RepositoryMock<Comment>();

            for (int i = 0; i < DataCount; i++)
            {
                repo.Add(new Comment() { Id = i, Text = "Text" + i, PostId = 1 });
            }

            var repo2 = new RepositoryMock<Post>();

            for (int i = 0; i < DataCount; i++)
            {
                repo2.Add(new Post() { Id = i, Text = "Text" + i });
            }

            this.controller = new PostController(
                new CommentService(repo, new CacheServiceMock()), 
                new PostService(repo2, new CacheServiceMock()));

            var context = new Mock<ControllerContext>();
            var session = new Mock<HttpSessionStateBase>();
            session.Setup(s => s["postId"]).Returns(1);
            context.Setup(m => m.HttpContext.Session).Returns(session.Object);
            this.controller.ControllerContext = context.Object;
        }
    }
}