using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aurible.Controllers
{
    [ApiController]
    [Route("auth")] // Route de base pour ce contrôleur
    public class AuthController : ControllerBase
    {
        // Endpoint de connexion (Sign In) accessible à tout le monde
        [HttpPost("signin")]
        [AllowAnonymous] // Accessible à tous
        public IActionResult SignIn([FromBody] AuthRequest request)
        {
            // Logique pour authentifier l'utilisateur
            // Exemple de réponse fictive (remplace par une logique réelle)
            if (request.Username == "user" && request.Password == "password")
            {
                return Ok(new { Token = "exemple_de_token_jwt" }); // Exemple de token JWT
            }
            return Unauthorized();
        }

        // Endpoint de déconnexion (Sign Out)
        // Note : En JWT, la déconnexion est généralement gérée côté client (on supprime le token)
        [HttpPost("signout")]
        [Authorize] // Nécessite que l'utilisateur soit connecté pour se déconnecter
        public IActionResult SignOut()
        {
            // En pratique, pour JWT, il suffit de demander au client de supprimer le token côté client.
            // Vous pourriez aussi implémenter une logique pour ajouter le token à une liste noire.
            return Ok("Déconnecté avec succès");
        }
    }

    // Modèle de données pour l'authentification
    public class AuthRequest
    {
        public int id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string accessToken { get; set; }
        public DateTime expirationDate { get; set; }
        public string refreshToken { get; set; }
        public string role { get; set; }
        public string microsoftId { get; set; }
    }
}
