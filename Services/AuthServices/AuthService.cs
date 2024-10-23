
using AuribleDotnet_back.Interface;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuribleDotnet_back.Service.AuthServices{
    public class AuthService(IJwtTokenService JWTService,IConfiguration configuration):IAuthService
    {
        private readonly IJwtTokenService _JWTService = JWTService;
        private readonly IConfiguration _configuration = configuration;

        public bool AccessTokenIsValid(string accessToken)
        {
            throw new NotImplementedException();
        }

        public void CheckUser()
        {
            throw new NotImplementedException();
        }

        public void ExchangeToAccessToken()
        {
            _JWTService.GenerateAccessToken();
            throw new NotImplementedException();
        }
        /**
        * Enregistre un utilisateur si celui-ci n'existe pas dans la base de données
        * Les informations sont pris depuis Azure AD
        */
        public void Register()
        {
            throw new NotImplementedException();
        }

        public string? SignIn(string accessToken){
            try{
                ClaimsPrincipal? claims = _JWTService.ValidateToken(accessToken) ?? throw new SecurityTokenException("Invalid token");
                string newAccessToken =_JWTService.GenerateAccessToken();
                return newAccessToken;
            }
            catch(Exception ex){
                Console.WriteLine($"Token validation failed: {ex.Message}");
                return null;
            }
        }
        public void SignOut(){

        }

    }
}