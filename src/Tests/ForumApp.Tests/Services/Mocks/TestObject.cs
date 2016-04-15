namespace ForumApp.Tests.Services.Mocks
{
    public class TestObject
    {
        public readonly object SyncRoot = new object();

        public int Data { get; set; }
    }
}
