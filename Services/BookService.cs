using System.Collections.Generic;
using System.Linq;
using Aurible.Models;
using Microsoft.EntityFrameworkCore;

namespace Aurible.Services
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;

        public BookService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _context.Book.ToList(); // Récupère tous les livres de la base de données
        }

        public Book? GetBookById(int id)
        {
            return _context.Book.FirstOrDefault(b => b.idBook == id);
        }
    }
}
