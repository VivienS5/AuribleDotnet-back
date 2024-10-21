using Microsoft.AspNetCore.Mvc;
using AuribleDotnet_back.Interface;
using Microsoft.AspNetCore.Authorization;
using AuribleDotnet_back.Service.AuthServices;
using Aurible.Models;
using System.Security.Claims;

namespace AuribleDotnet_back.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserService  _userService;
        public AuthController(UserService userService){
            _userService = userService;
        }
        [HttpGet("login")]
        [Authorize]
        public IActionResult Login(){
            var accessToken = HttpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", "");
            var claimse = HttpContext.User.Claims;
            try{
                var nameidentifierClaim = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                if(nameidentifierClaim == null){
                    return BadRequest();
                }
                Console.WriteLine("OID: " + nameidentifierClaim);
                bool exist =_userService.HasExist(nameidentifierClaim);
                if(exist){
                    return Ok();
                }
                var claims = HttpContext.User.Claims;
                var user = new User
                {
                    IdUser = 0,
                    Name = claims.FirstOrDefault(c => c.Type == "name")?.Value ?? "Default Name",
                    Email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value ?? "Default Email",
                    IdMicrosoft = nameidentifierClaim,
                    Role = 0
                };
                _userService.CreateUser(user);

                return Ok();
            }catch(Exception e){
                Console.WriteLine(e);
                return BadRequest();
            }
        }

    }
}
