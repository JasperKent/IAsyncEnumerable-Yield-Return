using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsyncCollections
{
    class Program
    {
        static async IAsyncEnumerable<int> CreateAsyncList()
        {
            for (int i = 0; i < 10; i++)
            {
                await Task.Delay(1000);  // Simulate delay

                yield return i;
            }
        }

        static async Task<IEnumerable<int>> CreateListAsync()
        {
            List<int> items = new List<int>();

            for (int i = 0; i < 10; i++)
            {
                await Task.Delay(1000);  // Simulate delay
                items.Add(i);
            }
            return items;
        }

        static async Task ShowAsyncList()
        {
            await foreach(var item in CreateAsyncList())
                Console.WriteLine($"{DateTime.Now.ToLongTimeString()}: {item}");
        }

        static async Task ShowListAsync()
        {
            foreach (var item in await CreateListAsync())
                Console.WriteLine($"{DateTime.Now.ToLongTimeString()}: {item}");
        }


        static void Main()
        {
            Console.WriteLine("Created Asynchronous List");
            ShowAsyncList();

            Console.WriteLine("Doing something else ...");
            Console.ReadLine();
        }
    }
}
