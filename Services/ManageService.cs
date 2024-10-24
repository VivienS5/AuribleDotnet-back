using Aurible.Models;

namespace Aurible.Services
{
    public class ManageService : IManageService
    {
        private readonly ApplicationDbContext _context;
        private readonly IChapterService _chapterService;
        private readonly TTSService _ttsService;
        private delegate List<Chapter> GetChapters();
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ManageService(ApplicationDbContext context,IChapterService chapterDbService,TTSService ttsService,IServiceScopeFactory serviceScopeFactory)
        {
            _context = context;
            _chapterService = chapterDbService;
            _ttsService = ttsService;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public Book? GetBookById(int id)
        {
            return _context.Books.FirstOrDefault(b => b.idBook == id);
        }

        public Book AddBook(BookDto bookDto)
        {
            var book = new Book
            {
                title = bookDto.title,
                resume = bookDto.resume,
                coverURL = bookDto.coverURL,
                author = bookDto.author
            };
            Book newBook = _context.Books.Add(book).Entity; 
            _context.SaveChanges();
            return newBook;
        }
        public bool UploadBook(IFormFile formFile,int idBook){
            var filePath = Path.Combine("livre",formFile.FileName);
            Book? findBook = _context.Books.Find(idBook);
            if(findBook == null) return false;
            using var stream = new FileStream(filePath, FileMode.Create);
            formFile.CopyTo(stream);
            Task.Run(() => _ttsService.UploadBook(filePath,findBook.idBook,OnChapterAdded));
            return true;
        }
        public void UpdateBook(Book book)
        {
            // Vérifie si le livre existe déjà dans la base de données
            var existingBook = GetBookById(book.idBook);
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
                _context.Books.Remove(book); // Supprime le livre de la DbContext
                _context.SaveChanges(); // Sauvegarde les modifications
            }
        }
        public void OnChapterAdded(List<ChapterTTS> chapters,int idBook)
        {
            Console.WriteLine("Chapters added: "+chapters.Count);
            if(chapters.Count == 0) return;
            Task.Run(() => {
                using var scope = _serviceScopeFactory.CreateScope();
                var dbContext =scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var chapterDbService = scope.ServiceProvider.GetRequiredService<IChapterService>();
                Book? book = dbContext.Books.Find(idBook);
                if(book == null) return;
                chapterDbService.Add(chapters,book);
                book.audioPath = $"audio/{idBook}.mp3";
                dbContext.Books.Update(book);
                 dbContext.SaveChanges();
            });
        }
    }
}
