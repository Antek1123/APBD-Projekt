using projektApbd.Shared.Models.DTOs;
using System.Net.Http.Json;

namespace projektApbd.Client.Services
{
    public interface IUserService
    {

    }
    public class UserService : IUserService
    {
        private readonly HttpClient _htttpClient;
        public UserService(HttpClient httpclient)
        {
            _htttpClient = httpclient;
        }
    }
}
