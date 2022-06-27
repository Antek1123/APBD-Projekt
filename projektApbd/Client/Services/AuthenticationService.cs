using projektApbd.Shared.Models.DTOs;

using Microsoft.AspNetCore.Components;

namespace projektApbd.Client.Services
{
    public interface IAuthenticationService
    {
        public UserLoginResponse UserResponse { get; }
        public Task Initialize();
        public Task Login(UserLoginRequest userLoginRequest);
        public Task Logout();
        public Task Register(User user);
    }
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IHttpService _httpService;
        private readonly NavigationManager _navigationManager;
        private readonly ILocalStorageService _localStorageService;
        public UserLoginResponse UserResponse { get; private set; }

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

        public async Task Initialize()
        {
            UserResponse = await _localStorageService.GetItem<UserLoginResponse>("user");
        }

        public async Task Login(UserLoginRequest userLoginRequest)
        {
            UserResponse = await _httpService.Login<UserLoginResponse, UserLoginRequest>(userLoginRequest);
            await _localStorageService.SetItem("user", UserResponse);
        }

        public async Task Logout()
        {
            UserResponse = null;
            await _localStorageService.RemoveItem("user");
            _navigationManager.NavigateTo("/login");
        }

        public async Task Register(User user)
        {
            await _httpService.Post("https://localhost:7040/api/User/register", user);
        }
    }
}