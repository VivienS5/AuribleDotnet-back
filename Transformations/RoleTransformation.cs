using System.Security.Claims;
using AuribleDotnet_back.Service.AuthServices;
using Microsoft.AspNetCore.Authentication;

namespace AuribleDotnet_back.Transformations
{
    public class RoleTransformation : IClaimsTransformation
    {
        private readonly UserService _userService;
        public RoleTransformation(UserService userService)
        {
            _userService = userService;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            Console.WriteLine("Role Transformation");
            if(principal.Identity != null && principal.Identity.IsAuthenticated){
                var nameIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier);
                if (nameIdClaim != null)
                {
                    Console.WriteLine($"OID: {nameIdClaim.Value}");
                    var isAdmin = _userService.IsAdmin(nameIdClaim.Value);
                    Claim roleClaim;
                    if(isAdmin){
                        roleClaim = new Claim(ClaimTypes.Role, "Admin");
                    }else{
                        roleClaim = new Claim (ClaimTypes.Role, "User");
                    }
                    var claimIdentity = principal.Identity as ClaimsIdentity;
                    claimIdentity?.AddClaim(roleClaim);
                }
            
            }
            return await Task.FromResult(principal);
        }
    }
}