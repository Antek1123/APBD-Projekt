using projektApbd.Shared.Models.DTOs;
using System.Net.Http.Json;

namespace projektApbd.Client.Services
{
    public interface IUserService
    {
        public Task PostCompanyToWatchlist(int companyId);
        public Task DeleteCompanyFromWatchlist(int companyId);
        public Task<List<Company>> GetWatchlistCompany();
    }
    public class UserService : IUserService
    {
        private readonly IHttpService _httpService;
        private readonly ILocalStorageService _localStorage;
        public UserService(IHttpService httpService, ILocalStorageService localStorage)
        {
            _httpService = httpService;
            _localStorage = localStorage;
        }

        public Task DeleteCompanyFromWatchlist(int companyId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Company>> GetWatchlistCompany()
        {
            var userId = _localStorage.GetItem<UserLoginResponse>("user").Id;
            return await _httpService.Get<List<Company>>($"https://localhost:7040/api/User/{userId}/watchlist");
        }

        public async Task PostCompanyToWatchlist(int companyId)
        {
            var userId = _localStorage.GetItem<UserLoginResponse>("user").Id;
            await _httpService.Post($"https://localhost:7040/api/User/{userId}/watchlist", companyId);
        }
    }
}
