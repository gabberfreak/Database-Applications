namespace NewsDatabase.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using NewsDatabase.Data;
    using NewsDatabase.Models;


    internal sealed class Configuration : DbMigrationsConfiguration<NewsContext>
    {
        public Configuration()
        {
            AutomaticMigrationDataLossAllowed = true;
            AutomaticMigrationsEnabled = true;
            ContextKey = "NewsDatabase.Data.NewsContext";
        }

        protected override void Seed(NewsContext context)
        {
            if (context.News.Count() == 0)
            {
                LoadNews(context);
            }
        }

        private void LoadNews(NewsContext context)
        {
            context.News.AddOrUpdate(new News()
            {
                Content = "New Conference this week"
            });

            context.News.AddOrUpdate(new News()
            {
                Content = "Programming Basics starts today"
            });

            context.News.AddOrUpdate(new News()
            {
                Content = "Entity Framework new version 7.0"
            });

            context.News.AddOrUpdate(new News()
            {
                Content = "Google has updated their cores"
            });
        }
    }
}
