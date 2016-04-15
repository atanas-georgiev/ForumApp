namespace ForumApp.Tests.Services
{
    using ForumApp.Services.Cache;
    using ForumApp.Tests.Services.Mocks;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CacheServiceTest
    {
        private CacheService cacheService;

        //private TestObject testObject;
        
        [TestInitialize]
        public void Init()
        {
            this.cacheService = new CacheService();
        }

        [TestMethod]
        public void CacheSeriveShouldReallyCacheData()
        {
           // var data = this.cacheService.Get("test", () => new TestObject(), 1);

            //lock()
          //  Assert.AreEqual(data, 1);
        }
    }
}
