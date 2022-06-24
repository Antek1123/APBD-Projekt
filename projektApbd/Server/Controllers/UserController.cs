using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public async Task<IActionResult> GetUser(string username)
        {
            return Ok(await _userService.GetUser(username));
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(projektApbd.Shared.Models.DTOs.User user)
        {
            await _userService.AddUser(user);
            return Ok();

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(projektApbd.Shared.Models.DTOs.User user)
        {
            await _userService.DeleteUser(user);
            return Ok();
        }


    }
}
