using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace AuribleDotnet_back.Transformations
{
    public class RoleTransformation : IClaimsTransformation
    {

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            Console.WriteLine("Role Transformation");
            if(principal.Identity != null && principal.Identity.IsAuthenticated){
                var roleClaim = new Claim (ClaimTypes.Role, "Admin");
                
                var claimIdentity = principal.Identity as ClaimsIdentity;
                claimIdentity?.AddClaim(roleClaim);
            
            }
            return await Task.FromResult(principal);
        }
    }
}