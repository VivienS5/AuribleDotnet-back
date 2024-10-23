
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
        public void Register(ClaimsPrincipal claims)
        {
            // Récupérer les informations de l'utilisateur depuis les claims (par exemple, l'email)
            var email = claims?.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(email))
            {
                throw new Exception("Email not found in claims");
            }

            // Vérifier si l'utilisateur existe déjà dans la base de données
            var existingUser = _userRepository.FindByEmail(email);
            if (existingUser == null)
            {
                // Si l'utilisateur n'existe pas, le créer
                var newUser = new User
                {
                    Email = email,
                    Name = claims.FindFirst(ClaimTypes.Name)?.Value,
                    Role = "User" // Par défaut, l'utilisateur aura le rôle "User"
                };

                _userRepository.Create(newUser);
            }
        }
public bool AccessTokenIsValid(string accessToken)
{
    try
    {
        // Valider le token en utilisant le service JWT
        var claims = _JWTService.ValidateToken(accessToken);
        return claims != null;
    }
    catch (SecurityTokenException)
    {
        return false;
    }
}

    }
    
    
}