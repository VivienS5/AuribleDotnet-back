using AuribleDotnet_back.Interface;
using Microsoft.Extensions.Options;
using AuribleDotnet_back.Settings;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace AuribleDotnet_back.Service.AuthServices
{
    class JWTService : IJwtTokenService
    {
        private readonly JwtSettings _jwtSettings;
        public JWTService(IOptions<JwtSettings> jwtSettings){
            _jwtSettings = jwtSettings.Value;
        }
        public bool AccessTokenIsValid()
        {
            throw new NotImplementedException();
        }

        public string GenerateAccessToken()
        {
            var claim = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };
            throw new NotImplementedException();
        }
    }
}