namespace ForumApp.Tests.Services.Mocks
{
    using System;
    using ForumApp.Services.Cache;

    public class CacheServiceMock : ICacheService
    {
        public T Get<T>(string itemName, Func<T> getDataFunc, int durationInSeconds)
        {
            return getDataFunc();
        }

        public void Remove(string itemName)
        {
            return;
        }
    }
}
