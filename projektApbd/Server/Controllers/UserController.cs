using BCryptNet = BCrypt.Net.BCrypt;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using projektApbd.Server.Services;

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
        public async Task<IActionResult> Register(Shared.Models.DTOs.User user)
        {
            await _userService.Register(user);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(Shared.Models.DTOs.UserLoginRequest userLogin)
        {
            var response = await _userService.Login(userLogin);
            return Ok(response);
        }
    }
}
