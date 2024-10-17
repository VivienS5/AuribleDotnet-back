using System.Collections.Generic;
using System.Linq;
using Aurible.Models;

namespace Aurible.Services
{
    public class ManageService : IManageService
    {
        private static List<Book> _books = new List<Book>();

        public Book? GetBookById(int id)
        {
            return _books.FirstOrDefault(b => b.Id == id);
        }

        public void AddBook(Book book)
        {
            book.Id = _books.Count + 1; // Simple génération d'ID
            _books.Add(book);
        }

        public void UpdateBook(Book book)
        {
            var existingBook = GetBookById(book.Id);
            if (existingBook != null)
            {
                existingBook.title = book.title;
                existingBook.resume = book.resume;
                existingBook.coverURL = book.coverURL;
                existingBook.audioPath = book.audioPath;
                existingBook.maxPage = book.maxPage;
                existingBook.author = book.author;
            }
        }

        public void DeleteBook(int id)
        {
            var book = GetBookById(id);
            if (book != null)
            {
                _books.Remove(book);
            }
        }
    }
}
