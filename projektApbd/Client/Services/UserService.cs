using projektApbd.Shared.Models.DTOs;
using System.Net.Http.Json;

namespace projektApbd.Client.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _htttpClient;
        public UserService(HttpClient httpclient)
        {
            _htttpClient = httpclient;
        }

        public Task<UserLoginResponse> UserLogin(UserLoginRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
