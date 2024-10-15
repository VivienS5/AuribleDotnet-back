using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aurible.Controllers
{
    [ApiController]
    [Route("manage")] // Route de base pour ce contrôleur
    [Authorize(Roles = "Admin")] // Accessible uniquement aux utilisateurs avec le rôle Admin
    public class ManageController : ControllerBase
    {
        // Endpoint pour la gestion accessible uniquement aux admins
        [HttpGet]
        public IActionResult Manage()
        {
            // Logique pour gérer les ressources
            return Ok("Zone de gestion, accessible uniquement par l'admin.");
        }
    }
}
