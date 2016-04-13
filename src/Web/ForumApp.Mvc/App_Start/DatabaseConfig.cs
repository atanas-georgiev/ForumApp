namespace ForumApp.Mvc
{
    using System.Data.Entity;
    using ForumApp.Data;
    using ForumApp.Data.Migrations;

    public static class DatabaseConfig
    {
        public static void Initialize()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ForumAppDbContext, Configuration>());
        }
    }
}