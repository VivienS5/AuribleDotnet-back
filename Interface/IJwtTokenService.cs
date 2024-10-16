using System.Security.Claims;

namespace AuribleDotnet_back.Interface{
    public interface IJwtTokenService{
        string GenerateAccessToken();
        string GenerateAccessToken(IEnumerable<Claim> claims);

        string GenerateRefreshToken();
        ClaimsPrincipal? ValidateToken(string accessToken);

        bool ValidateRefreshToken(string refreshToken);
        
    }
}