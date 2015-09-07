namespace ProductsShop.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using ProductsShop.Models;
    using System.Data.Entity.Migrations;
    using ProductsShop.Migrations;

    public class ProductsShopContext : DbContext
    {
        public ProductsShopContext()
            : base("name=ProductsShopContext")
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<ProductsShopContext, Configuration>());
            //Database.SetInitializer(
            //    new DropCreateDatabaseAlways<ProductsShopContext>());
        }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithMany(p => p.Categories)
                .Map(m =>
                {
                    m.MapLeftKey("Category_Id");
                    m.MapRightKey("Product_Id");
                    m.ToTable("CategoryProducts");
                });

            modelBuilder.Entity<User>()
                .HasMany(u => u.Friends)
                .WithMany()
                .Map(m =>
                {
                    m.MapLeftKey("UserId");
                    m.MapRightKey("FriendId");
                    m.ToTable("UserFriends");
                });

            modelBuilder.Entity<User>()
                .HasMany(u => u.ProductsBought)
                .WithOptional(p => p.Buyer)
                .HasForeignKey(u => u.BuyerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(u => u.ProductsSold)
                .WithRequired(p => p.Seller)
                .HasForeignKey(u => u.SellerId)
                .WillCascadeOnDelete(false);
        }
    }
}