using System;
using System.Collections.Generic;
using System.Linq;
using ProductsShop.Data;
using ProductsShop.Models;
using System.Xml;
using System.IO;
using System.Web.Script.Serialization;

namespace SeedDatabase
{
    class SeedDatabase
    {
        static void Main()
        {
            var context = new ProductsShopContext();

            SeedUsers(context);

            Random rnd = new Random();

            SeedProducts(context, rnd);

            SeedCategories(context, rnd);
        }

        private static void SeedCategories(ProductsShopContext context, Random rnd)
        {
            var json = File.ReadAllText(@"../../SeedData/categories.json");
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var categories = serializer.Deserialize<CategoryDTO[]>(json);

            var products = context.Products.ToArray();

            foreach (var category in categories)
            {
                Category currCategory = new Category()
                {
                    Name = category.Name
                };

                context.Categories.Add(currCategory);
                context.SaveChanges();
            }

            var allCategories = context.Categories.ToArray();

            for (int i = 0; i < products.Length * 2; i++)
            {
                int productIndex = rnd.Next(0, products.Length);
                var product = products[productIndex];

                var categoryIndex = rnd.Next(0, allCategories.Length);
                var category = allCategories[categoryIndex];

                product.Categories.Add(category);
                context.SaveChanges();
            }
        }

        private static void SeedProducts(ProductsShopContext context, Random rnd)
        {
            var json = File.ReadAllText(@"../../SeedData/products.json");
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var products = serializer.Deserialize<ProductDTO[]>(json);

            var buyers = context.Users.ToArray();
            var sellers = context.Users.ToArray();

            foreach (var product in products)
            {

                int buyerIndex = rnd.Next(1, buyers.Length);
                var buyer = buyers[buyerIndex];

                int sellerIndex = rnd.Next(1, sellers.Length);
                var seller = sellers[sellerIndex];

                Product newProduct = new Product()
                {
                    Name = product.Name,
                    Price = product.Price,
                    Seller = seller
                };

                if (buyerIndex != sellerIndex)
                {
                    newProduct.Buyer = buyer;
                }

                context.Products.Add(newProduct);
                context.SaveChanges();
            }
        }

        private static void SeedUsers(ProductsShopContext context)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"../../SeedData/users.xml");

            var root = doc.DocumentElement;
            foreach (XmlNode node in root.ChildNodes)
            {
                string firstName = null;
                string lastName = node.Attributes["last-name"].Value;

                if (node.Attributes["first-name"] != null)
                {
                    firstName = node.Attributes["first-name"].Value;
                }

                User newUser = new User()
                {
                    FirstName = firstName,
                    LastName = lastName,
                };

                if (node.Attributes["age"] != null)
                {
                    newUser.Age = int.Parse(node.Attributes["age"].Value);
                }

                context.Users.Add(newUser);
                context.SaveChanges();
            }
        }
    }
}
