using Aurible.Models;

namespace Aurible.Services
{
    public class ManageService : IManageService
    {
        private readonly ApplicationDbContext _context;
        private readonly IChapterService _chapterService;
        private readonly TTSService _ttsService;
        private delegate List<Chapter> GetChapters();

        public ManageService(ApplicationDbContext context,IChapterService chapterDbService,TTSService ttsService)
        {
            _context = context;
            _chapterService = chapterDbService;
            _ttsService = ttsService;
        }

        public Book? GetBookById(int id)
        {
            return _context.Books.FirstOrDefault(b => b.idBook == id);
        }

        public void AddBook(BookDto bookDto)
        {
            var book = new Book
            {
                title = bookDto.title,
                resume = bookDto.resume,
                coverURL = bookDto.coverURL,
                author = bookDto.author
            };
            string file_path = "livre/romance_tragique.pdf";
            Book newBook =  _context.Books.Add(book).Entity; 
            _context.SaveChanges();
            Task.Run(() => _ttsService.UploadBook(file_path,newBook.idBook,OnChapterAdded));
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
            Console.WriteLine("Chapters added: "+idBook);
            Book? book = _context.Books.Find(idBook);
            if(book == null) return;
            _chapterService.Add(chapters,book);
            book.audioPath = $"audio/{idBook}.mp3";
            _context.Books.Update(book);
             _context.SaveChanges();
        }
    }
}
