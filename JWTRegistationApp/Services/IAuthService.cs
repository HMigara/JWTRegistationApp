using JWTRegistationApp.Models;


namespace JWTRegistationApp.Services
{
    public interface IAuthService
    {
        Task<bool> RegisterUser(RegisterModel model);
        Task<string> Authenticate(LoginModel model);
        Task<bool> UserExists(string username);
    }
}