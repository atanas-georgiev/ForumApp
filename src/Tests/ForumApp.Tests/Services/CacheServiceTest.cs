namespace ForumApp.Tests.Services
{
    using ForumApp.Services.Cache;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CacheServiceTest
    {
        private CacheService cacheService;

        [TestMethod]
        public void CacheSeriveShouldReallyCacheData()
        {
            var data = this.cacheService.Get("test", () => 1, 1);
            this.cacheService.Remove("test");
            Assert.AreEqual(data, 1);
        }

        // private TestObject testObject;
        [TestInitialize]
        public void Init()
        {
            this.cacheService = new CacheService();
        }
    }
}