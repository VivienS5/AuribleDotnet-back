using System.Collections.Generic;
using Aurible.Models;

namespace Aurible.Services
{
    public interface IBookService
    {
        IEnumerable<Book> GetAllBooks();
        Book? GetBookById(int id);
        void AddBook(Book book);
        void UpdateBook(Book book);
        void DeleteBook(int id);
    }
}
