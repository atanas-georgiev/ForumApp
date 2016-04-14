namespace ForumApp.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public sealed class Configuration : DbMigrationsConfiguration<ForumAppDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true; // todo: remove in production
        }

        protected override void Seed(ForumApp.Data.ForumAppDbContext context)
        {
            // todo   
        }
    }
}
