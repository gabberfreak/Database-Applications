using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdsDatabase;
using System.Diagnostics;

namespace Select_Everything_And_Certain_Columns
{
    class SelectEverythingAndCertain
    {
        static void Main()
        {
            var context = new AdsEntities();
            //This neutralize the first time connect to base that takes time
            context.Ads.Count();
            context.Database.ExecuteSqlCommand("CHECKPOINT");
            context.Database.ExecuteSqlCommand("DBCC DROPCLEANBUFFERS");

            double sum = 0;
            int runs = 1;

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Run #{0}", runs++);
                SelectEverything(context);
                double time = stopwatch.Elapsed.TotalSeconds;
                Console.WriteLine("----{0:F4}", time);
                sum += time;
            }

            Console.WriteLine("Average time non-optimized: {0:F4}", sum / 10);
            Console.WriteLine();

            runs = 1;
            double sum2 = 0;

            stopwatch.Restart();

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Run #{0}", runs++);
                SelectCertainColumn(context);
                double time = stopwatch.Elapsed.TotalSeconds;
                Console.WriteLine("----{0:F4}", time);
                sum2 += time;
            }

            Console.WriteLine("Average time optimized: {0:F4}", sum2 / 10);
        }

        public static void SelectEverything(AdsEntities context)
        {
            var testQuery = context.Ads.Select(a => a);

            foreach (var ad in testQuery)
            {
                Console.WriteLine(ad.Title);
            }
        }

        public static void SelectCertainColumn(AdsEntities context)
        {
            var testQuery = context.Ads.Select(a => a.Title);

            foreach (var ad in testQuery)
            {
                Console.WriteLine(ad);
            }
        }
    }
}
