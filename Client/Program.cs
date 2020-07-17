using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        private static readonly HttpClient _client = new HttpClient { BaseAddress = new Uri("https://localhost:5001/api/book/") };

        private async static Task<IEnumerable<string>> GetAll()
        {
            var res = await _client.GetAsync("");

            var books = await res.Content.ReadAsAsync<IEnumerable<Book>>();

            return books.Select(b => b.Title);
        }

        private async static IAsyncEnumerable<string> GetRecommendations(params string[] genres)
        {
            foreach (var genre in genres)
            {
                var res = await _client.GetAsync($"Recommendations/{genre}");

                var books = await res.Content.ReadAsAsync<IEnumerable<Book>>();

                foreach(var b in books)
                {
                    yield return b.Title;
                }
            }
        }
                
        static async Task Main()
        {
            Console.WriteLine("Press return when server ready.");
            Console.ReadLine();

            await foreach(var title in GetRecommendations("Horror","SF"))
                Console.WriteLine(title);

            Console.WriteLine();
            Console.WriteLine("Complete");
            Console.ReadLine();
        }
    }
}
