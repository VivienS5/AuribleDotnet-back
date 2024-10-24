namespace AuribleDotnet_back.Interface
{
    public interface IAuthService{
        string? SignIn(string accessToken);
        void SignOut();
        void Register();
        void CheckUser();
    }    
}