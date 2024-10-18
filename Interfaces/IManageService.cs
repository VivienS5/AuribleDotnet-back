using Aurible.Models;

namespace Aurible.Services
{
    public interface IManageService
    {
        Book? GetBookById(int id);
        void AddBook(BookDto bookDto);
        void UpdateBook(Book book);
        void DeleteBook(int id);
    }
}
