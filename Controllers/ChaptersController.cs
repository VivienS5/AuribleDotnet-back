using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 
using Aurible.Models;

namespace Aurible.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class ChaptersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ChaptersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: chapters/book/{idBook}
        [HttpGet("book/{idBook}")]
        public ActionResult<IEnumerable<Chapter>> GetChaptersByidBook(int idBook)
        {
            // Récupérer le livre avec les chapitres associés via la propriété de navigation
            var bookWithChapters = _context.Books // Remplacez Book par Books
                .Include(b => b.Chapters) // Inclure les chapitres associés
                .FirstOrDefault(b => b.idBook == idBook);

            if (bookWithChapters == null || bookWithChapters.Chapters == null || !bookWithChapters.Chapters.Any())
            {
                return NotFound("Aucun chapitre trouvé pour ce livre.");
            }

            // Renvoyer la collection des chapitres associés au livre
            return Ok(bookWithChapters.Chapters);
        }
    }
}
