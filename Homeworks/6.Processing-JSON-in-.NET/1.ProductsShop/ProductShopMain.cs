using System;
using System.Linq;
using ProductsShop.Data;

namespace ProductsShop
{
    class ProductShopMain
    {
        static void Main()
        {
            var context = new ProductsShopContext();

            Console.WriteLine(context.Users.Count());
        }
    }
}
