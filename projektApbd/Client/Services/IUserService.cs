using projektApbd.Shared.Models.DTOs;

namespace projektApbd.Client.Services
{
    public interface IUserService
    {
        public Task<UserLoginResponse> UserLogin(UserLoginRequest request);
    }
}
