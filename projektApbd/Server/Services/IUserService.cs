using projektApbd.Shared.Models;

namespace projektApbd.Server.Services
{
    public interface IUserService
    {
        public Task<bool> IsUserExistsByUsername(string username);
        public Task<bool> IsUserExistsByEmail(string email);
        public Task<User> GetUser(string username);
        public Task<User> GetUserById(int id);
        public Task AddUser(User user);
        public Task Register(Shared.Models.DTOs.User user);
        public Task<Shared.Models.DTOs.UserLoginResponse> Login(Shared.Models.DTOs.UserLoginRequest userLoginRequest);
        public Task DeleteUser(Shared.Models.DTOs.User user);
        public Task AddCompanyToWatchlist(int userId, int companyId);
        public Task<List<Shared.Models.DTOs.Company>> GetWatchlistCompanies(int userId);
        public Task DeleteCompanyFromWatchlist(int userId, int companyId);
        public Task SaveChanges();
    }
}