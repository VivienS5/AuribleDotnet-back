using Aurible.Models;
using Microsoft.AspNetCore.Mvc;

namespace Aurible.Services
{
    public class AudioStreamingService
    {
        private readonly ApplicationDbContext _context;

        public AudioStreamingService(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Chapter>? GetChaptersByBookId(int idBook)
        {
            var chapters = _context.Books
                       .Where(book => book.idBook == idBook)
                       .SelectMany(book => book.Chapters)
                       .ToList();
            return chapters.Count != 0 ? chapters : null;
        }

        public FileStreamResult? GetAudioStreamingById(int id)
        {
            string? path = _context.Books.Where(book => book.idBook == id).Select(book => book.audioPath).FirstOrDefault();
            if (path == null || !File.Exists(path))
            {
                return null;
            }
            var stream = new FileStream(path, FileMode.Open, FileAccess.Read);

            return new FileStreamResult(stream, "audio/mpeg")
            {
                EnableRangeProcessing = true,
            };
        }
    }
    
}