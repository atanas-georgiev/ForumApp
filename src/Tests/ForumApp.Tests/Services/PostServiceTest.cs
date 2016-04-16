namespace ForumApp.Tests.Services
{
    using System;
    using System.Linq;

    using ForumApp.Data.Models;
    using ForumApp.Services.Post;
    using ForumApp.Tests.Mocks;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PostServiceTest
    {
        private const int DataCount = 100;

        private PostService postService;

        private RepositoryMock<Post> repo;

        [TestMethod]
        public void AddFuctionShouldAddSingleEntryInRepository()
        {
            this.postService.Add(new Post());
            Assert.AreEqual(this.repo.All().Count(), DataCount + 1);
        }

        [TestMethod]
        public void GetByPageShouldReturnCorrectValue()
        {
            var data = this.postService.GetForumPostsOrderedByDate(1, 1);
            Assert.AreEqual(data.Count(), Constants.Page.ItemsPerPage);

            for (int i = 0; i < Constants.Page.ItemsPerPage; i++)
            {
                Assert.AreEqual(data.ToList()[i].Text, "Text" + i);
            }
        }

        [TestMethod]
        public void GetForumPostsAllPagesCountShouldReturnCorrectValue()
        {
            var count = this.postService.GetForumPostsAllPagesCount(1);
            Assert.AreEqual(count, (int)Math.Ceiling(DataCount / (decimal)Constants.Page.ItemsPerPage));
        }

        [TestMethod]
        public void GetTitleByIdShouldReturnCorrectValue()
        {
            var title = this.postService.GetTitleById(0);
            Assert.AreEqual(title, "Text" + 0);
        }

        [TestMethod]
        public void GetTitleByIdShouldReturnCorrectValueWithInvalidParameters()
        {
            var title = this.postService.GetTitleById(DataCount + 1);
            Assert.AreEqual(title, string.Empty);
        }

        [TestInitialize]
        public void Init()
        {
            this.repo = new RepositoryMock<Post>();

            for (var i = 0; i < DataCount; i++)
            {
                this.repo.Add(new Post() { Id = i, Text = "Text" + i, CreatedDateTime = DateTime.UtcNow, ForumId = 1 });
            }

            this.postService = new PostService(this.repo, new CacheServiceMock());
        }
    }
}