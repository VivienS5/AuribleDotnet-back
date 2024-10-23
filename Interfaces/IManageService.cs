using Aurible.Models;

namespace Aurible.Services
{
    public interface IManageService
    {
        Book? GetBookById(int id);
        Book AddBook(BookDto bookDto);
        bool UploadBook(IFormFile formFile,int idBook);
        void UpdateBook(Book book);
        void DeleteBook(int id);
    }
}
