namespace AuribleDotnet_back.Interface{
    public interface IJwtTokenService{
        string GenerateAccessToken();
        bool AccessTokenIsValid();
    }
}