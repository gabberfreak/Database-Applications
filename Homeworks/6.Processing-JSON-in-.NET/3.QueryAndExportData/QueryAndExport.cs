using System;
using System.Collections.Generic;
using System.Linq;
using ProductsShop.Data;
using System.Web.Script.Serialization;
using System.IO;
using System.Xml.Linq;


namespace QueryAndExportData
{
    class QueryAndExport
    {
        static void Main()
        {
            var context = new ProductsShopContext();

            //1.Products in range

            var productsInRange = context.Products
                .Where(p => p.Price >= 500 && p.Price <= 1000 && p.Buyer == null)
                .OrderBy(p => p.Price)
                .Select(p => new
                {
                    name = p.Name,
                    price = p.Price,
                    seller = (p.Seller.FirstName != null ? p.Seller.FirstName + " " : "") + p.Seller.LastName
                })
                .ToList();

            foreach (var product in productsInRange)
            {
                Console.WriteLine("Product name: " + product.name);
                Console.WriteLine("Product price: " + product.price);
                Console.WriteLine("Seller: " + product.seller);
            }

            var json = new JavaScriptSerializer().Serialize(productsInRange);
            File.WriteAllText(@"../../products-in-range.json", json);

            //2.Successfully Sold Products

            var userSoldProducts = context.Users
                .Where(u => u.ProductsSold.Count >= 1 && u.ProductsSold.Any(p => p.BuyerId != null))
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Select(u => new
                {
                    firstName = u.FirstName != null ? u.FirstName : null,
                    lastName = u.LastName,
                    soldProducts = u.ProductsSold
                        .Select(p => new
                        {
                            name = p.Name,
                            price = p.Price,
                            buyerFirstName = p.Buyer.FirstName != null ? p.Buyer.FirstName : null,
                            buyerLastName = p.Buyer.LastName
                        })
                })
                .ToList();

            var json2 = new JavaScriptSerializer().Serialize(userSoldProducts);
            File.WriteAllText(@"../../users-sold-products.json", json2);

            //3.Categories By Products Count

            var categoriesByProducts = context.Categories
                .OrderByDescending(c => c.Products.Count)
                .Select(c => new
                {
                    category = c.Name,
                    productsCount = c.Products.Count,
                    averagePrice = c.Products.Average(p => p.Price),
                    totalRevenue = c.Products.Sum(p => p.Price)
                })
                .ToList();

            var json3 = new JavaScriptSerializer().Serialize(categoriesByProducts);
            File.WriteAllText(@"../../categories-by-products.json", json3);

            //4.Users and Products

            var usersAndProducts = context.Users
                .Where(u => u.ProductsSold.Count >= 1)
                .OrderByDescending(u => u.ProductsSold.Count)
                .ThenBy(u => u.LastName)
                .Select(u => new
                {
                    u.FirstName,
                    u.LastName,
                    u.Age,
                    SoldProducts = u.ProductsSold
                        .Select(p => new
                        {
                            p.Name,
                            p.Price
                        })
                })
                .ToList();

            var xmlRoot = new XElement("users");
            xmlRoot.Add(new XAttribute("count", usersAndProducts.Count));
            foreach (var user in usersAndProducts)
            {
                var userXml = new XElement("user");
                if (user.FirstName != null)
                {
                    userXml.Add(new XAttribute("first-name", user.FirstName));
                }
                userXml.Add(new XAttribute("last-name", user.LastName));
                if (user.Age != null)
                {
                    userXml.Add(new XAttribute("age", user.Age));
                }

                var productsXml = new XElement("sold-products",
                    new XAttribute("count", user.SoldProducts.Count()));
                foreach (var product in user.SoldProducts)
                {
                    productsXml.Add(new XElement("product",
                        new XAttribute("name", product.Name),
                        new XAttribute("price", product.Price)));
                }
                userXml.Add(productsXml);
                xmlRoot.Add(userXml);
            }
            var xmlDoc = new XDocument(xmlRoot);
            xmlDoc.Save("../../users-and-products.xml");
        }
    }
}
