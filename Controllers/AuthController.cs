using Microsoft.AspNetCore.Mvc;
using AuribleDotnet_back.Interface;
using Microsoft.AspNetCore.Authorization;

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
        [HttpPost("logout")]
        [Authorize]
        public IActionResult Logout(){
            return Ok();
        }
        [HttpGet("callback")]
        public IActionResult Callback(){
            return Ok();
        }

    }
}
