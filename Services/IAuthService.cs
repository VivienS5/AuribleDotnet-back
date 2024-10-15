using System.Threading.Tasks;

public interface IAuthService
{
    Task<LoginResponse> AuthenticateAsync(LoginRequest request);
}
