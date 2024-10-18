using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using AuribleDotnet_back.Interface;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using System.Security.Claims;

namespace AuribleDotnet_back.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService ;
        public AuthController(IAuthService authService){
            _authService = authService;
        }
        [HttpGet("signin")]
        public IActionResult SignIn()
        {
            var redirectUrl = Url.Action("SignInCallback", "Auth");
            var properties = new AuthenticationProperties
            {
                RedirectUri = redirectUrl,
                // Aucune nécessité de stocker le state manuellement ici
            };

            return Challenge(properties, OpenIdConnectDefaults.AuthenticationScheme);
        }

        [HttpGet("signin-callback")]
        public async Task<IActionResult> SignInCallback()
        {
            var auth = await HttpContext.AuthenticateAsync(OpenIdConnectDefaults.AuthenticationScheme);
            var returnedState = auth.Properties?.Items["state"];
            var storedState = HttpContext.Session.GetString("authState");

            return Ok();
            // Authentification via OpenID Connect
            // var auth = await HttpContext.AuthenticateAsync(OpenIdConnectDefaults.AuthenticationScheme);
            // if (!auth.Succeeded)
            // {
            //     return BadRequest(new { error = "Authentication failed" });
            // }

            // // Récupération des claims utilisateur
            // var claims = auth.Principal.Claims;
            // var userId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            // var userName = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

            // // Retourner les informations utilisateur
            // return Ok(new { userId, userName });
        }
        
        [HttpPost("signout")]
        public new IActionResult SignOut()
        {
            return Ok("Sign out");
        }
    }
}
