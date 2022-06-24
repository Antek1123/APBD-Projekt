using projektApbd.Shared.Models;
using projektApbd.Server.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace projektApbd.Server.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddUser(Shared.Models.DTOs.User user)
        {
            var newUser = new Shared.Models.User
            {
                Username = user.Username,
                Password = user.Password,
                Email = user.Email
            };

            _context.Entry(newUser).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            await SaveChanges(); 
        }

        public async Task DeleteUser(Shared.Models.DTOs.User user)
        {
            if (!IsUserExists(user.Username).Result)
                throw new NotFoundException("User not exists");

            var deleteUser = new Shared.Models.User
            {
                Username = user.Username,
                Password = user.Password,
                Email = user.Email
            };

            _context.Entry(deleteUser).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            await SaveChanges();
        }

        public async Task<Shared.Models.User> GetUser(string username)
        { 
            if (IsUserExists(username).Result)
                return await _context.Users.FirstOrDefaultAsync(e => e.Username == username);
            else
                throw new NotFoundException("User not exists");
        }

        public Task<bool> IsUserExists(string username)
        {
            return _context.Users.AnyAsync(e => e.Username == username);
        }

        public Task SaveChanges()
        {
            return _context.SaveChangesAsync();
        }
    }
}
