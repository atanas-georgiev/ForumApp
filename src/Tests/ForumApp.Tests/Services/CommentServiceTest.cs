namespace ForumApp.Tests.Services
{
    using System;
    using System.Linq;

    using ForumApp.Data.Models;
    using ForumApp.Services.Comment;
    using ForumApp.Tests.Services.Mocks;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CommentServiceTest
    {
        private const int DataCount = 100;

        private CommentService commentService;
        private RepositoryMock<Comment> repo;

        [TestInitialize]
        public void Init()
        {
            this.repo = new RepositoryMock<Comment>();

            for (var i = 0; i < DataCount; i++)
            {
                this.repo.Add(new Comment()
                {
                    Id = i,
                    Text = "Text" + i,
                    CreatedDateTime = DateTime.UtcNow,
                    PostId = 1
                });
            }

            this.commentService = new CommentService(this.repo, new CacheServiceMock());
        }

        [TestMethod]
        public void GetPostCommentsAllPagesCountShouldReturnCorrectValue()
        {
            var count = this.commentService.GetPostCommentsAllPagesCount(1);
            Assert.AreEqual(count, (int)Math.Ceiling(DataCount / (decimal)Constants.Page.ItemsPerPage));
        }

        [TestMethod]
        public void GetPostCommentsOrderedByDateShouldReturnCorrectValue()
        {
            var data = this.commentService.GetPostCommentsOrderedByDate(1, 1);
            Assert.AreEqual(data.Count(), Constants.Page.ItemsPerPage);

            for (int i = 0; i < Constants.Page.ItemsPerPage; i++)
            {
                Assert.AreEqual(data.ToList()[i].Text, "Text" + i);
            }
        }

        [TestMethod]
        public void AddFuctionShouldAddSingleEntryInRepository()
        {
            this.commentService.Add(new Comment());
            Assert.AreEqual(this.repo.All().Count(), DataCount + 1);
        }
    }
}
