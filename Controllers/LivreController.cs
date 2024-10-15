using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aurible.Controllers
{
    [ApiController]
    [Route("livre")] // Route de base pour ce contrôleur
    public class LivreController : ControllerBase
    {
        // Endpoint pour obtenir un livre par ID
        [HttpGet("{id}")]
        [Authorize] // Accessible uniquement aux utilisateurs connectés
        public IActionResult GetLivre(int id)
        {
            // Logique pour récupérer le livre avec l'ID donné
            // Exemple de réponse fictive
            return Ok(new { Id = id, Titre = "Exemple de Livre", Auteur = "Auteur Exemple" });
        }
    }
}
