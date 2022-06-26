using Microsoft.AspNetCore.Components;

namespace projektApbd.Client.Services
{
    public interface IAuthenticationService
    {
        public Task Login(string username, string password);
    }
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IHttpService _httpService;
        private NavigationManager _navigationManager;
        private ILocalStorageService _localStorageService;
        public Task Login(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
