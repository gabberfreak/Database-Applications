using AdsDatabase;
using System;
using System.Linq;
using System.Data.Entity;

namespace Show_Data_Related_Tables
{
    class DataTable
    {
        static void Main()
        {
            var context = new AdsEntities();

            //Count of queries = 28
            foreach (var ad in context.Ads)
            {
                Console.WriteLine("{0} - {1} \n Category: {2} \n Town: {3} \n User: {4}\n",
                    ad.Title, ad.AdStatus, ad.Category, ad.Town, ad.AspNetUser);
            }

            //Count of queries = 1
            foreach (var ad in context.Ads
                .Include(a => a.AdStatus)
                .Include(a => a.Category)
                .Include(a => a.Town)
                .Include(a => a.AspNetUser))
            {
                Console.WriteLine("Ad Status: {0}\nCategory: {1}\nTown: {2}\nUser: {3}",
                    ad.AdStatus, ad.Category, ad.Town, ad.AspNetUser);
            }

        }
    }
}
