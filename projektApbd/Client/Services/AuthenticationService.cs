using projektApbd.Shared.Models.DTOs;

using Microsoft.AspNetCore.Components;

namespace projektApbd.Client.Services
{
    public interface IAuthenticationService
    {
        UserLoginResponse UserResponse { get; }
        public Task Initialize();
        public Task Login(UserLoginRequest userLoginRequest);
        public Task Logout();
    }
    public class AuthenticationService : IAuthenticationService
    { 
        private readonly IHttpService _httpService;
        private readonly NavigationManager _navigationManager;
        private readonly ILocalStorageService _localStorageService;

        public UserLoginResponse UserResponse { get; private set; } 

        public async Task Initialize()
        {
            UserResponse = await _localStorageService.GetItem<UserLoginResponse>("user");
        }

        public async Task Login(UserLoginRequest userLoginRequest)
        {
            UserResponse = await _httpService.Post<UserLoginResponse>("/api/User/login", userLoginRequest);
            await _localStorageService.SetItem("user", UserResponse);
        }

        public async Task Logout()
        {
            UserResponse = null;
            await _localStorageService.RemoveItem("user");
            _navigationManager.NavigateTo("login");
        }
    }
}
