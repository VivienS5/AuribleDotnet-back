using AuribleDotnet_back.Interface;
using Microsoft.Extensions.Options;
using AuribleDotnet_back.Settings;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Cryptography;

namespace AuribleDotnet_back.Service.AuthServices
{
    public class JWTService(IOptions<JwtSettings> jwtSettings,IConfiguration configuration) : IJwtTokenService
    {
        private readonly JwtSettings _jwtSettings = jwtSettings.Value;
        private readonly IConfiguration _configuration = configuration;
        public bool ValidateRefreshToken(string refreshToken){
            return true;
        }
        public ClaimsPrincipal? ValidateToken(string accessToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);

            var validationParameters = new TokenValidationParameters{
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtSettings.Issuer,
                ValidAudience = _jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ClockSkew  = TimeSpan.Zero
            };
            try{
                var principal = tokenHandler.ValidateToken(accessToken, validationParameters, out SecurityToken validatedToken);

                if(validatedToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase)){
                    throw new SecurityTokenException("Invalid token");
                }
                return principal;
            }catch(Exception ex){
                Console.WriteLine($"Token validation failed: {ex.Message}");
                return null;
            }
            throw new NotImplementedException();
        }

        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.ExpiryMinutes),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public string GenerateAccessToken()
        {
            var claims = new List<Claim>{
                new(ClaimTypes.Role,"0"),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.ExpiryMinutes),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
            throw new NotImplementedException();
        }
    }
}