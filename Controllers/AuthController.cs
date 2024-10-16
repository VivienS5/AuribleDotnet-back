// using Microsoft.AspNetCore.Authentication;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using Aurible.Models;
// using Aurible.Services;
// using System.Security.Claims;
// using System.Threading.Tasks;

// namespace Aurible.Controllers
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     public class AuthController : ControllerBase
//     {
//         private readonly IAuthService _authService;

//         public AuthController(IAuthService authService)
//         {
//             _authService = authService;
//         }

//         [HttpGet("me")]
//         [Authorize]
//         public async Task<ActionResult<AuthModel>> GetMe()
//         {
//             var auth = await _authService.GetAuthAsync(User);
//             return Ok(auth);
//         }
        
//         [HttpPost("signout")]
//         public async Task<IActionResult> SignOut()
//         {
//             await HttpContext.SignOutAsync();
//             return Ok();
//         }
//     }
// }
