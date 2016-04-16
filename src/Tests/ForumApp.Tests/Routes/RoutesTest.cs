namespace ForumApp.Tests.Routes
{
    using System.Web.Routing;

    using ForumApp.Mvc;
    using ForumApp.Mvc.Controllers;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using MvcRouteTester;

    [TestClass]
    public class RoutesTest
    {
        private RouteCollection routeCollection;

        [TestMethod]
        public void HomeIndexWithParamsShouldPointToCorrectRoute()
        {
            this.routeCollection.ShouldMap("/Home/Index/5/5").To<HomeController>(x => x.Index(5, 5));
        }

        [TestMethod]
        public void ForumIndexWithParamsShouldPointToCorrectRoute()
        {
            this.routeCollection.ShouldMap("/Forum/Index/5/5").To<ForumController>(x => x.Index(5, 5));
        }

        [TestMethod]
        public void ForumAddPostShouldPointToCorrectRoute()
        {
            this.routeCollection.ShouldMap("/Forum/AddPost").To<ForumController>(x => x.AddPost());
        }

        [TestMethod]
        public void PostIndexWithParamsShouldPointToCorrectRoute()
        {
            this.routeCollection.ShouldMap("/Post/Index/5/5").To<PostController>(x => x.Index(5, 5));
        }

        [TestMethod]
        public void PostAddCommentShouldPointToCorrectRoute()
        {
            this.routeCollection.ShouldMap("/Post/AddComment").To<PostController>(x => x.AddComment());
        }

        [TestMethod]
        public void ErrorNotFoundShouldPointToCorrectRoute()
        {
            this.routeCollection.ShouldMap("/Error/NotFound").To<ErrorController>(x => x.NotFound());
        }

        [TestInitialize]
        public void Init()
        {
            this.routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(this.routeCollection);
        }
    }
}