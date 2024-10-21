using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using Microsoft.Identity.Abstractions;
using Microsoft.Identity.Web;

namespace AuribleDotnet_back.Controllers
{
    [ApiController]
    [Route("user")]
public class UserController : ControllerBase
{

    public UserController()
    {

    }

    [AuthorizeForScopes(Scopes = new[] { "profile","email"})]
    [Authorize(Policy = "RequireAdminPolicy")]
    [HttpGet]
    public IActionResult GetUserInfo()
    {
        var accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        // Décodez le jeton pour obtenir les claims
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(accessToken);
        // Extraire le nom
        var name = jwtToken.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
        var email = jwtToken.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
        var oid = jwtToken.Claims.FirstOrDefault(c => c.Type == "oid")?.Value;
        // Console.Write all claims

        // Vérifiez si le nom existe et renvoyez-le
        if (name != null)
        {
            return Ok(new { Name = name, Email = email, Oid = oid });
        }
        else
        {
            return NotFound("Le nom n'est pas trouvé dans le jeton.");
        }
    }

    }

}