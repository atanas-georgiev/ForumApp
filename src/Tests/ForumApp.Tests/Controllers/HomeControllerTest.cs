namespace ForumApp.Tests.Controllers
{
    using System.Reflection;

    using ForumApp.Data.Models;
    using ForumApp.Mvc;
    using ForumApp.Mvc.Controllers;
    using ForumApp.Mvc.Models.Forum;
    using ForumApp.Mvc.Models.Shared;
    using ForumApp.Services.Forum;
    using ForumApp.Tests.Mocks;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using TestStack.FluentMVCTesting;

    [TestClass]
    public class HomeControllerTest
    {
        private const int DataCount = 100;

        private AutoMapperConfig autoMapperConfig;

        private HomeController controller;

        [TestMethod]
        public void IndexShouldReturnCorrectViewAndModelWithValidPage()
        {
            this.controller.WithCallTo(c => c.Index(1, 1))
                .ShouldRenderDefaultView()
                .WithModel<PagableListViewModel<ForumViewModel>>();
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

            var repo = new RepositoryMock<Forum>();

            for (int i = 0; i < DataCount; i++)
            {
                repo.Add(new Forum() { Id = i, Title = "Title" + i });
            }

            this.controller = new HomeController(new ForumService(repo, new CacheServiceMock()));
        }
    }
}