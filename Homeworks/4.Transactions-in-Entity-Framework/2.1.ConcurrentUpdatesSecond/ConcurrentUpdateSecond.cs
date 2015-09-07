using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsDatabase.Data;
using System.Data.Entity.Infrastructure;

namespace ConcurrentUpdatesSecond
{
    class ConcurrentUpdateSecond
    {
        static void Main()
        {
            //To run simultaneously two instances of your app you have to configure your solution by this way:
            //Right-Click -> Properties -> Multiple Startup Projects -> and select both projects actions to Start

            var contextSecond = new NewsContext();
            var contextFirst = new NewsContext();

            Console.WriteLine("Second User");
            Console.WriteLine("Application started.");
            var firstNewsSecondUser = contextSecond.News.First();
            Console.WriteLine("Text from DB:" + firstNewsSecondUser.Content);
            Console.WriteLine("Enter the corrected text:");
            string newValue2 = Console.ReadLine();
            firstNewsSecondUser.Content = newValue2;
            Console.WriteLine("User input: " + newValue2);

            try
            {
                contextSecond.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var firstContent = contextFirst.News.First();
                Console.WriteLine("Conflict! Text from DB: " + firstContent.Content);
                Console.WriteLine("Enter the corrected text:");

                string newValue = Console.ReadLine();
                firstContent.Content = newValue;
                Console.WriteLine("User input: " + newValue);
                contextFirst.SaveChanges();
            }

            Console.WriteLine("Changes successfully saved in the DB.");

        }
    }
}
