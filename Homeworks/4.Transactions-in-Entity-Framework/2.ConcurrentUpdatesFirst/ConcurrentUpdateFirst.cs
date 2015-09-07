using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsDatabase.Data;

namespace ConcurrentUpdates
{
    class ConcurrentUpdateFirst
    {
        static void Main()
        {
            //To run simultaneously two instances of your app you have to configure your solution by this way:
            //Right-Click -> Properties -> Multiple Startup Projects -> and select both projects actions to Start

            var contextFirst = new NewsContext();

            Console.WriteLine("First User");
            Console.WriteLine("Application started.");
            var firstNewsFirstUser = contextFirst.News.First();
            //Step 1
            Console.WriteLine("Text from DB:" + firstNewsFirstUser.Content);
            Console.WriteLine("Enter the corrected text:");
            //Step 2
            string newValue = Console.ReadLine();
            firstNewsFirstUser.Content = newValue;
            Console.WriteLine("User input: " + newValue);
            //Step 3
            contextFirst.SaveChanges();
            Console.WriteLine("Changes succesfully saved in the DB.");
        }
    }
}
