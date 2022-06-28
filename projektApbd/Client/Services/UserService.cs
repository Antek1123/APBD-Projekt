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

        public async Task DeleteCompanyFromWatchlist(int companyId)
        {
            var user = _localStorage.GetItem<UserLoginResponse>("user");
            await _httpService.Delete($"https://localhost:7040/api/User/{user.Id}/watchlist/{companyId}");
        }

        public async Task<List<Company>> GetWatchlistCompany()
        {
            var user = await _localStorage.GetItem<UserLoginResponse>("user");
            return await _httpService.Get<List<Company>>($"https://localhost:7040/api/User/{user.Id}/watchlist");
        }

        public async Task PostCompanyToWatchlist(int companyId)
        {
            var user = await _localStorage.GetItem<UserLoginResponse>("user");
            await _httpService.Post($"https://localhost:7040/api/User/{user.Id}/watchlist", companyId);
        }
    }
}
