using Microsoft.AspNetCore.Mvc;
using Aurible.Services; // Assurez-vous d'inclure le bon namespace
using Aurible.Models; // Pour le modèle Chapter
using System.Collections.Generic;

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

        // GET: api/chapters/{bookId}
        [HttpGet("{bookId}")]
        public ActionResult<IEnumerable<Chapter>> GetChaptersByBookId(int bookId)
        {
            // Récupérer tous les chapitres associés au livre avec bookId
            var chapters = _context.Chapter.Where(c => c.idBook_FK == bookId).ToList();
            if (chapters == null || chapters.Count == 0)
            {
                return NotFound("Aucun chapitre trouvé pour ce livre.");
            }

            return Ok(chapters);
        }
    }
}
