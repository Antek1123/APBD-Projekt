using BCryptNet = BCrypt.Net.BCrypt;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using projektApbd.Server.Services;
using projektApbd.Server.Authorization;

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
        public async Task<IActionResult> Register([FromBody] Shared.Models.DTOs.User user)
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

        [HttpPost("{userId}/watchlist/")]
        [Authorize]
        public async Task<IActionResult> AddCompanyToWatchlist([FromRoute] int userId,[FromBody] int companyId)
        {
            await _userService.AddCompanyToWatchlist(userId, companyId);
            return Ok();
        }

        [HttpDelete("{userId}/watchlist/{companyId}")]
        [Authorize]
        public async Task<IActionResult> DeleteCompanyFromWatchlist([FromRoute] int userId, [FromRoute] int companyId)
        {
            await _userService.DeleteCompanyFromWatchlist(userId, companyId);
            return Ok();
        }

        [HttpGet("{userId}/watchlist")]
        [Authorize]
        public async Task<IActionResult> GetWathclistCompanies(int userId)
        {
            var output = await _userService.GetWatchlistCompanies(userId);
            return Ok(output);
        }
    }
}
