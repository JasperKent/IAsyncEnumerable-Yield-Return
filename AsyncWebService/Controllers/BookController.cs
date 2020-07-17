using System.Collections.Generic;
using System.Linq;
using Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace AsyncWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private static readonly List<Book> _books;

        static BookController()
        {
            _books = new List<Book>
            {
                new Book {Title = "Dr No", Genre = "Thriller", Rating = 4},
                new Book {Title = "Goldfinger", Genre = "Thriller", Rating = 5},
                new Book {Title = "Farenheit 451", Genre = "SF", Rating = 4},
                new Book {Title = "2001 - A Space Odyssey", Genre = "SF", Rating = 5},
                new Book {Title = "Frankenstein", Genre = "Horror", Rating = 5},
                new Book {Title = "Twelve", Genre = "Horror", Rating = 5},
                new Book {Title = "Varney the Vampire", Genre = "Horror", Rating = 3},
                new Book {Title = "Emma", Genre = "Classic", Rating = 4}
            };
        }

        public IEnumerable<Book> Get()
        {
            return _books;
        }

        [HttpGet("Recommendations/{genre}")]
        public IEnumerable<Book> Recommendations(string genre)
        {
            Thread.Sleep(1000);

            var genreBooks = _books.Where(b => string.Compare(b.Genre,genre,true) == 0);

            var maxRating = genreBooks.Max(b => b.Rating);

            return genreBooks.Where(b => b.Rating == maxRating);
        }
    }
}
