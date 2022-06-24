﻿using projektApbd.Shared.Models;

namespace projektApbd.Server.Services
{
    public interface IUserService
    {
        public Task<bool> IsUserExistsByUsername(string username);
        public Task<bool> IsUserExistsByEmail(string email);
        public Task<User> GetUser(string username);
        public Task AddUser(User user);
        public Task Register(Shared.Models.DTOs.User user);
        public Task<projektApbd.Shared.Models.DTOs.UserLoginResponse> Login(projektApbd.Shared.Models.DTOs.UserLoginRequest userLoginRequest);
        public Task DeleteUser(Shared.Models.DTOs.User user);
        public Task SaveChanges();
    }
}