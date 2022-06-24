using projektApbd.Shared.Models;

namespace projektApbd.Server.Services
{
    public interface IUserService
    {
        public Task<bool> IsUserExists(string username);
        public Task<User> GetUser(string username);
        public Task AddUser(Shared.Models.DTOs.User user);
        public Task DeleteUser(Shared.Models.DTOs.User user);
        public Task SaveChanges();
    }
}