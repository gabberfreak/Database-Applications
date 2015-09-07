namespace NewsDatabase.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Models;
    using NewsDatabase.Migrations;

    public class NewsContext : DbContext
    {
        public NewsContext()
            : base("name=NewsContext")
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<NewsContext, Configuration>());
        }
        public virtual DbSet<News> News { get; set; }
    }
}