namespace ForumApp.Tests.Controllers
{
    using System.Reflection;

    using ForumApp.Mvc;
    using ForumApp.Mvc.Controllers;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using TestStack.FluentMVCTesting;

    [TestClass]
    public class ErrorControllerTest
    {
        private AutoMapperConfig autoMapperConfig;

        private ErrorController controller;

        [TestInitialize]
        public void Init()
        {
            this.autoMapperConfig = new AutoMapperConfig();
            this.autoMapperConfig.Execute(Assembly.Load("ForumApp.Mvc"));

            this.controller = new ErrorController();
        }

        [TestMethod]
        public void NotFoundShouldReturnCorrectView()
        {
            this.controller.WithCallTo(c => c.NotFound()).ShouldRenderDefaultView();
        }
    }
}