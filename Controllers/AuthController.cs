using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using AuribleDotnet_back.Interface;
using Microsoft.AspNetCore.Mvc.Routing;

namespace AuribleDotnet_back.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn()
        {
            var authHeader = Request.Headers.Authorization.FirstOrDefault();
            if(authHeader is not null && authHeader.StartsWith("Bearer ")){
                var token = authHeader["Bearer ".Length..].Trim();
                var result = _authService.SignIn(token);
                Console.WriteLine(result);
                if(result is null){
                    return Redirect(_authService.AzureUrl());
                }
                return Ok();
            }
            return Redirect(_authService.AzureUrl());
        }
        [HttpPost("signin-callback")]
        public async Task<IActionResult> SignInCallback(){
            return Ok();
        }
        
        [HttpPost("signout")]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync();
            return Ok();
        }
    }
}
