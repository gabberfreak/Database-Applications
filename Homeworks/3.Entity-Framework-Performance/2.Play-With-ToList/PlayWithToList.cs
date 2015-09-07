using System;
using System.Linq;
using System.Diagnostics;
using AdsDatabase;

namespace Play_With_ToList
{
    class PlayWithToList
    {
        static void Main()
        {
            var context = new AdsEntities();
            //This neutralize the first time connect to base that takes time
            context.Ads.Count();

            double sum = 0;
            int runs = 1;

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Run #{0}", runs++);
                NonOptimized(context);
                double time = stopwatch.Elapsed.TotalSeconds;
                Console.WriteLine("{0:F4}", time);
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
                Optimized(context);
                double time = stopwatch.Elapsed.TotalSeconds;
                Console.WriteLine("{0:F4}", time);
                sum2 += time;
            }

            Console.WriteLine("Average time optimized: {0:F4}", sum2 / 10);
        }

        private static void NonOptimized(AdsEntities context)
        {

            var allAds = context.Ads
                .ToList()
                .Where(a => a.AdStatus.Status == "Published")
                .Select(a => new
                {
                    a.Title,
                    Category = a.Category != null ? a.Category.Name : null,
                    Town = a.Town != null ? a.Town.Name : null,
                    a.Date
                })
                .ToList()
                .OrderBy(a => a.Date);
        }

        private static void Optimized(AdsEntities context)
        {
             var allAds = context.Ads
                .Where(a => a.AdStatus.Status == "Published")
                .OrderBy(a => a.Date)
                .Select(a => new
                {
                    a.Title,
                    Category = a.Category != null ? a.Category.Name : null,
                    Town = a.Town != null ? a.Town.Name : null,
                    a.Date
                });
        }
    }
}
