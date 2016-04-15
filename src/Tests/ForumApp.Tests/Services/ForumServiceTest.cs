namespace ForumApp.Tests.Services
{
    using System;
    using System.Linq;

    using ForumApp.Data.Models;
    using ForumApp.Services.Forum;
    using ForumApp.Tests.Services.Mocks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    
    [TestClass]
    public class ForumServiceTest
    {
        private const int DataCount = 100;

        private ForumService forumService;
        private RepositoryMock<Forum> repo;
        
        [TestInitialize]
        public void Init()
        {
            this.repo = new RepositoryMock<Forum>();

            for (int i = 0; i < DataCount; i++)
            {
                this.repo.Add(new Forum()
                {
                    Id = i,
                    Title = "Title" + i
                });
            }

            this.forumService = new ForumService(this.repo, new CacheServiceMock());
        }

        [TestMethod]
        public void GetAllPagesCountShouldReturnCorrectValue()
        {
            var count = this.forumService.GetAllPagesCount();
            Assert.AreEqual(count, (int)Math.Ceiling(DataCount / (decimal)Constants.Page.ItemsPerPage));
        }

        [TestMethod]
        public void GetTitleByIdShouldReturnCorrectValue()
        {
            var title = this.forumService.GetTitleById(0);
            Assert.AreEqual(title, "Title" + 0);
        }

        [TestMethod]
        public void GetTitleByIdShouldReturnCorrectValueWithInvalidParameters()
        {
            var title = this.forumService.GetTitleById(DataCount + 1);
            Assert.AreEqual(title, string.Empty);
        }

        [TestMethod]
        public void GetByPageShouldReturnCorrectValue()
        {
            var data = this.forumService.GetByPage(1);
            Assert.AreEqual(data.Count(), Constants.Page.ItemsPerPage);

            for (int i = 0; i < Constants.Page.ItemsPerPage; i++)
            {
                Assert.AreEqual(data.ToList()[i].Title, "Title" + i);
            }
        }
    }
}
