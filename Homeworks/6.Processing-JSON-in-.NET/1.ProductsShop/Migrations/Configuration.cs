namespace ProductsShop.Migrations
{
    using ProductsShop.Data;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ProductsShopContext>
    {
        public Configuration()
        {
            AutomaticMigrationDataLossAllowed = true;
            AutomaticMigrationsEnabled = true;
            ContextKey = "ProductsShopContext";
        }

        protected override void Seed(ProductsShopContext context)
        {
            
        }
    }
}
