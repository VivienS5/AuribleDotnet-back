using System.Collections.Generic;
using Aurible.Models;

namespace Aurible.Services
{
    public interface IBookService
    {
        IEnumerable<Book> GetAllBooks();
        Book? GetBookById(int id);
    }
}
