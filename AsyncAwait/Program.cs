using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwait
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("----- Download Data -----");
                Console.WriteLine("1. Download Asynchronous");
                Console.WriteLine("2. Download Parallel Asynchronous");
                int n = int.Parse(Console.ReadLine());
                if (n == 1)
                {
                    MyAppAsync();

                }
                if (n == 2)
                {
                    MyAppParallelAsync();
                }
                Console.ReadLine();
            }

        }

        static async void MyAppAsync()
        {
            int n = int.Parse(Console.ReadLine());
            var watch = System.Diagnostics.Stopwatch.StartNew();
            await BulkDownloadAsync(n);
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            System.Console.WriteLine($"Total execution time {elapsedMs}");
        }

        static async void MyAppParallelAsync()
        {
            int n = int.Parse(Console.ReadLine());
            var watch = System.Diagnostics.Stopwatch.StartNew();
            await BulkDownloadParallelAsync(n);
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            System.Console.WriteLine($"Total execution time {elapsedMs}");
        }

        private static void BulkDownloadSync(int n)
        {
            for (int i = 0; i < n; i++)
            {
                string result = DownloadData();
                Console.WriteLine(result);
            }
        }

        private static async Task BulkDownloadAsync(int n)
        {
            for (int i = 0; i < n; i++)
            {
                string result = await Task.Run(() => DownloadData());
                Console.WriteLine(result);
            }
        }

        private static async Task BulkDownloadParallelAsync(int n)
        {
            List<Task<string>> tasks = new List<Task<string>>();
            for (int i = 0; i < n; i++)
            {
                tasks.Add(Task.Run(() => DownloadData()));
            }
            var results = await Task.WhenAll(tasks);
            foreach (var result in results)
            {
                Console.WriteLine(result);
            }
        }

        private static string DownloadData()
        {
            List<string> dummy = new() { "Anime", "Drakor", "Hollywood", "Indian" };
            Random rnd = new Random();
            for (int i = 0; i < 1000000000; i++)
            {
                // looping doang
            }

            return $"Downloaded content {dummy[rnd.Next(0, 4)]}";
        }
    }
}
