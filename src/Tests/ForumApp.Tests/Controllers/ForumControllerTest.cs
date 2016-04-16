namespace ForumApp.Tests.Controllers
{
    using System.Reflection;
    using System.Web;
    using System.Web.Mvc;

    using ForumApp.Data.Models;
    using ForumApp.Mvc;
    using ForumApp.Mvc.Controllers;
    using ForumApp.Mvc.Models.Forum;
    using ForumApp.Mvc.Models.Post;
    using ForumApp.Mvc.Models.Shared;
    using ForumApp.Services.Forum;
    using ForumApp.Services.Post;
    using ForumApp.Tests.Mocks;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using TestStack.FluentMVCTesting;

    [TestClass]
    public class ForumControllerTest
    {
        private const int DataCount = 100;

        private AutoMapperConfig autoMapperConfig;

        private ForumController controller;

        [TestMethod]
        public void AddPostGetShouldReturnCorrectPartialView()
        {
            this.controller.WithCallTo(c => c.AddPost()).ShouldRenderPartialView("_AddPost");
        }

        [TestMethod]
        public void AddPostGetShouldReturnCorrectPartialViewIfErrorInModelState()
        {
            this.controller.ModelState.AddModelError("test", "error");
            this.controller.WithCallTo(c => c.AddPost(new AddPostViewModel()))
                .ShouldRenderPartialView("_AddPost")
                .WithModel<AddPostViewModel>();
        }

        [TestMethod]
        public void AddPostPostShouldRedirectIfNoErrorInModelState()
        {
            this.controller.WithCallTo(c => c.AddPost(new AddPostViewModel())).ShouldRedirectToRoute(string.Empty);
        }

        [TestMethod]
        public void IndexShouldReturnCorrectViewAndModelWithValidPage()
        {
            this.controller.WithCallTo(c => c.Index(1, 1))
                .ShouldRenderDefaultView()
                .WithModel<PagableListViewModel<PostViewModel>>();
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

            var repo = new RepositoryMock<Post>();

            for (int i = 0; i < DataCount; i++)
            {
                repo.Add(new Post() { Id = i, Text = "Text" + i, ForumId = 1 });
            }

            var repo2 = new RepositoryMock<Forum>();

            for (int i = 0; i < DataCount; i++)
            {
                repo2.Add(new Forum() { Id = i, Title = "Title" + i });
            }

            this.controller = new ForumController(
                new PostService(repo, new CacheServiceMock()), 
                new ForumService(repo2, new CacheServiceMock()));

            var context = new Mock<ControllerContext>();
            var session = new Mock<HttpSessionStateBase>();
            session.Setup(s => s["forumId"]).Returns(1);
            context.Setup(m => m.HttpContext.Session).Returns(session.Object);
            this.controller.ControllerContext = context.Object;
        }
    }
}