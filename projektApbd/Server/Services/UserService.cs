﻿using projektApbd.Shared.Models;
using projektApbd.Server.Authorization;
using BCryptNet = BCrypt.Net.BCrypt;
using projektApbd.Server.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace projektApbd.Server.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IJwtUtils _jwtUtils;

        public UserService(AppDbContext context, IJwtUtils jwtUtils)
        {
            _context = context;
            _jwtUtils = jwtUtils;
        }

        public async Task AddCompanyToWatchlist(int userId, int companyId)
        {
            await _context.UserCompanies.AddAsync(new UserCompany
            {
                UserId = userId,
                CompanyId = companyId
            });
            await SaveChanges();
        }

        public async Task AddUser(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task DeleteCompanyFromWatchlist(int userId, int companyId)
        {
            var userCompany = new UserCompany
            {
                UserId = userId,
                CompanyId = companyId
            };
            _context.Entry(userCompany).State = EntityState.Deleted;
            await SaveChanges();
        }

        public async Task DeleteUser(Shared.Models.DTOs.User user)
        {
            if (!IsUserExistsByUsername(user.Username).Result)
                throw new NotFoundException("User not exists");

            var deleteUser = new User
            {
                Username = user.Username,
                Password = user.Password,
                Email = user.Email
            };

            _context.Entry(deleteUser).State = EntityState.Deleted;
            await SaveChanges();
        }

        public async Task<List<Shared.Models.DTOs.Company>> GetWatchlistCompanies(int userId)
        {
            return await _context.UserCompanies.Where(e => e.UserId == userId).Select(e => new Shared.Models.DTOs.Company
            {
                Id = _context.Companies.FirstOrDefault(f => f.Id == e.CompanyId).Id,
                Ticker = _context.Companies.FirstOrDefault(f => f.Id == e.CompanyId).Ticker,
                Name = _context.Companies.FirstOrDefault(f => f.Id == e.CompanyId).Name,
                Homepage_url = _context.Companies.FirstOrDefault(f => f.Id == e.CompanyId).Homepage_url,
                Locale = _context.Companies.FirstOrDefault(f => f.Id == e.CompanyId).Locale,
                Logo_Url = _context.Companies.FirstOrDefault(f => f.Id == e.CompanyId).Logo_Url,
                //Phone_Number = _context.Companies.FirstOrDefault(f => f.Id == e.CompanyId).Phone_Number,
                Description = _context.Companies.FirstOrDefault(f => f.Id == e.CompanyId).Description,
                Currency_Name = _context.Companies.FirstOrDefault(f => f.Id == e.CompanyId).Currency_Name,
                Active = _context.Companies.FirstOrDefault(f => f.Id == e.CompanyId).Active
            }).ToListAsync();
        }

        public async Task<User> GetUser(string username)
        { 
            if (IsUserExistsByUsername(username).Result)
                return await _context.Users.FirstOrDefaultAsync(e => e.Username.Equals(username));
            else
                throw new NotFoundException("User not exists");
        }

        public async Task<User> GetUserById(int id)
        { 
            var user = await _context.Users.FirstOrDefaultAsync(e => e.Id == id);
            if (user == null)
                throw new NotFoundException("User not found");
            return user;
        }

        public Task<bool> IsUserExistsByEmail(string email)
        {
            return _context.Users.AnyAsync(e => e.Email == email);
        }

        public Task<bool> IsUserExistsByUsername(string username)
        {
            return _context.Users.AnyAsync(e => e.Username == username);
        }

        public async Task<Shared.Models.DTOs.UserLoginResponse> Login(Shared.Models.DTOs.UserLoginRequest userLoginRequest)
        {
            var user = await GetUser(userLoginRequest.Username);

            if (!BCryptNet.Verify(userLoginRequest.Password, user.Password))
                throw new BadRequestException("Incorrect username or password");

            var userResponse = new Shared.Models.DTOs.UserLoginResponse
            {
                Id = user.Id,
                Username = user.Username,
                JwtToken = await _jwtUtils.GenerateToken(user)
            };

            return userResponse;
        }

        public async Task Register(Shared.Models.DTOs.User user)
        {
            if (IsUserExistsByUsername(user.Username).Result)
                throw new BadRequestException("User with this username already exists");

            if (IsUserExistsByEmail(user.Email).Result)
                throw new BadRequestException("This email is already taken");

            var newUser = new User
            {
                Username = user.Username,
                Password = BCryptNet.HashPassword(user.Password),
                Email = user.Email
            };

            await AddUser(newUser);
            await SaveChanges();
        }

        public Task SaveChanges()
        {
            return _context.SaveChangesAsync();
        }
    }
}
