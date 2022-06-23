using Microsoft.AspNetCore.Mvc;
using projektApbd.Server.Services;

namespace projektApbd.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCompanyController : Controller
    {
        private readonly IUserCompanyService _service;

        public UserCompanyController(IUserCompanyService service)
        {
            _service = service;
        }
    }
}
