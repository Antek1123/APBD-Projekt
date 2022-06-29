using projektApbd.Shared.Models.DTOs;

using Microsoft.AspNetCore.Components;

namespace projektApbd.Client.Services
{
    public interface IAuthenticationService
    {
        public Task Initialize();
        public Task Login(UserLoginRequest userLoginRequest);
        public Task Logout();
        public Task Register(User user);
        bool IsUserLogin();
        static UserLoginResponse CurrentUser { get; set; }
    }
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IHttpService _httpService;
        private readonly NavigationManager _navigationManager;
        private readonly ILocalStorageService _localStorageService;
        public static UserLoginResponse CurrentUser { get; set; }

        public AuthenticationService(
            IHttpService httpService,
            NavigationManager navigationManager,
            ILocalStorageService localStorageService
        )
        {
            _httpService = httpService;
            _navigationManager = navigationManager;
            _localStorageService = localStorageService;
        }

        public bool IsUserLogin()
        {
            return !string.IsNullOrEmpty(CurrentUser?.JwtToken);
        }

        public async Task Initialize()
        {
            CurrentUser = await _localStorageService.GetItem<UserLoginResponse>("user");
        }

        public async Task Login(UserLoginRequest userLoginRequest)
        {
            CurrentUser = await _httpService.Login<UserLoginResponse, UserLoginRequest>(userLoginRequest);
            await _localStorageService.SetItem("user", CurrentUser);
        }

        public async Task Logout()
        {
            CurrentUser = null;
            await _localStorageService.RemoveItem("user");
            _navigationManager.NavigateTo("/login");
        }

        public async Task Register(User user)
        {
            await _httpService.Post("https://localhost:7040/api/User/register", user);
        }
    }
}