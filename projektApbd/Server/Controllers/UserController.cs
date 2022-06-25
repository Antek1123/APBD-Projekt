﻿using BCryptNet = BCrypt.Net.BCrypt;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using projektApbd.Server.Services;
using Microsoft.AspNetCore.Authorization;

namespace projektApbd.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(Shared.Models.DTOs.User user)
        {
            await _userService.Register(user);
            return Ok();
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(Shared.Models.DTOs.UserLoginRequest userLogin)
        {
            var response = await _userService.Login(userLogin);
            return Ok(response);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUser(string username)
        {
            return Ok(await _userService.GetUser(username));
        }

        [HttpPost]
        public async Task<IActionResult> AddCompanyToWatchlist(int userId, int companyId)
        {
            await _userService.AddCompanyToWatchlist(userId, companyId);
            await _userService.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCompanyFromWatchlist(int userId, int companyId)
        {
            _userService.DeleteCompanyFromWatchlist(userId, companyId);
            await _userService.SaveChanges();
            return Ok();
        }
    }
}
