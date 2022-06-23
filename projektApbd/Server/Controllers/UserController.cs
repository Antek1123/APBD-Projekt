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
    }
}
