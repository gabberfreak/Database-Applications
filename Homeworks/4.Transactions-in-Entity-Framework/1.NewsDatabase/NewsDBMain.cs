using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsDatabase.Data;
using NewsDatabase.Models;

namespace NewsDatabase
{
    class NewsDBMain
    {
        static void Main()
        {
            var context = new NewsContext();

            Console.WriteLine(context.News.Count());
        }
    }
}
