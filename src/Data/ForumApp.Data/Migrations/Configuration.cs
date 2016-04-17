namespace ForumApp.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;

    using ForumApp.Data.Models;

    public sealed class Configuration : DbMigrationsConfiguration<ForumAppDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true; // todo: remove in production
        }

        protected override void Seed(ForumApp.Data.ForumAppDbContext context)
        {
            if (!context.Forums.Any())
            {
                this.AddForums(context);
            }            
        }

        private void AddForums(ForumAppDbContext context)
        {
            context.Forums.Add(new Forum() { Title = "Lorem Ipsum" });
            context.Forums.Add(new Forum() { Title = "Телерик" });
            context.Forums.Add(new Forum() { Title = "Some staff" });
            context.Forums.Add(new Forum() { Title = "C# Discussions" });
            context.Forums.Add(new Forum() { Title = "ASP.NET" });
            context.Forums.Add(new Forum() { Title = "Free time" });
            context.Forums.Add(new Forum() { Title = "Test Projects" });
            context.Forums.Add(new Forum() { Title = "General Discussions" });
            context.Forums.Add(new Forum() { Title = "Catsss" });
            context.Forums.Add(new Forum() { Title = "Pesho" });
            context.Forums.Add(new Forum() { Title = "Gosho" });
            context.Forums.Add(new Forum() { Title = "Глупости" });
            context.SaveChanges();
        }
    }
}
