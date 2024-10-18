namespace AuribleDotnet_back.Settings
{
    public class JwtSettings{
        public required string SecretKey {get; set;}
        public required string Issuer {get; set;}
        public required string Audience {get; set;}
        public int ExpiryMinutes { get; set; }

    }
}