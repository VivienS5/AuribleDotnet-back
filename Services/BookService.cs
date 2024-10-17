using System.Collections.Generic;
using System.Linq;
using Aurible.Models;

namespace Aurible.Services
{
    public class BookService : IBookService
    {
        private static List<Book> _books = new List<Book>();

        public IEnumerable<Book> GetAllBooks()
        {
            return _books;
        }

        public Book? GetBookById(int id)
        {
            return _books.FirstOrDefault(b => b.Id == id);
        }
    }
}
