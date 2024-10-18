using Aurible.Models;

namespace Aurible.Services
{
    public class ManageService : IManageService
    {
        private readonly ApplicationDbContext _context;

        public ManageService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Book? GetBookById(int id)
        {
            return _context.Book.FirstOrDefault(b => b.Id == id);
        }

    public void AddBook(BookDto bookDto)
    {
        var book = new Book
        {
            title = bookDto.title,
            resume = bookDto.resume,
            coverURL = bookDto.coverURL,
            audioPath = bookDto.audioPath,
            maxPage = bookDto.maxPage,
            author = bookDto.author
        };
            _context.Book.Add(book); 
            _context.SaveChanges();
    }
        

        public void UpdateBook(Book book)
        {
            // Vérifie si le livre existe déjà dans la base de données
            var existingBook = GetBookById(book.Id);
            if (existingBook != null)
            {
                        existingBook.title = book.title;
                        existingBook.resume = book.resume;
                        existingBook.coverURL = book.coverURL;
                        existingBook.audioPath = book.audioPath;
                        existingBook.maxPage = book.maxPage;
                        existingBook.author = book.author;

                _context.SaveChanges();
            }
        }

        public void DeleteBook(int id)
        {
            var book = GetBookById(id);
            if (book != null)
            {
                _context.Book.Remove(book); // Supprime le livre de la DbContext
                _context.SaveChanges(); // Sauvegarde les modifications
            }
        }

        public void AddBook(Book book)
        {
            throw new NotImplementedException();
        }
    }
}
