using System.Collections.Generic;
using Aurible.Models;

namespace Aurible.Services
{
    public interface IManageService
    {
        Book? GetBookById(int id);
        void AddBook(Book book);
        void UpdateBook(Book book);
        void DeleteBook(int id);
    }
}
