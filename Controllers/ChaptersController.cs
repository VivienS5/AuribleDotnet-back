using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Pour inclure la navigation property
using Aurible.Models;
using System.Collections.Generic;
using System.Linq;

namespace Aurible.Controllers
{
    [Route("chapters")]
    [ApiController]
    public class ChaptersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ChaptersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: chapters/book/{bookId}
        [HttpGet("book/{bookId}")]
        public ActionResult<IEnumerable<Chapter>> GetChaptersByBookId(int bookId)
        {
            // Récupérer le livre avec les chapitres associés via la propriété de navigation
            var bookWithChapters = _context.Book
                .Include(b => b.Chapters) // Inclure les chapitres associés
                .FirstOrDefault(b => b.idBook == bookId);

            if (bookWithChapters == null || bookWithChapters.Chapters == null || bookWithChapters.Chapters.Count == 0)
            {
                return NotFound("Aucun chapitre trouvé pour ce livre.");
            }

            // Renvoyer la collection des chapitres associés au livre
            return Ok(bookWithChapters.Chapters);
        }
    }
}
